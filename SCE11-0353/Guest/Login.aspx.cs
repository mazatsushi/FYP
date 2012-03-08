using System;
using System.Web.Security;

/// <summary>
/// Code behind for the ~/Guest/Login.aspx page
/// </summary>

public partial class Account_Login : System.Web.UI.Page
{
	/// <summary>
	/// Page load event
	/// </summary>
	/// <param name="sender">The web element that triggered the event</param>
	/// <param name="e">Event parameters</param>
	protected void Page_Load(object sender, EventArgs e)
	{
		// Reject if the user is already authenticated
		if (User.Identity.IsAuthenticated)
		{
			TransferToHome(User.Identity.Name);
		}
    }

	/// <summary>
	/// Event that triggers when the user is successfully logged in.
	/// </summary>
	/// <param name="sender">The web element that triggered the event</param>
	/// <param name="e">Event parameters</param>
	protected void OnLoggedIn(object sender, EventArgs e)
	{
		TransferToHome(LoginUser.UserName);
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
