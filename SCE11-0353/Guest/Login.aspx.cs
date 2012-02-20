using System;
using System.Web.Security;

public partial class Account_Login : System.Web.UI.Page
{
	private string[] _roleList = new string[] { "Admin", "Physician", "Radiologist", "Staff", "Patient" };

	protected void Page_Load(object sender, EventArgs e)
	{
		// If the user is already logged on, reject
		if (User.Identity.IsAuthenticated)
		{
			Server.Transfer("~/Error/Error.aspx");
		}

	    Page.Title = "Login";
	}

	/*
	 * Event that triggers after login is successful.
	 * We shall redirect the user to their respective role's home page.
	 */

	protected void OnLoggedIn(object sender, EventArgs e)
	{
		switch (FindMostPrivilegedRole(LoginUser.UserName))
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

	// Method that determines the most privileged role for the user who just logged in.
	private static int FindMostPrivilegedRole(string username)
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
