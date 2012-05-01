using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

namespace Admin
{
    /// <summary>
    /// Code behind for the ~/Physician/AddDrug.aspx page
    /// </summary>
    public partial class AddDrug : System.Web.UI.Page
    {
        private const string SuccessRedirect = "~/Physician/AddDrugSuccess.aspx";

        /// <summary>
        /// Event that triggers when the add button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void AddButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;
            /*
             * At this point, all user entered information has been verified.
             * We shall now programmatically insert the new drug allergy to database
             */
            if (!DatabaseHandler.AddNewDrug(new CultureInfo("en-SG").TextInfo.ToTitleCase(HttpUtility.HtmlEncode(DrugName.Text.Trim()))))
                return;

            Server.Transfer(SuccessRedirect);
        }

        /// <summary>
        /// Server side validation to check whether drug name already exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void DrugNotExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !DatabaseHandler.DrugExists(HttpUtility.HtmlEncode(DrugName.Text.Trim().ToLowerInvariant()));
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