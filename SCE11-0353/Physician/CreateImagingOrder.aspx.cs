using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/CreateImagingOrder.aspx page
    /// </summary>

    public partial class CreateImagingOrder : System.Web.UI.Page
    {
        // TODO: Refactor this class to use common NRIC search feature
        /// <summary>
        /// Event that triggers when the create button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void CreateButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var nric = HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant());
            if (!DatabaseHandler.IsInRole(nric, "Patient"))
            {
                ErrorMessage.Text = "NRIC provided does not belong to any patient.";
                return;
            }

            if (!DatabaseHandler.HasMedicalRecords(nric))
            {
                ErrorMessage.Text = "Patient has no medical records in system. Please update blood type to create it.";
                return;
            }

            if (DatabaseHandler.HasOpenStudies(nric))
            {
                ErrorMessage.Text = "Patient still has uncompleted studies.";
                return;
            }
            /*
             * At this point, all user entered information has been verified.
             * We shall now perform two critical actions:
             * 1) Programmatically create a new study
             * 2) Programmatically create a new appointment
             */
            // Step 1
            var desc = HttpUtility.HtmlEncode(Description.Text.ToLowerInvariant());
            var referredBy = DatabaseHandler.GetGuidFromUserName(User.Identity.Name);

            var studyId = DatabaseHandler.CreateImagingOrder(desc, referredBy);
            if (studyId == 0)
            {
                ErrorMessage.Text = "An error occured while creating a new study.";
                return;
            }

            // Step 2
            var date = DateTime.Parse(Date.Text.Trim());
            date = date.AddHours(Int32.Parse(HttpUtility.HtmlEncode(Hour.Text.Trim())));
            switch (HttpUtility.HtmlEncode(AMPM.Text.Trim()))
            {
                case "PM":
                    date = date.AddHours(12);
                    break;
            }
            date = date.AddMinutes((Int32.Parse(HttpUtility.HtmlEncode(Minute.Text.Trim()))));

            if (!DatabaseHandler.CreateAppointment(date, studyId, nric))
            {
                ErrorMessage.Text = "An error occured while creating a new appointment.";
                return;
            }

            Response.Redirect("~/Physician/CreateImagingOrderSuccess.aspx");
        }

        /// <summary>
        /// Server side validation to check whether NRIC already exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void NricExists(object source, ServerValidateEventArgs args)
        {
            /*
         * Step 1: Desensitize the input
         * Step 2: Check for existing NRIC
         */
            args.IsValid = DatabaseHandler.NricExists(HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant()));
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            DateRangeCheck.MinimumValue = DateTime.Today.ToShortDateString();
            DateRangeCheck.MaximumValue = DateTime.Today.AddYears(2).ToShortDateString();

            var hour = new List<int>();
            for (var i = 1; i < 13; i++)
            {
                hour.Add(i);
            }
            Hour.DataSource = hour;
            Hour.DataBind();

            var min = new List<int>();
            for (var i = 0; i < 60; i++)
            {
                min.Add(i);
            }
            Minute.DataSource = min;
            Minute.DataBind();

            AMPM.DataSource = new List<string> { "AM", "PM" };
            AMPM.DataBind();
        }
    }
}