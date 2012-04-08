using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Admin
{
    /// <summary>
    /// Code behind for the ~/Admin/UpdateUser.aspx page
    /// </summary>

    public partial class UpdateUser : System.Web.UI.Page
    {
        // TODO: Refactor this class
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var nric = Request.QueryString["Nric"];
                if (String.IsNullOrWhiteSpace(nric))
                {
                    Server.Transfer("~/Common/SearchByNric.aspx?ReturnUrl=" + Request.Url);
                }
            }
            else
            {
                ;
            }
        }

        /// <summary>
        /// Event handler for when the next button is clicked
        /// </summary>
        /// <param name="sender">The server control that raised the event</param>
        /// <param name="e">The event arguments</param>
        protected void NextButtonClick(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            var username = HttpUtility.HtmlEncode(UserName.Text.Trim());
            Response.Redirect("~/Admin/UpdateUserStep2.aspx?Username=" + username);
        }

        /// <summary>
        /// Determines if the provided user name is currently in use
        /// </summary>
        /// <param name="source">The server control that invoked this method</param>
        /// <param name="args">The arguments</param>
        protected void UserNameExists(object source, ServerValidateEventArgs args)
        {
            args.IsValid = DatabaseHandler.UserNameExists(HttpUtility.HtmlEncode(UserName.Text.Trim()));
        }
    }
}