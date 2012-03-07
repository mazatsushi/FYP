using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

/// <summary>
/// Code behind for the ~/Account/UpdateParticulars.aspx page
/// </summary>
public partial class Account_UpdateParticulars : System.Web.UI.Page
{
    private MembershipUser _user;

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        DateRangeCheck.MinimumValue = "1/1/1900";
        DateRangeCheck.MaximumValue = DateTime.Today.ToShortDateString();

        // Get the user name, and fill in the fields as required.
        _user = DatabaseHandler.GetUser(User.Identity.Name);
        Email.Text = _user.Email;
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

    // <summary>
    /// Method to check whether user email already exists
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="args">Event parameters</param>
    protected void EmailNotInUse(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = !DatabaseHandler.EmailInUse(HttpUtility.HtmlEncode(Email.Text.Trim().ToLowerInvariant()));
    }
    
    /// <summary>
    /// Event that triggers when a new user account has been created.
    /// We will add them to the 'Patients' role programatically here.
    /// Accounts for all other roles are to be done by the 'Administrator' role.
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void UpdateButton_Click(object sender, EventArgs e)
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

        // TODO: Membership takes in the UserParticulars 
        // Fetch information that is needed for creating a new account
        var email = Email.Text.Trim().ToLowerInvariant();
        var question = Question.Text.Trim();
        var answer = Answer.Text.Trim().ToLowerInvariant();

        // Create new account in Membership
        MembershipCreateStatus status;
        // TODO: Change the line of code below
        var user = DatabaseHandler.CreateUser(username, password, email, question, answer, IsApproved, out status);
    }
}