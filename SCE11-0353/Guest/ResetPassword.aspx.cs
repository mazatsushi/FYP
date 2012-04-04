using System;
using System.Web;
using System.Web.UI.WebControls;

public partial class Account_ResetPassword : System.Web.UI.Page
{
    private const string RedirectUrl = "~/Guest/ResetPasswordStep2.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void UserNameExists(object source, ServerValidateEventArgs args)
    {
        args.IsValid = DatabaseHandler.UserNameExists(HttpUtility.HtmlEncode(UserName.Text.Trim()));
    }

    protected void NextButtonClick(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        var username = HttpUtility.HtmlEncode(UserName.Text.Trim());
        Response.Redirect(RedirectUrl + "?Username=" + username);
    }
}