using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Admin
{
    /// <summary>
    /// Code behind for the ~/Admin/ManagerUser.aspx page
    /// </summary>

    public partial class ManageUser : System.Web.UI.Page
    {
        private const string FailureRedirect = "~/Common/SearchByNric.aspx";
        private const string HashFailure = "~/Error/HashFailure.aspx";
        private const string SuccessRedirect = "~/Common/NricFound.aspx";

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            // We must have a NRIC to work with
            var nric = Request.QueryString["Nric"];
            if (String.IsNullOrWhiteSpace(nric))
                Server.Transfer(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" + CryptoHandler.GetHash(Request.Url.ToString()));

            // Ensure query string has not been illegaly modified
            var checksum = Request.QueryString["Checksum"];
            if (String.IsNullOrWhiteSpace(checksum) || !CryptoHandler.IsHashValid(checksum, nric))
                Server.Transfer(HashFailure);
            
            // Get the patient's user name in system and save to session state
            var targetUserName = DatabaseHandler.GetUserNameFromNric(nric);
            Session["TargetUserName"] = targetUserName;

            // Let administrator know which user is currently being referenced
            Label.Text = HttpUtility.HtmlDecode("Current role(s) for: " + nric);

            // Get all the roles existing within system and display them
            Roles.DataSource = DatabaseHandler.GetAllRoles();
            Roles.DataBind();

            // Get all existing role(s) user is assigned to and display them
            var roleList = Roles.Items;
            foreach (var userRole in DatabaseHandler.GetUserRoles(targetUserName))
                roleList.FindByText(userRole).Selected = true;
        }

        /// <summary>
        /// Event handler for when the update button in this page is clicked
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void UpdateButtonClick(object sender, EventArgs e)
        {
            /*
             * Step 1: Remove user from all existing roles
             * Step 2: Add user to selected roles
             */

            // Step 1
            var targetUserName = Session["TargetUserName"].ToString();
            foreach (var roleName in DatabaseHandler.GetUserRoles(targetUserName))
                DatabaseHandler.RemoveUserFromRole(targetUserName, roleName);

            // Step 2
            foreach (var role in Roles.Items.Cast<ListItem>().Where(role => role.Selected))
            {
                DatabaseHandler.AddUserToRole(targetUserName, role.ToString());
            }
            Server.Transfer(SuccessRedirect);
        }
    }
}