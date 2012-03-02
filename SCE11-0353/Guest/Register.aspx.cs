using System;
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
    // Private constant used for ASP.NET Membership user creation
    private const bool IsApproved = true;
    private const string RoleName = "Patient";

    protected void Page_Load(object sender, EventArgs e)
    {
        // If the user is already logged on, reject
        if (User.Identity.IsAuthenticated)
            Server.Transfer("~/Error/Error.aspx");
    }

    /*
     * Event that triggers when a new user account has been created.
     * We will add them to the 'Patients' role programatically here.
     * Accounts for all other roles are to be done by the 'Administrator' role.
     */
    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        /*
         * At this point, all user entered information has been verified.
         * We shall now perform two critical actions:
         * 1) Programmatically add account information to the Membership provider
         *  1.1) Note that since we manually checked whether the username and email are unique,
         *  it is 100% guaranteed that Membership information is valid as well.
         * 2) Programmatically insert personal particulars into the associated table.
         * 3) Programmatically add the newly created user to the 'Patient' role.
         */

        // Fetch information that is needed for creating a new account
        var username = UserName.Text.Trim();
        var password = Password.Text.Trim();
        var email = Email.Text.Trim().ToLowerInvariant();
        var question = Question.Text.Trim();
        var answer = Answer.Text.Trim();

        // Create new account in Membership
        MembershipCreateStatus status;
        var user = Membership.CreateUser(username, password, email, question, answer, IsApproved, out status);

        // Return and show error message if account creation unsuccessful
        if (user == null)
        {
            ErrorMessage.Text += HttpUtility.HtmlDecode("<ul>");
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>Username already exists. Please enter a different user name.</li>");
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>A username for that e-mail address already exists. Please enter a different e-mail address.</li>");
                    break;

                case MembershipCreateStatus.InvalidPassword:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>The password provided is invalid. Please enter a valid password value.</li>");
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>The e-mail address provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.InvalidAnswer:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>The password retrieval answer provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.InvalidQuestion:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>The password retrieval question provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.InvalidUserName:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>The user name provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.ProviderError:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.</li>");
                    break;

                case MembershipCreateStatus.UserRejected:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.</li>");
                    break;

                default:
                    ErrorMessage.Text += HttpUtility.HtmlDecode("<li>An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.</li>");
                    break;
            }
            ErrorMessage.Text += HttpUtility.HtmlDecode("</ul>");
            return;
        }

        // Fetch information that is needed for storing personal information
        var nric = NRIC.Text.Trim().ToUpperInvariant();
        var firstName = FirstName.Text.Trim();
        var middleName = string.Empty;
        if (!String.IsNullOrEmpty(MiddleName.Text))
            middleName = MiddleName.Text.Trim();
        var lastName = LastName.Text.Trim();
        var gender = Char.Parse(Gender.SelectedValue);
        var namePrefix = Prefix.Text.Trim();
        var nameSuffix = string.Empty;
        if (!String.IsNullOrEmpty(Suffix.Text))
            nameSuffix = Suffix.Text.Trim();
        var dob = DateTime.Parse(DateOfBirth.Text.Trim());
        var address = Address.Text.Trim();
        var contact = ContactNumber.Text.Trim();
        var postalCode = PostalCode.Text.Trim();
        var country = Country.Text.Trim();
        var nationality = Nationality.Text.Trim();

        // Add user personal information into the UserParticulars table
        using (var db = new RIS_DB())
        {
            // Get the country ID
            var countryId = (from cty in db.Countries
                             where cty.CountryName == country
                             select cty).First<Country>().CountryId;

            // Insert all data into user particulars table
            var userParticulars = new UserParticular()
                                      {
                                          NRIC = nric,
                                          FirstName = firstName,
                                          MiddleName = middleName,
                                          LastName = lastName,
                                          Gender = gender,
                                          Prefix = namePrefix,
                                          Suffix = nameSuffix,
                                          DateOfBirth = dob,
                                          Address = address,
                                          ContactNumber = contact,
                                          PostalCode = postalCode,
                                          CountryOfResidence = countryId,
                                          Nationality = nationality,
                                          //Ignore the potential error below, since we are 100% sure it is valid
                                          UserId = Guid.Parse(user.ProviderUserKey.ToString())
                                      };
            db.UserParticulars.InsertOnSubmit(userParticulars);
            db.SubmitChanges();
        }

        // Add user to the patient role then redirect as appropriate
        Roles.AddUserToRole(username, RoleName);
        Server.Transfer("~/Guest/AccountCreated.aspx");
    }

    // Server side validation to check whether NRIC already exists
    protected void NRICNotExists(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for existing NRIC
         */
        var input = (HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant()));
        using (var db = new RIS_DB())
        {
            var query = from record in db.UserParticulars
                        where (record.NRIC == input)
                        select record.NRIC;
            args.IsValid = !query.Any();
        }
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

    // Server side validation to check whether date of birth is within acceptable values
    protected void IsDobValid(object source, ServerValidateEventArgs args)
    {
        var dob = DateTime.Parse(DateOfBirth.Text.Trim());
        args.IsValid = (dob < DateTime.Today);
    }

    // Server side validation to check whether nationality is within acceptable values
    protected void IsNationalityValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for numeric characters
         */
        var lastName = (HttpUtility.HtmlEncode(LastName.Text.Trim().ToCharArray()));
        args.IsValid = !lastName.Any(Char.IsDigit);
    }

    // Method to check whether username already exists
    protected void UserNameNotExists(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = (Membership.GetUser(HttpUtility.HtmlEncode(UserName.Text.Trim())) == null);
    }

    // Method to check whether user email already exists
    protected void EmailNotInUse(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = (Membership.GetUserNameByEmail(HttpUtility.HtmlEncode(Email.Text.Trim().ToLowerInvariant())) ==
                        null);
    }
}
