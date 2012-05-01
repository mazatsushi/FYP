using System;

namespace Physician
{
    /// <summary>
    /// Code behind for the ~/Physician/NoBlood.aspx page
    /// </summary>
    public partial class NoBlood : System.Web.UI.Page
    {
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event that triggers when the return button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ResetButtonClick(object sender, EventArgs e)
        {
            Session["Allergies"] = null;
            Session["Nric"] = null;
        }
    } 
}