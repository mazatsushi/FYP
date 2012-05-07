using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using DB_Handlers;

namespace Patients
{
    /// <summary>
    /// Code behind for the ~/Patients/ViewSeries.aspx page
    /// It is assumed that every patient can have at most 1 unresolved study at any time
    /// </summary>
    public partial class ViewSeries : System.Web.UI.Page
    {
        private const string HashFailure = @"~/Error/HashFailure.aspx";
        private const string PreviousRedirect = @"~/Physician/ManageImagingHistory.aspx";
        private const string ResizeString = @"?maxwidth=200&maxheight=200";

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize(int studyId)
        {
            // Code block for enabling/disabling the page as a whole
            var show = StudyHandler.StudyExists(studyId);
            switch (show)
            {
                case true:
                    StudyIdLabel.Text = "Study ID: " + studyId.ToString(CultureInfo.InvariantCulture);
                    Details.Visible = true;
                    StudyDetails.DataSource = StudyHandler.GetStudy(studyId);
                    StudyDetails.DataBind();
                    break;
                case false:
                    StudyIdLabel.Text = "Study ID cannot be found in the system";
                    Details.Visible = false;
                    break;
            }
            ToggleImages(studyId);
            Session["StudyId"] = studyId;
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

            // We need a Study ID before proceeding
            var temp = Request.QueryString["StudyId"];
            var checksum = Request.QueryString["Checksum"];
            if (String.IsNullOrEmpty(temp) || String.IsNullOrEmpty(checksum))
                Server.Transfer(ResolveUrl(PreviousRedirect));

            // Make sure query string has not been ilegally modified
            var studyID = int.Parse(temp);
            if (!CryptoHandler.IsHashValid(checksum, temp) || !StudyHandler.StudyExists(studyID))
                Server.Transfer(HashFailure);

            Initialize(studyID);
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

        /// <summary>
        /// Method for showing/hiding all images associated with this study
        /// </summary>
        private void ToggleImages(int studyId)
        {
            var show = StudyHandler.HasImages(studyId);
            None.Visible = !show;

            var list = (from series in SeriesHandler.GetAllSeries(studyId) from imageId in ImageHandler.GetLinkedImageId(series.SeriesId) from link in JpegImageHandler.GetLinks(imageId) select link).ToList();

            // Code to fetch thumbnails and display in page
            if (list.Count < 1)
                return;

            foreach (var link in list)
            {
                // Build a relative URL for the image (it will be referenced twice)
                var relativeLink = ResolveUrl("~/" + new Uri(new DirectoryInfo(link).Parent.Parent.FullName).MakeRelativeUri(new Uri(link)));

                // Build a panel to hold the image
                var panel = new Panel();

                // Build a hyperlink
                var hyperlink = new HyperLink
                {
                    CssClass = "Thumbnail",
                    ImageUrl = ResolveUrl(relativeLink + ResizeString),
                    NavigateUrl = ResolveUrl(relativeLink),
                };

                // Add image to panel
                panel.Controls.Add(hyperlink);

                // Add panel to container
                ImagesDiv.Controls.Add(panel);
            }
        }
    }
}