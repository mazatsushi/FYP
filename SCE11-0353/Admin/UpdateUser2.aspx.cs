using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Admin
{
    /// <summary>
    /// Code behind for the ~/Admin/UpdateUser2.aspx page
    /// </summary>

    public partial class UpdateUser2 : System.Web.UI.Page
    {
        // TODO: Refactor this class
        private string _username;
        private bool[] _existingRoles;
        private bool[] _newRoles;
        private readonly string[] _rolesList = { "Admin", "Patient", "Physician", "Radiologist", "Staff" };

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _username = Request.QueryString["Username"];

            if (String.IsNullOrWhiteSpace(_username))
            {
                Server.Transfer("~/Admin/UpdateUser.aspx");
            }

            if (IsPostBack)
                return;
            /*
         * Pre-select checkboxes based on roles the user is already in.
         * While at it, we save that information for processing later.
         */
            Label.Text = HttpUtility.HtmlDecode("Current role(s) for user: " + _username);
            var allRoles = DatabaseHandler.GetAllRoles();
            Role.DataSource = allRoles;
            Role.DataBind();
            _existingRoles = new bool[allRoles.Count()];
            _newRoles = new bool[allRoles.Count()];
            var userRoles = DatabaseHandler.GetUserRoles(User.Identity.Name);
            var listItems = Role.Items;

            // Save the viewstate in our own boolean array
            foreach (var roleName in userRoles)
            {
                var li = new ListItem(roleName);
                if (!listItems.Contains(li))
                {
                    _existingRoles[listItems.IndexOf(li)] = false;
                    continue;
                }
                listItems.FindByText(roleName).Selected = true;
                _existingRoles[listItems.IndexOf(li)] = true;
            }

            /*
         * Information is saved as session state on server side for increased security.
         * It comes at the expense of memory space.
         */
            Session["existingRoles"] = _existingRoles;
        }

        /// <summary>
        /// Event handler for when the update button in this page is clicked
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void UpdateButtonClick(object sender, EventArgs e)
        {
            _existingRoles = (bool[])(Session["existingRoles"]);
            _newRoles = new bool[_existingRoles.Length];
            var listItems = Role.Items;
            foreach (ListItem listItem in listItems)
            {
                if (!listItem.Selected)
                {
                    _newRoles[listItems.IndexOf(listItem)] = false;
                    continue;
                }
                _newRoles[listItems.IndexOf(listItem)] = true;
            }

            /*
         * Now compare both boolean arrays.
         * We are only interested in values that have changed since the previous postback.
         */
            for (var i = 0; i < _existingRoles.Length; ++i)
            {
                if (_existingRoles[i] == _newRoles[i])
                    continue;
                /*
             * At this stage, we are only concerned with the boolean values in the second array.
             * If changed value is false, implies that we should remove user from role.
             * If changed value is true, implies that we should add user to role.
             */
                var affectedRole = _rolesList[i];
                if (_newRoles[i])
                {
                    if (!DatabaseHandler.AddUserToRole(_username, affectedRole))
                    {
                        ErrorMessage.Text = HttpUtility.HtmlDecode("There was an error updating the user role(s). Please contact the system administrator.");
                    }
                }
                else
                {
                    if (DatabaseHandler.RemoveUserFromRole(_username, affectedRole))
                    {
                        ErrorMessage.Text = HttpUtility.HtmlDecode("There was an error updating the user role(s). Please contact the system administrator.");
                    }
                }
            }

            Server.Transfer("~/Admin/UpdateUserSuccess.aspx");
        }
    }
}