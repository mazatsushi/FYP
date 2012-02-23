using System;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

/*
 * Notes: 
 * 
 * 1) All user input are first desensitized by calling the HttpUtility.HTMLEncode() method.
 * For more information, please refer to:
 * http://msdn.microsoft.com/en-us/library/73z22y6h.aspx
 * 
 * 2) We let the compiler determine at run-time the data type of local variables.
 * Hence the use of the new C# keyword 'var'. For more information, please refer to:
 * http://msdn.microsoft.com/en-us/library/bb384061.aspx
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

        /*
         * Validates all controls on the page, just in case of server postback.
         * Specifically, we are checking whether the server is returning any error messages
         * from user submitted information beforehand.
         * For more information, please refer to:
         * http://msdn.microsoft.com/en-us/library/0ke7bxeh.aspx
         */
        if (Page.IsPostBack)
        {
            Validate();
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
        /*
         * Trigger all validators to perform input checks.
         * This single line is all that we need for server side input validation.
         * For more information, please refer to:
         * http://msdn.microsoft.com/en-us/library/0ke7bxeh.aspx
         */
        Validate();

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

    // Method to check whether user account already exists
    protected void UniqueUserName_Validate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = (Membership.GetUser(HttpUtility.HtmlEncode(UserName.Text.Trim())) == null);
    }

    // Server side validation to check whether first name no numeric characters
    protected void IsFirstNameValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for numeric characters
         */
        var firstName = (HttpUtility.HtmlEncode(FirstName.Text.Trim().ToCharArray()));
        args.IsValid = !firstName.Any(Char.IsDigit);
    }

    // Server side validation to check whether middle name has no numeric characters
    protected void IsMiddleNameValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for null or empty value
         * Step 3: Check for numeric characters
         */
        var temp = HttpUtility.HtmlEncode(MiddleName.Text);
        if (string.IsNullOrEmpty(temp))
            return;

        var middleName = (temp.Trim().ToCharArray());
        args.IsValid = !middleName.Any(Char.IsDigit);
    }

    // Server side validation to check whether last name has no numeric characters
    protected void IsLastNameValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for numeric characters
         */
        var lastName = (HttpUtility.HtmlEncode(LastName.Text.Trim().ToCharArray()));
        args.IsValid = !lastName.Any(Char.IsDigit);
    }

    // Server side validation to check whether gender is within acceptable values
    protected void IsGenderValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for valid input range
         */
        char gender;
        var parse = Char.TryParse(HttpUtility.HtmlEncode(Gender.SelectedValue.Trim().ToLowerInvariant()), out gender);
        if (!parse)
        {
            args.IsValid = false;
            return;
        }

        /*
         * We utilize the implicit fall through feature of the switch statement as
         * more than one value is valid.
         * For more information, please refer to:
         * http://msdn.microsoft.com/en-us/library/06tc147t.aspx
         */
        switch (gender)
        {
            // True iff gender == 'm' || gender == 'f'
            case 'm':
            case 'f':
                args.IsValid = true;
                return;
            default:
                args.IsValid = false;
                return;
        }
    }

    // Server side validation to check whether prefix is within acceptable values
    protected void IsPrefixValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for valid input range
         */
        var prefix = HttpUtility.HtmlEncode(Prefix.Text.Trim().ToLowerInvariant());

        /*
         * We utilize the implicit fall through feature of the switch statement as
         * more than one value is valid.
         * For more information, please refer to:
         * http://msdn.microsoft.com/en-us/library/06tc147t.aspx
         */
        switch (prefix)
        {
            // True iff prefix == "dr." || prefix == "mdm." || prefix == "mr." || prefix == "ms." || prefix == "prof."
            case "dr.":
            case "mdm.":
            case "mr.":
            case "ms.":
            case "prof.":
                args.IsValid = true;
                return;
            default:
                args.IsValid = false;
                return;
        }
    }

    // Server side validation to check whether suffix is within acceptable values
    protected void IsSuffixValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for null or empty input
         * Step 3: Check for valid input range
         */
        var temp = HttpUtility.HtmlEncode(Suffix.SelectedValue);
        if (String.IsNullOrEmpty(temp))
            return;

        var suffix = (temp.Trim().ToLowerInvariant());
        switch (suffix)
        {
            // True iff suffix == "jr." || suffix == "sr."
            case "jr.":
            case "sr.":
                args.IsValid = true;
                return;
            default:
                args.IsValid = false;
                return;
        }
    }
}
