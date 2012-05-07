using System;
using DB_Handlers;

namespace Common
{
    /// <summary>
    /// Code behind for the ~/Common/SearchStudies.aspx page
    /// </summary>
    public partial class SearchStudies : System.Web.UI.Page
    {
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

            // Make sure we have a return URL and the query string has not been modified
            var returnUrl = Request.QueryString["ReturnUrl"];
            var checksum = Request.QueryString["Checksum"];
            if (String.IsNullOrWhiteSpace(returnUrl))
                TransferToHome(User.Identity.Name);
            if (!CryptoHandler.IsHashValid(checksum, returnUrl))
                Server.Transfer(ResolveUrl(HashFailure));

            // Populate the grid view with all studies in the system
            var list = StudyHandler.GetAllStudies();
            var display = list.Count > 0;
            AllStudies.Visible = display;
            None.Visible = !display;
            if (display)
            {
                AllStudies.DataSource = list;
                AllStudies.DataBind();
            }
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
                    Server.Transfer(ResolveUrl("~/Patient/Default.aspx"));
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