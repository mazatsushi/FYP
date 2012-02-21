using System;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

/*
 * Note that all custom validators are validated according to where they are placed in the ASP file.
 * E.g. a validator located at line 10 would trigger before a validator at line 20.
 * It is this behaviour that we are utilizing to do the server validation.
 */

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

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        //using (var context = new RISDB_Context())
        //{
        //    Table<Country> countries = context.GetTable<Country>();
        //    IQueryable<Country> query = from cust in countries
        //                                select cust;
        //    foreach (var country in query)
        //    {
        //        Console.WriteLine("Country Name: {0}", country.CountryName);
        //    }
        //    Console.ReadLine();
        //}
    }

    // Method to check whether user email is unique
    protected void UniqueEmail_Validate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = (Membership.GetUserNameByEmail(HttpUtility.HtmlEncode(Email.Text.Trim().ToLowerInvariant())) ==
                        null);
    }

    // Method to check whether user account alredy exists
    protected void UniqueUserName_Validate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = (Membership.GetUser(HttpUtility.HtmlEncode(UserName.Text.Trim())) == null);
    }

    // Server side validation to check whether NRIC provided is a null value
    protected void NRIC_NotNull(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !string.IsNullOrEmpty(HttpUtility.HtmlEncode(NRIC.Text));
    }

    // Server side validation to check whether NRIC provided is valid
    protected void NRIC_Pattern(object source, ServerValidateEventArgs args)
    {
        var match = new Regex(@"^[SFTG]\d{7}[A-Z]$").Match(HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant()));
        args.IsValid = match.Success;
    }

    // Server side validation to check whether first name provided is valid
    protected void FirstName_NotNull(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !string.IsNullOrEmpty(HttpUtility.HtmlEncode(FirstName.Text));
    }


    // Server side validation to check whether user first name has numbers
    protected void FirstName_Pattern(object source, ServerValidateEventArgs args)
    {
        var firstName = (HttpUtility.HtmlEncode(FirstName.Text.Trim().ToCharArray()));
        args.IsValid = !firstName.Any(Char.IsDigit);
    }

    // Server side validation to check whether middle name provided has numbers
    protected void MiddleName_Pattern(object source, ServerValidateEventArgs args)
    {
        var temp = HttpUtility.HtmlEncode(MiddleName.Text);
        if (string.IsNullOrEmpty(temp))
            return;

        var middleName = (HttpUtility.HtmlEncode(temp.Trim().ToCharArray()));
        args.IsValid = !middleName.Any(Char.IsDigit);
    }

    // Server side validation to check whether last name provided is valid
    protected void LastName_NotNull(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !string.IsNullOrEmpty(HttpUtility.HtmlEncode(LastName.Text));
    }

    // Server side validation to check whether user first name has numbers
    protected void LastName_Pattern(object source, ServerValidateEventArgs args)
    {
        var lastName = (HttpUtility.HtmlEncode(LastName.Text.Trim().ToCharArray()));
        args.IsValid = !lastName.Any(Char.IsDigit);
    }

    // Server side validation to check whether last name provided is valid
    protected void Gender_NotNull(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !string.IsNullOrEmpty(Gender.SelectedValue);
    }

    // Server side validation to check whether user first name has numbers
    protected void Gender_Pattern(object source, ServerValidateEventArgs args)
    {
        var gender = HttpUtility.HtmlEncode(Gender.Text.Trim().ToLowerInvariant());

        // First check that the value is male
        args.IsValid = ((String.CompareOrdinal(gender, "male")) == 0);

        /*
         * The IF block will be entered iff the above statement returns false,
         * i.e. the string value is not male
         */
        if (!args.IsValid)
        {
            // Then check that the value is female
            args.IsValid = ((String.CompareOrdinal(gender, "female")) == 0);
        }
    }
}
