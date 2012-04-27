using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Common
{
    /// <summary>
    /// Code behind for the ~/Common/SearchByNric.aspx page
    /// </summary>
    public partial class SearchByNric : System.Web.UI.Page
    {
        /// <summary>
        /// Checks whether NRIC already exists
        /// </summary>
        /// <param name="source">The web element that triggered the event</param>
        /// <param name="args">Event parameters</param>
        protected void NricExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = DatabaseHandler.NricExists(HttpUtility.HtmlEncode(Nric.Text.Trim().ToUpperInvariant()));
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

            // Do not allow users to directly access this page, due to potentially sensitive information
            if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                TransferToHome(User.Identity.Name);
        }

        /// <summary>
        /// Event that triggers when the search button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void SearchButtonClick(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            var nric = HttpUtility.HtmlEncode(Nric.Text.Trim().ToUpperInvariant());
            Response.Redirect("~/Common/NricFound.aspx?ReturnUrl=" + Request.QueryString["ReturnUrl"] +
                "&Nric=" + nric + "&Checksum=" + CryptoHandler.GetHash(nric));
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
                    Server.Transfer("~/Admin/Default.aspx");
                    break;
                case 1:
                    Server.Transfer("~/Patient/Default.aspx");
                    break;
                case 2:
                    Server.Transfer("~/Physician/Default.aspx");
                    break;
                case 3:
                    Server.Transfer("~/Radiologist/Default.aspx");
                    break;
            }
        }
    }
}