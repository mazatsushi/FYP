using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Guest
{
    /// <summary>
    /// Code behind for the ~/Guest/ResetPassword.aspx page
    /// </summary>

    public partial class ResetPassword : System.Web.UI.Page
    {

        /// <summary>
        /// Event that triggers user clicks on the next button
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void NextButtonClick(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            var username = HttpUtility.HtmlEncode(UserName.Text.Trim());
            Response.Redirect("~/Guest/ResetPasswordStep2.aspx?Username=" + username);
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Reject if the user is already authenticated
            if (User.Identity.IsAuthenticated)
                TransferToHome(User.Identity.Name);
        }

        /// <summary>
        /// Method that redirects the user to their role's home page
        /// </summary>
        /// <param name="username">The user name</param>
        private void TransferToHome(string username)
        {
            switch (DatabaseHandler.FindMostPrivilegedRole(username))
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
        /// Validator method to check whether username exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void UserNameExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = DatabaseHandler.UserNameExists(HttpUtility.HtmlEncode(UserName.Text.Trim()));
        }
    }
}