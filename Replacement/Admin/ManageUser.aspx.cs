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
        private const string SuccessRedirect = "~/Common/NricFound.aspx";

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize()
        {
            // Get the patient's user name in system and save to session state
            var nric = Session["Nric"].ToString();
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
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            // We must have a NRIC to work with
            if (Session["Nric"] == null)
                Server.Transfer(ResolveUrl(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                    CryptoHandler.GetHash(Request.Url.ToString())));

            Initialize();
        }

        /// <summary>
        /// Event handler for when the new user button in this page is clicked
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void NewUserButtonClick(object sender, EventArgs e)
        {
            Session["TargetUserName"] = null;
            Session["Nric"] = null;
            Server.Transfer(ResolveUrl(FailureRedirect + "?ReturnUrl=" + Request.Url + "&Checksum=" +
                CryptoHandler.GetHash(Request.Url.ToString())));
        }

        /// <summary>
        /// Event handler for when the update button in this page is clicked
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void UpdateButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

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
            Session["TargetUserName"] = null;
            Server.Transfer(ResolveUrl(SuccessRedirect));
        }
    }
}