using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

/// <summary>
/// Code behind for the ~/Guest/Register.aspx page
/// 
/// Note:
/// 1) All user input are first desensitized by calling the HttpUtility.HTMLEncode() method.
/// For more information, please refer to: http://msdn.microsoft.com/en-us/library/73z22y6h.aspx
/// 
/// 2) We let the compiler determine at run-time the data type of local variables.
/// Hence the use of the new C# keyword 'var'. For more information, please refer to:
/// http://msdn.microsoft.com/en-us/library/bb384061.aspx
/// </summary>

public partial class Account_Register : System.Web.UI.Page
{
    /// <summary>
    /// Private constants used for ASP.NET Membership user creation
    /// </summary>
    private const bool IsApproved = true;
    private const string RoleName = "Patient";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // If the user is already logged on, reject
        // Reject if the user is already authenticated
        if (User.Identity.IsAuthenticated)
        {
            TransferToHome(User.Identity.Name);
        }

        if (IsPostBack)
            return;

        DateRangeCheck.MinimumValue = "1/1/1900";
        DateRangeCheck.MaximumValue = DateTime.Today.ToShortDateString();
    }

    /// <summary>
    /// Event that triggers when a new user account has been created.
    /// We will add them to the 'Patients' role programatically here.
    /// Accounts for all other roles are to be done by the 'Administrator' role.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
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
        var answer = Answer.Text.Trim().ToLowerInvariant();

        // Create new account in Membership
        MembershipCreateStatus status;
        var user = DatabaseHandler.CreateUser(username, password, email, question, answer, IsApproved, out status);

        // Return and show error message if account creation unsuccessful
        if (user == null)
        {
            ErrorMessage.Text = HttpUtility.HtmlDecode("<ul>");
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>Username already exists. Please enter a different user name.</li>");
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>A username for that e-mail address already exists. Please enter a different e-mail address.</li>");
                    break;

                case MembershipCreateStatus.InvalidPassword:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>The password provided is invalid. Please enter a valid password value.</li>");
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>The e-mail address provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.InvalidAnswer:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>The password retrieval answer provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.InvalidQuestion:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>The password retrieval question provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.InvalidUserName:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>The user name provided is invalid. Please check the value and try again.</li>");
                    break;

                case MembershipCreateStatus.ProviderError:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact the system administrator.</li>");
                    break;

                case MembershipCreateStatus.UserRejected:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact the system administrator.</li>");
                    break;

                default:
                    ErrorMessage.Text = HttpUtility.HtmlDecode("<li>An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact the system administrator.</li>");
                    break;
            }
            ErrorMessage.Text = HttpUtility.HtmlDecode("</ul>");
            return;
        }

        // Fetch information that is needed for storing personal information
        var nric = NRIC.Text.Trim().ToUpperInvariant();
        var firstName = FirstName.Text.Trim();
        string middleName = null;
        if (!String.IsNullOrEmpty(MiddleName.Text))
            middleName = MiddleName.Text.Trim();
        var lastName = LastName.Text.Trim();
        var gender = Char.Parse(Gender.SelectedValue);
        var namePrefix = Prefix.Text.Trim();
        string nameSuffix = null;
        if (!String.IsNullOrEmpty(Suffix.Text))
            nameSuffix = Suffix.Text.Trim();
        var dob = DateTime.Parse(DateOfBirth.Text.Trim());
        var address = Address.Text.Trim();
        var contact = ContactNumber.Text.Trim();
        var postalCode = PostalCode.Text.Trim();
        var nationality = Nationality.Text.Trim();
        var countryId = DatabaseHandler.GetCountryId(Country.Text.Trim());

        // Add user personal information into the UserParticulars table
        var addStatus = DatabaseHandler.AddUserParticulars(user.ProviderUserKey, nric, firstName, middleName, lastName, gender, namePrefix, nameSuffix, dob, address, contact, postalCode, countryId, nationality);

        if (!addStatus)
        {
            ErrorMessage.Text = HttpUtility.HtmlDecode("<ul>");
            ErrorMessage.Text = HttpUtility.HtmlDecode("<li>An error occured while adding your information. Please contact the system administrator.</li>");
            ErrorMessage.Text = HttpUtility.HtmlDecode("</ul>");
            return;
        }
        
        // Add user to the patient role then redirect as appropriate
        addStatus = DatabaseHandler.AddUserToRole(username, RoleName);
        if (!addStatus)
        {
            ErrorMessage.Text = HttpUtility.HtmlDecode("<ul>");
            ErrorMessage.Text = HttpUtility.HtmlDecode("<li>An error occured while adding your role. Please contact the system administrator.</li>");
            ErrorMessage.Text = HttpUtility.HtmlDecode("</ul>");
            return;
        }
        
        Server.Transfer("~/Guest/AccountCreated.aspx");
    }

    /// <summary>
    /// Server side validation to check whether NRIC already exists
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void NricNotExists(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for existing NRIC
         */
        args.IsValid = !DatabaseHandler.NricExists(HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant()));
    }

    /// <summary>
    /// Server side validation to check whether first name no numeric characters
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void IsFirstNameValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for numeric characters
         */
        var firstName = (HttpUtility.HtmlEncode(FirstName.Text.Trim().ToCharArray()));
        args.IsValid = !firstName.Any(Char.IsDigit);
    }

    /// <summary>
    /// Server side validation to check whether middle name has no numeric characters
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
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

    /// <summary>
    /// Server side validation to check whether last name has no numeric characters
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void IsLastNameValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for numeric characters
         */
        var lastName = (HttpUtility.HtmlEncode(LastName.Text.Trim().ToCharArray()));
        args.IsValid = !lastName.Any(Char.IsDigit);
    }

    /// <summary>
    /// Server side validation to check whether gender is within acceptable values
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
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

    /// <summary>
    /// Server side validation to check whether prefix is within acceptable values
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
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

    /// <summary>
    /// Server side validation to check whether suffix is within acceptable values
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
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

    /// <summary>
    /// Method to check whether nationality is valid
    /// </summary>
    /// <param name="source">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void IsNationalityValid(object source, ServerValidateEventArgs args)
    {
        /*
         * Step 1: Desensitize the input
         * Step 2: Check for numeric characters
         */
        var lastName = (HttpUtility.HtmlEncode(LastName.Text.Trim().ToCharArray()));
        args.IsValid = !lastName.Any(Char.IsDigit);
    }

    /// <summary>
    /// Method to check whether username already exists
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void UserNameNotExists(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = !DatabaseHandler.UserNameExists(HttpUtility.HtmlEncode(UserName.Text.Trim()));
    }

    /// <summary>
    /// Method to check whether user email already exists
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void EmailNotInUse(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = !DatabaseHandler.EmailInUse(HttpUtility.HtmlEncode(Email.Text.Trim().ToLowerInvariant()));
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
                Response.Redirect("~/Patient/Default.aspx");
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
        }
    }
}
