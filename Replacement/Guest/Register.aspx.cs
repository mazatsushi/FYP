using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Guest
{
    /// <summary>
    /// Code behind for the ~/Guest/Register.aspx page
    /// </summary>

    public partial class Register : System.Web.UI.Page
    {
        private const string SuccessRedirect = "~/Guest/AccountCreated.aspx";

        /// <summary>
        /// Private constants used for ASP.NET Membership user creation
        /// </summary>
        private const bool IsApproved = true;
        private const string RoleName = "Patient";

        /// <summary>
        /// Checks whether user email already exists
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void EmailNotInUse(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = !DatabaseHandler.EmailInUse(HttpUtility.HtmlEncode(Email.Text.Trim().ToLowerInvariant()));
        }

        /// <summary>
        /// Checks whether first name no numeric characters
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
        /// Checks whether gender is within acceptable values
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
        /// Checks whether last name has no numeric characters
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
        /// Checks whether middle name has no numeric characters
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
        /// Checks whether nationality is valid
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
        /// Checks whether prefix is within acceptable values
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
        /// Checks whether suffix is within acceptable values
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
        /// Checks whether NRIC already exists
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

            if (IsPostBack)
                return;

            DateRangeCheck.MinimumValue = DateTime.Parse("1/1/1900").ToShortDateString();
            DateRangeCheck.MaximumValue = DateTime.Today.ToShortDateString();

            var countries = DatabaseHandler.GetAllCountries();
            countries.Insert(0, "");
            Country.DataSource = countries;
            Country.DataBind();
        }

        /// <summary>
        /// Event that triggers when a new user account has been created.
        /// We will add them to the 'Patients' role programatically here.
        /// Accounts for all other roles are to be done by the 'Administrator' role.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void RegisterButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            if (String.IsNullOrWhiteSpace(Prefix.Text.Trim()))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("Please specify the salutation of the user.");
                return;
            }

            if (String.IsNullOrWhiteSpace(Country.Text.Trim()))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("Please specify the country of residence of the user.");
                return;
            }

            /*
             * At this point, all user entered information has been verified.
             * We shall now perform two critical actions:
             * 1) Programmatically add account information to the Membership provider
             * 2) Programmatically insert personal particulars into the associated table.
             * 3) Programmatically add the newly created user to the 'Patient' role.
             */

            // Reference to title case converter
            var text = new CultureInfo("en-SG").TextInfo;
            
            // Fetch information that is needed for creating a new account
            var username = HttpUtility.HtmlEncode(UserName.Text.Trim());
            var password = HttpUtility.HtmlEncode(Password.Text.Trim());
            var email = HttpUtility.HtmlEncode(Email.Text.Trim().ToLowerInvariant());
            var question = text.ToTitleCase(HttpUtility.HtmlEncode(Question.Text.Trim()));
            var answer = HttpUtility.HtmlEncode(Answer.Text.Trim().ToLowerInvariant());

            // Create new account in Membership
            MembershipCreateStatus status;
            var user = DatabaseHandler.CreateUser(username, password, email, question, answer, IsApproved, out status);

            // Return and show error message if account creation unsuccessful
            if (user == null)
            {
                switch (status)
                {
                    case MembershipCreateStatus.DuplicateUserName:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("Username already exists. Please enter a different user name.");
                        break;

                    case MembershipCreateStatus.DuplicateEmail:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("A username for that e-mail address already exists. Please enter a different e-mail address.");
                        break;

                    case MembershipCreateStatus.InvalidPassword:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("The password provided is invalid. Please enter a valid password value.");
                        break;

                    case MembershipCreateStatus.InvalidEmail:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("The e-mail address provided is invalid. Please check the value and try again.");
                        break;

                    case MembershipCreateStatus.InvalidAnswer:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("The password retrieval answer provided is invalid. Please check the value and try again.");
                        break;

                    case MembershipCreateStatus.InvalidQuestion:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("The password retrieval question provided is invalid. Please check the value and try again.");
                        break;

                    case MembershipCreateStatus.InvalidUserName:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("The user name provided is invalid. Please check the value and try again.");
                        break;

                    case MembershipCreateStatus.ProviderError:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact the system administrator.");
                        break;

                    case MembershipCreateStatus.UserRejected:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact the system administrator.");
                        break;

                    default:
                        ErrorMessage.Text = HttpUtility.HtmlDecode("An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact the system administrator.");
                        break;
                }
                return;
            }
            // Fetch information that is needed for storing personal information
            var nric = HttpUtility.HtmlEncode(NRIC.Text.Trim().ToUpperInvariant());
            var firstName = text.ToTitleCase(HttpUtility.HtmlEncode(FirstName.Text.Trim()));
            string middleName = null;
            if (!String.IsNullOrEmpty(MiddleName.Text))
                text.ToTitleCase(middleName = HttpUtility.HtmlEncode(MiddleName.Text.Trim()));
            var lastName = text.ToTitleCase(HttpUtility.HtmlEncode(LastName.Text.Trim()));
            var gender = text.ToTitleCase(HttpUtility.HtmlEncode(Gender.SelectedValue.Trim()));
            var namePrefix = text.ToTitleCase(HttpUtility.HtmlEncode(Prefix.Text.Trim()));
            string nameSuffix = null;
            if (!String.IsNullOrEmpty(Suffix.Text))
                nameSuffix = text.ToTitleCase(HttpUtility.HtmlEncode(Suffix.Text.Trim()));
            var dob = DateTime.Parse(HttpUtility.HtmlEncode(DateOfBirth.Text.Trim()));
            var address = text.ToTitleCase(HttpUtility.HtmlEncode(Address.Text.Trim()));
            var contact = HttpUtility.HtmlEncode(ContactNumber.Text.Trim());
            var postalCode = HttpUtility.HtmlEncode(PostalCode.Text.Trim());
            var nationality = text.ToTitleCase(HttpUtility.HtmlEncode(Nationality.Text.Trim()));
            var countryId = DatabaseHandler.GetCountryId(HttpUtility.HtmlEncode(Country.Text.Trim()));

            // Add user personal information into the UserParticulars table
            var addStatus = DatabaseHandler.UpdateParticulars(Guid.Parse(user.ProviderUserKey.ToString()), nric, firstName, middleName, lastName, gender, namePrefix, nameSuffix, dob, address, contact, postalCode, countryId, nationality);

            if (!addStatus)
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("An error occured while adding your information. Please contact the system administrator.");
                return;
            }

            // Add user to the patient role then redirect as appropriate
            addStatus = DatabaseHandler.AddUserToRole(username, RoleName);
            if (!addStatus)
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("An error occured while adding your role. Please contact the system administrator.");
                return;
            }

            Response.Redirect(ResolveUrl(SuccessRedirect));
        }

        /// <summary>
        /// Checks whether username already exists
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void UserNameNotExists(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = !DatabaseHandler.UserNameExists(HttpUtility.HtmlEncode(UserName.Text.Trim()));
        }

        /// <summary>
        /// Redirects the user to their role's home page
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
            }
        }
    }
}
