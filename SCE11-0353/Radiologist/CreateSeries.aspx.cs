using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/CreateSeries.aspx page
    /// </summary>

    public partial class CreateSeries : System.Web.UI.Page
    {
        // TODO: Refactor this class to use the common NRIC search feature
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
                ErrorMessage.Text = "NRIC provided is not a patient.";
            }

            if (!DatabaseHandler.HasOpenStudies(nric))
            {
                ErrorMessage.Text = "Patient has no imaging orders.";
                return;
            }

            Server.Transfer("~/Radiologist/StartNewSeries2.aspx?NRIC=" + HttpUtility.HtmlEncode(NRIC.Text.Trim()));
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
        }
    }
}