using System;
using System.Web;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/UploadDicom.aspx page
    /// </summary>

    public partial class UploadDicom : System.Web.UI.Page
    {
        // TODO: Rewrite this class to use the common NRIC search feature
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event that triggers when the retrieve button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void RetrieveButtonClick(object sender, EventArgs e)
        {
            Validate();
            if (!IsValid)
                return;

            var seriesId = 0;
            if (!int.TryParse(HttpUtility.HtmlEncode(SeriesID.Text.Trim()), out seriesId))
            {
                ErrorMessage.Text = "Invalid Series ID.";
                return;
            }

            if (!DatabaseHandler.SeriesExists(seriesId))
            {
                ErrorMessage.Text = "Series ID does not exist.";
                return;
            }

            Response.Redirect("~/Radiologist/UploadDicom2.aspx?SeriesID=" + seriesId);
        }
    }
}