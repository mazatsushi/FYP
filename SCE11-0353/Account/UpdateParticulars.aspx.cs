﻿using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DB_Handlers;

namespace Account
{
    /// <summary>
    /// Code behind for the ~/Account/UpdateParticulars.aspx page
    /// </summary>
    public partial class UpdateParticulars : System.Web.UI.Page
    {
        private const string SuccessRedirect = "~/Account/UpdateParticularsSuccess.aspx";

        /// <summary>
        /// Event handler for when the Cancel button in this page is clicked
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void CancelButtonClick(object sender, EventArgs e)
        {
            switch (MembershipHandler.FindMostPrivilegedRole(User.Identity.Name))
            {
                case 0:
                    Response.Redirect(ResolveUrl("~/Admin/Default.aspx"));
                    break;
                case 1:
                    Response.Redirect(ResolveUrl("~/Patients/Default.aspx"));
                    break;
                case 2:
                    Response.Redirect(ResolveUrl("~/Physician/Default.aspx"));
                    break;
                case 3:
                    Response.Redirect(ResolveUrl("~/Radiologist/Default.aspx"));
                    break;
            }
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
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            // Fill in account information fields
            Email.Text = MembershipHandler.GetUserEmail(User.Identity.Name);

            // Fill in personal information fields
            var particulars = UserParticularsHandler.GetParticularsFromUsername(User.Identity.Name);
            if (null == particulars)
                return;

            // Convenience class for beautifying the output
            var text = new CultureInfo("en-SG").TextInfo;

            FirstName.Text = text.ToTitleCase(particulars.FirstName);
            if (!String.IsNullOrEmpty(particulars.MiddleName))
                MiddleName.Text = text.ToTitleCase(particulars.MiddleName);
            LastName.Text = text.ToTitleCase(particulars.LastName);
            Prefix.SelectedValue = particulars.Prefix.ToString(CultureInfo.InvariantCulture);
            Suffix.SelectedValue = particulars.Suffix;
            Address.Text = text.ToTitleCase(particulars.Address);
            ContactNumber.Text = particulars.ContactNumber;
            PostalCode.Text = particulars.PostalCode;
            Nationality.Text = text.ToTitleCase(particulars.Nationality);

            Country.DataSource = CountryHandler.GetAllCountries();
            Country.DataBind();
            Country.SelectedValue = CountryHandler.GetCountryName(particulars.CountryOfResidence);
        }

        /// <summary>
        /// Event that triggers when the update button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void UpdateButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;
            /*
             * At this point, all user entered information has been verified.
             * We shall now perform two critical actions:
             * 1) Programmatically update account information via Membership
             * 2) Call DatabaseHandler to handle the updates for us.
             */
            // Step 1
            var username = User.Identity.Name;
            var email = HttpUtility.HtmlEncode(Email.Text.Trim().ToLowerInvariant());
            if (String.IsNullOrEmpty(email))
            {
                ErrorMessage.Text += HttpUtility.HtmlDecode("<ul>");
                ErrorMessage.Text = HttpUtility.HtmlDecode("<li>Please provide an email address</li>");
                ErrorMessage.Text += HttpUtility.HtmlDecode("</ul>");
                return;
            }

            if (!MembershipHandler.UpdateAccount(User.Identity.Name, email))
            {
                ErrorMessage.Text += HttpUtility.HtmlDecode("<ul>");
                ErrorMessage.Text = HttpUtility.HtmlDecode("<li>An error occured while trying to update your account information</li>");
                ErrorMessage.Text += HttpUtility.HtmlDecode("</ul>");
                return;
            }

            // Reference to title case converter
            var text = new CultureInfo("en-SG").TextInfo;

            // Step 2
            var firstName = text.ToTitleCase(HttpUtility.HtmlEncode(FirstName.Text.Trim()));
            string middleName = null;
            if (!String.IsNullOrEmpty(MiddleName.Text))
                text.ToTitleCase(middleName = HttpUtility.HtmlEncode(MiddleName.Text.Trim()));
            var lastName = text.ToTitleCase(HttpUtility.HtmlEncode(LastName.Text.Trim()));
            var namePrefix = text.ToTitleCase(HttpUtility.HtmlEncode(Prefix.Text.Trim()));
            string nameSuffix = null;
            if (!String.IsNullOrEmpty(Suffix.Text))
                nameSuffix = text.ToTitleCase(HttpUtility.HtmlEncode(Suffix.Text.Trim()));
            var address = text.ToTitleCase(HttpUtility.HtmlEncode(Address.Text.Trim()));
            var contact = HttpUtility.HtmlEncode(ContactNumber.Text.Trim());
            var postalCode = HttpUtility.HtmlEncode(PostalCode.Text.Trim());
            var nationality = text.ToTitleCase(HttpUtility.HtmlEncode(Nationality.Text.Trim()));
            var countryId = CountryHandler.GetCountryId(HttpUtility.HtmlEncode(Country.Text.Trim()));

            if (!UserParticularsHandler.UpdateParticulars(username, firstName, middleName, lastName, namePrefix, nameSuffix, address, contact, postalCode, countryId, nationality))
            {
                ErrorMessage.Text += HttpUtility.HtmlDecode("<ul>");
                ErrorMessage.Text += HttpUtility.HtmlDecode("<li>An error occured while updating your particulars. Please contact the system administrator.</li>");
                ErrorMessage.Text += HttpUtility.HtmlDecode("</ul>");
                return;
            }
            Response.Redirect(ResolveUrl(SuccessRedirect));
        }
    }
}