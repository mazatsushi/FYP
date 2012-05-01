using System;
using System.Globalization;
using System.Web;

namespace Guest
{
    /// <summary>
    /// Code behind for the ~/Guest/ResetPassword2.aspx page
    /// </summary>

    public partial class ResetPassword2 : System.Web.UI.Page
    {
        private const string MailTemplateUri = "~/MailTemplates/PasswordReset.txt";
        private const string SuccessRedirect = "~/Guest/ResetPasswordSuccess.aspx";

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

            if (IsPostBack)
                return;

            var username = Request.QueryString["Username"];

            // We must have a username to work with here
            if (String.IsNullOrWhiteSpace(username))
                Server.Transfer(ResolveUrl("~/Guest/ResetPassword.aspx"));

            var text = new CultureInfo("en-SG").TextInfo;
            Question.Text = text.ToTitleCase(HttpUtility.HtmlDecode("Security Question: " + DatabaseHandler.GetQuestion(username)));
        }

        /// <summary>
        /// Event that triggers user clicks on the password reset button
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ResetButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var username = Request.QueryString["Username"];
            var answer = HttpUtility.HtmlEncode(Answer.Text.Trim());
            var newPassword = string.Empty;

            // Attempt to reset the password
            if (!DatabaseHandler.ResetPassword(username, answer, out newPassword))
            {
                ErrorMessage.Text = newPassword;
                return;
            }

            // Get the user email address
            var userEmail = DatabaseHandler.GetUserEmail(username);
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                ErrorMessage.Text = "Unable to find your email address in the system.";
                return;
            }

            // Send an email containing the new password to user's inbox
            MailHandler.NewPassword(username, userEmail, newPassword, Server.MapPath(MailTemplateUri));

            Response.Redirect(ResolveUrl(SuccessRedirect));
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
                    Response.Redirect(ResolveUrl("~/Admin/Default.aspx"));
                    break;
                case 1:
                    Response.Redirect(ResolveUrl("~/Patient/Default.aspx"));
                    break;
                case 2:
                    Response.Redirect(ResolveUrl("~/Physician/Default.aspx"));
                    break;
                case 3:
                    Response.Redirect(ResolveUrl("~/Radiologist/Default.aspx"));
                    break;
            }
        }
    }
}