using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

/// <summary>
/// Code behind for the ~/Account/UpdateParticulars.aspx page
/// </summary>
public partial class Account_UpdateParticulars : System.Web.UI.Page
{
    private MembershipUser _account;
    private const string RedirectUrl = "~/Account/UpdateParticularsSuccess.aspx";

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Fill in account information fields
        _account = DatabaseHandler.GetUser(User.Identity.Name);
        Email.Text = _account.Email;

        // Fill in personal information fields
        var particulars = DatabaseHandler.GetUserParticulars(_account.ProviderUserKey.ToString());
        if (null == particulars)
            return;

        FirstName.Text = particulars.FirstName;
        MiddleName.Text = particulars.MiddleName;
        LastName.Text = particulars.LastName;
        Prefix.SelectedValue = particulars.Gender.ToString(CultureInfo.InvariantCulture);
        Suffix.SelectedValue = particulars.Suffix;
        Address.Text = particulars.Address;
        ContactNumber.Text = particulars.ContactNumber;
        PostalCode.Text = particulars.PostalCode;
        Nationality.Text = particulars.Nationality;
        Country.SelectedValue = DatabaseHandler.GetCountryName(particulars.CountryOfResidence);
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
    /// Event that triggers when the update button is clicked.
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
         * 1) Programmatically update account information via Membership
         *  1.1) Note that since we manually checked whether email is unique,
         *  it is 100% guaranteed that it is valid as well.
         * 2) Call DatabaseHandler to handle the updates for us.
         */

        // Fetch account information and update
        var email = Email.Text.Trim().ToLowerInvariant();
        if (String.IsNullOrEmpty(email))
            return;

        _account.Email = email;
        if (DatabaseHandler.UpdateAccount(_account))
        {
            ErrorMessage.Text = HttpUtility.HtmlDecode("An error occured while trying to update your account information");
            return;
        }

        var firstName = FirstName.Text.Trim();
        string middleName = null;
        if (!String.IsNullOrEmpty(MiddleName.Text))
            middleName = MiddleName.Text.Trim();
        var lastName = LastName.Text.Trim();
        var namePrefix = Prefix.Text.Trim();
        string nameSuffix = null;
        if (!String.IsNullOrEmpty(Suffix.Text))
            nameSuffix = Suffix.Text.Trim();
        var address = Address.Text.Trim();
        var contact = ContactNumber.Text.Trim();
        var postalCode = PostalCode.Text.Trim();
        var nationality = Nationality.Text.Trim();
        var countryId = DatabaseHandler.GetCountryId(Country.Text.Trim());

        if (!DatabaseHandler.UpdateParticulars(_account.ProviderUserKey, firstName, middleName, lastName, namePrefix, nameSuffix, address, contact, postalCode, countryId, nationality))
        {
            ErrorMessage.Text += HttpUtility.HtmlDecode("<ul>");
            ErrorMessage.Text += HttpUtility.HtmlDecode("<li>An error occured while adding your role. Please contact the system administrator.</li>");
            ErrorMessage.Text += HttpUtility.HtmlDecode("</ul>");
            return;
        }

        Server.Transfer(RedirectUrl);
    }

    /// <summary>
    /// Event handler for when the Cancel button in this page is clicked
    /// </summary>
    /// <param name="sender">The web element that triggered the event</param>
    /// <param name="e">Event parameters</param>
    protected void CancelButtonClick(object sender, EventArgs e)
    {
        switch (DatabaseHandler.FindMostPrivilegedRole(User.Identity.Name))
        {
            case 0:
                Response.Redirect("~/Admin/Default.aspx");
                break;
            case 1:
                Response.Redirect("~/Physician/Default.aspx");
                break;
            case 2:
                Response.Redirect("~/Radiologist/Default.aspx");
                break;
            case 3:
                Response.Redirect("~/Staff/Default.aspx");
                break;
            case 4:
                Response.Redirect("~/Patient/Default.aspx");
                break;
        }
    }
}