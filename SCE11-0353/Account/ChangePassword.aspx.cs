using System;
using System.Web.Security;

/// <summary>
/// Code behind for the ~/Account/ChangePassword.aspx page
/// </summary>

public partial class Account_ChangePassword : System.Web.UI.Page
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Event handler for when the Cancel button in this page is clicked
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void CancelPushButton_Click(object sender, EventArgs e)
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
}
