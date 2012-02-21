using System;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
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
    protected void UniqueEmail_Validate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = (Membership.GetUserNameByEmail(Email.Text.Trim()) == null);
    }

    // Method to check whether user account alredy exists
    protected void UniqueUserName_Validate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = (Membership.GetUser(UserName.Text.Trim()) == null);
    }

    // Method to check whether user middle name has numbers
    protected void MiddleName_Validate(object source, ServerValidateEventArgs args)
    {
    }

    // Method to check whether NRIC provided is a null value
    protected void NRIC_Required(object source, ServerValidateEventArgs args)
    {
        var nric = NRIC.Text;
        if (nric == null || nric.Trim().Length == 0)
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }

    // Method to check whether NRIC provided is valid
    protected void NRIC_Regex(object source, ServerValidateEventArgs args)
    {
        var match = new Regex(@"^[SFTG]\d{7}[A-Z]$").Match(NRIC.Text.Trim().ToUpper());
        if (!match.Success)
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }

    // Method to check whether user first name has numbers
    protected void FirstName_Regex(object source, ServerValidateEventArgs args)
    {
        var firstName = (FirstName.Text.Trim().ToLowerInvariant().ToCharArray());
        if (firstName.Any(c => Char.IsDigit(c)))
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        var context = new DataClassesDataContext();
        Table<Country> countries = context.GetTable<Country>();
        IQueryable<Country> query = from cust in countries
                                    select cust;
        foreach (var country in query)
        {
            Console.WriteLine("Country Name: {0}", country.CountryName);
        }
        Console.ReadLine();
    }
}
