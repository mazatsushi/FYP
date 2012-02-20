using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // If the user is already logged on, reject
        if (User.Identity.IsAuthenticated)
        {
            Server.Transfer("~/Error/Error.aspx");
        }

        Page.Title = "Register Account";
    }

    /*
     * Event that triggers when a new user account has been created.
     * We will add them to the 'Patients' role programatically here.
     * Accounts for all other roles are to be done by the 'Administrator' role.
     */
    protected void CreatedUser(object sender, EventArgs e)
    {
        // Create a persistent cookie
        //FormsAuthentication.SetAuthCookie(UserName.Text, true);

        // Add user to the patient role
    }

    // Method to check whether user email is unique
    protected void UniqueEmail_Validate(object sender, ServerValidateEventArgs e)
    {
        e.IsValid = (Membership.GetUserNameByEmail(RegisterUser.Email) == null);
    }

    // Method to check whether user account alredy exists
    protected void UniqueUserName_Validate(object sender, ServerValidateEventArgs e)
    {
        e.IsValid = (Membership.GetUser(RegisterUser.UserName) == null);
    }

    // Method to check whether user first name has numbers
    protected void FirstName_Validate(object source, ServerValidateEventArgs args)
    {
        var firstName = ((TextBox)FindControl("FirstName")).Text.Trim().ToLowerInvariant().ToCharArray();
        if (firstName.Any(c => Char.IsDigit(c)))
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }

    // Method to check whether user middle name has numbers
    protected void MiddleName_Validate(object source, ServerValidateEventArgs args)
    {
    }
}
