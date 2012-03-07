using System;
using System.Web;

public partial class Guest_ResetPasswordStep2 : System.Web.UI.Page
{
    private string _username;
    private const string Prepend = "Security Question: ";
    private const string RedirectUrl = "~/Guest/ResetPassword.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        _username = Request.QueryString["Username"];

        if (String.IsNullOrWhiteSpace(_username))
        {
            Server.Transfer(RedirectUrl);
        }
        
        var temp = DatabaseHandler.GetResetQuestion(_username);
        if (String.IsNullOrWhiteSpace(temp))
        {
            Server.Transfer(RedirectUrl);
        }

        Question.Text = HttpUtility.HtmlDecode(Prepend + temp);
    }

    protected void ResetButtonClick(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        var answer = HttpUtility.HtmlEncode(Answer.Text);
        var newPassword = string.Empty;

        // Attempt to reset the password
        if (!DatabaseHandler.ResetPassword(_username, answer, out newPassword))
        {
            ErrorMessage.Text = newPassword;
            return;
        }

        // Get the user email address
        var userEmail = DatabaseHandler.GetUserEmail(_username);
        if (string.IsNullOrWhiteSpace(userEmail))
        {
            ErrorMessage.Text = "Uunable to find your registered email address in the system.";
            return;
        }

        // Send an email containing the new password to user's inbox
        MailHandler.SendNewPassword(userEmail, newPassword);

        Server.Transfer("~/Guest/ResetPasswordSuccess.aspx");
    }
}