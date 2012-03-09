using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


/// <summary>
/// Code behind for the ~/Admin/AddUser.aspx page
/// 
/// Note:
/// 1) All user input are first desensitized by calling the HttpUtility.HTMLEncode() method.
/// For more information, please refer to: http://msdn.microsoft.com/en-us/library/73z22y6h.aspx
/// 
/// 2) We let the compiler determine at run-time the data type of local variables.
/// Hence the use of the new C# keyword 'var'. For more information, please refer to:
/// http://msdn.microsoft.com/en-us/library/bb384061.aspx
/// </summary>
public partial class Admin_AddUser : System.Web.UI.Page
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        DateRangeCheck.MinimumValue = "1/1/1900";
        DateRangeCheck.MaximumValue = DateTime.Today.ToShortDateString();
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

    protected void RegisterButtonClick(object sender, EventArgs e)
    {
    }
}