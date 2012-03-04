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
        switch (FindMostPrivilegedRole(User.Identity.Name))
        {
            case 1:
                Response.Redirect("~/Admin/Default.aspx");
                break;
            case 2:
                Response.Redirect("~/Physician/Default.aspx");
                break;
            case 3:
                Response.Redirect("~/Radiologist/Default.aspx");
                break;
            case 4:
                Response.Redirect("~/Staff/Default.aspx");
                break;
            case 5:
                Response.Redirect("~/Patient/Default.aspx");
                break;
        }
    }

    /// <summary>
    /// Finds the most privileged role that the current user is assigned to
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>A numeric value presenting the highest role of the user</returns>
    private int FindMostPrivilegedRole(string username)
    {
        int roleNum;
        /*
         * It is entirely possible that the user belongs to more than one role.
         * Regardless we shall just search for the most privileged role assigned.
         */
        if (Roles.IsUserInRole(username, "Admin"))
        {
            // User has administrator privileges
            roleNum = 1;
        }
        else if (Roles.IsUserInRole(username, "Physician"))
        {
            // User has physician privileges
            roleNum = 2;
        }
        else if (Roles.IsUserInRole(username, "Radiologist"))
        {
            // User has radiologist privileges
            roleNum = 3;
        }
        else if (Roles.IsUserInRole(username, "Staff"))
        {
            // User has staff privileges
            roleNum = 4;
        }
        else
        {
            // User has patient privileges
            roleNum = 5;
        }
        return roleNum;
    }
}
