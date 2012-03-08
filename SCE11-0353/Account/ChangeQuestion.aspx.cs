using System;
using System.Web;

/// <summary>
/// Code behind for the ~/Account/ChangeQuestion.aspx page
/// </summary>
public partial class Account_ChangeQuestion : System.Web.UI.Page
{
    private string _username;
    private const string RedirectToLogin = "~/Guest.Login.aspx";
    private const string RedirectToSuccess = "~/Account/ChangeQuestionSuccess";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        _username = User.Identity.Name;
        var question = DatabaseHandler.GetQuestion(User.Identity.Name);

        if (!String.IsNullOrEmpty(question))
            Question.Text = question;
        else
            Server.Transfer(RedirectToLogin);
    }

    /// <summary>
    /// Event handler for when the Cancel button in this page is clicked
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
                Response.Redirect("~/Physician/Default.aspx");
                break;
            case 2:
                Response.Redirect("~/Radiologist/Default.aspx");
                break;
            case 3:
                Response.Redirect("~/Staff/Default.aspx");
                break;
            case 4:
                Response.Redirect("~/Patient/Default.aspx");
                break;
        }
    }
    /// <summary>
    /// Event handler for when the update button in this page is clicked
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void UpdateButtonClick(object sender, EventArgs e)
    {
        var password = HttpUtility.HtmlEncode(Password.Text.Trim());
        var question = HttpUtility.HtmlEncode(Question.Text.Trim());
        var answer = HttpUtility.HtmlEncode(Answer.Text.Trim().ToLowerInvariant());

        if (!DatabaseHandler.ChangeQuestionAndAnswer(_username, password, question, answer))
            return;

        Server.Transfer(RedirectToSuccess);
    }
}