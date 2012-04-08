using System;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/CreateSeriesSuccess.aspx page
    /// </summary>

    public partial class CreateSeriesSuccess : System.Web.UI.Page
    {
        // TODO: Finish this class
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            Status.Text += Request.QueryString["SeriesID"];
        }
    }
}