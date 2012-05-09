using System;
using System.Web;
using System.Web.UI.WebControls;
using DB_Handlers;

namespace Common
{
    /// <summary>
    /// Code behind for the ~/Common/SearchByNric.aspx page
    /// </summary>
    public partial class SearchByNric : System.Web.UI.Page
    {
        private const string HashFailure = "~/Error/HashFailure.aspx";
        private const string SuccessRedirect = "~/Common/NricFound.aspx";

        /// <summary>
        /// Checks whether NRIC already exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void NricExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = UserParticularsHandler.NricExists(Nric.Text.Trim().ToUpperInvariant());
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

            // Make sure query string has not been illegaly modified
            var checksum = Request.QueryString["Checksum"];
            var returnUrl = Request.QueryString["ReturnUrl"];
            if (String.IsNullOrWhiteSpace(checksum) || String.IsNullOrWhiteSpace(returnUrl))
                TransferToHome(User.Identity.Name);
            if (!CryptoHandler.IsHashValid(checksum, returnUrl))
                Server.Transfer(ResolveUrl(HashFailure));
        }

        /// <summary>
        /// Event that triggers when the search button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void SearchButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var nric = HttpUtility.HtmlEncode(Nric.Text.Trim()).ToUpperInvariant();
            var returnUrl = Request.QueryString["ReturnUrl"];
            Response.Redirect(ResolveUrl(SuccessRedirect + "?ReturnUrl=" + returnUrl + "&Nric=" + nric + "&Checksum=" +
                CryptoHandler.GetHash(returnUrl, nric)));
        }

        /// <summary>
        /// Redirects the user to their role's home page
        /// </summary>
        /// <param name="username">The user name</param>
        private void TransferToHome(string username)
        {
            switch (MembershipHandler.FindMostPrivilegedRole(username))
            {
                case 0:
                    Server.Transfer(ResolveUrl("~/Admin/Default.aspx"));
                    break;
                case 1:
                    Server.Transfer(ResolveUrl("~/Patients/Default.aspx"));
                    break;
                case 2:
                    Server.Transfer(ResolveUrl("~/Physician/Default.aspx"));
                    break;
                case 3:
                    Server.Transfer(ResolveUrl("~/Radiologist/Default.aspx"));
                    break;
            }
        }
    }
}