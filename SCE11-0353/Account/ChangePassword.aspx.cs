using System;

namespace Account
{
    /// <summary>
    /// Code behind for the ~/Account/ChangePassword.aspx page
    /// </summary>

    public partial class ChangePassword : System.Web.UI.Page
    {
        /// <summary>
        /// Event that triggers user clicks on the cancel button
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void CancelButtonClick(object sender, EventArgs e)
        {
            switch (DatabaseHandler.FindMostPrivilegedRole(User.Identity.Name))
            {
                case 0:
                    Response.Redirect("~/Admin/Default.aspx");
                    break;
                case 1:
                    Response.Redirect("~/Patient/Default.aspx");
                    break;
                case 2:
                    Response.Redirect("~/Physician/Default.aspx");
                    break;
                case 3:
                    Response.Redirect("~/Radiologist/Default.aspx");
                    break;
            }
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
