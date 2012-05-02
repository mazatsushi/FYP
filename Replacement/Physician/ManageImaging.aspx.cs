using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/ManageImaging.aspx page
    /// It is assumed that every patient can have at most 1 unresolved study at any time
    /// </summary>
    public partial class ManageImaging : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Physician/ManagePatient.aspx";
        private const string SuccessRedirect = "~/Physician/AppointmentAdded.aspx";

        /// <summary>
        /// Event that triggers when the 'Add Allergy' button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void AddButtonClick(object sender, EventArgs e)
        {
            Validate("AddValidationGroup");
            if (!IsValid)
                return;

            // Check that the user is not creating an appointment for a completed study

            var studyId = -1;
            if (Session["OpenStudy"] == null)
            {
                // Get study ID from new record insertion
                var desc = Description.Text.Trim();
                var date = DatePicker.SelectedDate;
            }
            else
                // Get study ID from session state
                studyId = int.Parse(Session["OpenStudy"].ToString());

            /*
                 * Scenario 1 - No history or no open studies
                 * Step 1) Create new study in database (remember to get current staff ID)
                 * Step 2) Create new appointment
                 * Step 3) Link appointment via foreign key to study
                 */
            /*
                 * Case 2 - Has existing study
                 * Step 1) Create new appointment
                 * Step 2) Link appointment via foreign key to study
                 */
            //Session["AllStudies"] = null;
            //Session["OpenStudy"] = null;
            //Response.Redirect(ResolveUrl(SuccessRedirect));
        }

        /// <summary>
        /// Checks whether the proposed appointment time is later than the current time
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void DateTimeCheck(object sender, ServerValidateEventArgs args)
        {
            var proposed = DatePicker.SelectedDate;
            args.IsValid = proposed > DateTime.Now;
        }

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize()
        {
            DatePicker.MinValidDate = DateTime.Now;
            DatePicker.MaxValidDate = DateTime.Now.AddYears(10);

            // Let physician know which patient is currently being managed
            var nric = Session["Nric"].ToString();
            PatientName.Text = Session["PatientName"].ToString();

            // Determine if patient has existing studies first
            var history = DatabaseHandler.GetStudies(nric);
            var open = DatabaseHandler.GetOpenStudy(nric);
            ToggleAllStudies(history.Count > 0, history.ToList());
            ToggleExistingStudies(open > -1, open);
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

            // We must have the patient's NRIC and full name before proceeding.
            if (Session["Nric"] == null || Session["PatientName"] == null)
                Server.Transfer(ResolveUrl(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                    CryptoHandler.GetHash(Request.Url.ToString())));

            Initialize();
        }

        /// <summary>
        /// Event that triggers when the return button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ResetButtonClick(object sender, EventArgs e)
        {
            Session.Clear();
        }

        /// <summary>
        /// Displays / hides elements in the newStudyDiv based upon the value of show
        /// </summary>
        /// <param name="show">True to show elements. False to hide elements.</param>
        /// <param name="list">A list of the patient's medical imaging history.</param>
        private void ToggleAllStudies(bool show, IList<Study> list)
        {
            Session["AllStudies"] = list;
            AllStudies.Visible = show;
            Some.Visible = show;
            None.Visible = !show;

            // Show history if true
            if (show)
            {
                // TODO: Complete this portion
                ;
            }
        }

        /// <summary>
        /// Displays / hides elements in the existingStudyDiv based upon the value of show
        /// </summary>
        /// <param name="show">True to show elements. False to hide elements.</param>
        /// <param name="openId">The study ID of the open study.</param>
        private void ToggleExistingStudies(bool show, int openId)
        {
            Session["OpenStudy"] = openId;
            existingStudyDiv.Visible = show;

            // Show open study ID if true
            if (show)
                ExistingId.Text = openId.ToString(CultureInfo.InvariantCulture);
        }
    }
}