using System;
using System.Web;

namespace Common
{
    /// <summary>
    /// Code behind for the ~/Common/NricFound.aspx page
    /// </summary>
    public partial class NricFound : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Common/SearchByNric.aspx";
        private const string HashFailure = "~/Error/HashFailure.aspx";

        /// <summary>
        /// Event that triggers when the search again button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void CancelButtonClick(object sender, EventArgs e)
        {
            Server.Transfer(FailureRedirect + "?ReturnUrl=" + HttpUtility.HtmlEncode(Request.QueryString["ReturnUrl"]));
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

            // Check that user did not modify the return URL
            var checksum = Request.QueryString["Checksum"];
            var nric = Request.QueryString["Nric"];
            var returnUrl = Request.QueryString["ReturnUrl"];
            if (String.IsNullOrWhiteSpace(returnUrl) || String.IsNullOrWhiteSpace(nric) || String.IsNullOrWhiteSpace(checksum))
                Server.Transfer(FailureRedirect);
            if (!CryptoHandler.IsHashValid(checksum, returnUrl, nric))
                Server.Transfer(HashFailure);
        }

        /// <summary>
        /// Event that triggers when the search button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ProceedButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            Session["Nric"] = Request.QueryString["Nric"];
            Response.Redirect(HttpUtility.HtmlEncode(Request.QueryString["ReturnUrl"]));
        }
    }
}