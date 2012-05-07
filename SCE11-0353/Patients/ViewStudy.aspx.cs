using System;
using DB_Handlers;

namespace Patients
{
    /// <summary>
    /// Code behind for the ~/Patients/ViewStudy.aspx page
    /// </summary>
    public partial class ViewStudy: System.Web.UI.Page
    {
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            // Convert username to Patient ID and get related studies
            var studies = StudyHandler.GetStudyHistory(UserParticularsHandler.GetNricFromGuid(MembershipHandler.GetGuidFromUserName(User.Identity.Name)));
            if (studies.Count > 0)
            {
                AllStudies.DataSource = studies;
                AllStudies.DataBind();
                None.Visible = false;
            }
            else
            {
                None.Visible = true;
            }
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
    }
}