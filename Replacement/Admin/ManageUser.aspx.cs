using System;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

namespace Admin
{
    /// <summary>
    /// Code behind for the ~/Admin/ManagerUser.aspx page
    /// </summary>

    public partial class ManageUser : System.Web.UI.Page
    {
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
            if (String.IsNullOrEmpty(nric))
                Server.Transfer("~/Common/SearchByNric.aspx?ReturnUrl=" + HttpUtility.HtmlEncode(Request.Url));

            // Check that the query string has not been tampered with
            var checkSum = Request.QueryString["Checksum"];
            if (String.IsNullOrEmpty(checkSum))
                Server.Transfer("~/Error/HashFailure.aspx");
            if (!CryptoHandler.CheckHash(nric, checkSum))
                Server.Transfer("~/Error/HashFailure.aspx");

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
            foreach (ListItem role in Roles.Items)
            {
                if (role.Selected)
                    DatabaseHandler.AddUserToRole(targetUserName, role.ToString());
            }
            Server.Transfer("~/Admin/ManageUserSuccess.aspx");
        }
    }
}