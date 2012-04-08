using System;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/ManageImagingOrder2.aspx page
    /// </summary>

    public partial class ManageImagingOrder2 : System.Web.UI.Page
    {
        // TODO: Refactor this class to fit its predecessor (most probably going to be removed)
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            var nric = Request.QueryString["Nric"];
            if (string.IsNullOrEmpty(nric))
                Response.Redirect("~/Physician/ManageImagingOrder.aspx");

            PatientNameLabel.Text += DatabaseHandler.GetUserNameFromNric(nric);
            StudyGrid.DataSource = DatabaseHandler.GetStudies(nric);
            StudyGrid.DataBind();
        }

        /// <summary>
        /// Event that triggers when the retrieve button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void RetrieveButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var id = 0;
            if (!int.TryParse(Study.Text.Trim(), out id))
            {
                ErrorMessage.Text = "Please enter a valid Study ID";
                return;
            }

            Response.Redirect("~/Physician/ViewStudy.aspx?StudyID=" + id);
        }
    }
}