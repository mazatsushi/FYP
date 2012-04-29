using System;
using System.Web;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/ManagePatient.aspx page
    /// </summary>
    public partial class ManagePatient : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Common/SearchByNric.aspx";
        private const string HashFailure = "~/Error/HashFailure.aspx";

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            // We need patient's NRIC to be able to display data and prompt for actions
            var nric = Request.QueryString["Nric"];
            if (String.IsNullOrWhiteSpace(nric))
                Server.Transfer(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" + CryptoHandler.GetHash(Request.Url.ToString()));

            // Ensure query string has not been illegaly modified
            var checksum = Request.QueryString["Checksum"];
            if (String.IsNullOrWhiteSpace(checksum) || !CryptoHandler.IsHashValid(checksum, nric))
                Server.Transfer(HashFailure);
            
            // Let physician know which patient is currently being referenced
            Label.Text = HttpUtility.HtmlDecode("Current role(s) for: " + nric);

            // Display patient's medical data
        }
    } 
}