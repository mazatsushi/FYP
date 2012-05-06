using System;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/ImageAdded.aspx page
    /// </summary>
    public partial class ImageAdded : System.Web.UI.Page
    {
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BackButton.PostBackUrl = ResolveUrl(Request.QueryString["ReturnUrl"] + "?SeriesId=" + Request.QueryString["SeriesId"] +
                "&Checksum=" + Request.QueryString["Checksum"]);
        }

        /// <summary>
        /// Event that triggers when the return button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void ResetButtonClick(object sender, EventArgs e)
        {
            Session.Clear();
        }
    }
}