using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DB_Handlers;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/ManageSeries.aspx page
    /// </summary>
    public partial class ManageSeries : System.Web.UI.Page
    {
        private const string Beginning = "~/Common/SearchByNric.aspx";
        private const string DestDir = @"~\Uploads\";
        private const string DicomFileExt = ".dcm";
        private const string HashFailure = "~/Error/HashFailure.aspx";
        private const string HoldingArea = @"~\TempUploads";
        private const string Home = @"~/Radiologist/ManagePatient.aspx";
        private const string ResizeString = @"?maxwidth=200&maxheight=200";
        private const string SuccessRedirect = @"~/Radiologist/ImageAdded.aspx";
        private const string ThisPage = @"~/Radiologist/ManageSeries.aspx";

        /// <summary>
        /// Event that triggers when the DICOM file has been uploaded.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void FileUploaded(object sender, EventArgs e)
        {
            var fileName = string.Empty;
            // Only 1 file can be uploaded at a time so don't worry
            foreach (var file in Uploader.PostedFiles)
            {
                fileName = Path.Combine(Server.MapPath(HoldingArea), Guid.NewGuid() + DicomFileExt);
                File.Move(file.TempFileName, fileName);
            }

            // Handover to specialized class for conversion and other necessary work
            if (!DicomHandler.Convert(fileName, DestDir, Server.MapPath(DestDir), int.Parse(Request.QueryString["SeriesId"]), User.Identity.Name))
            {
                ErrorMessage.Text = HttpUtility.HtmlDecode("The DICOM file uploaded is incompatible with the system. " +
                                                           "Please contact the administrator for assistance.");
                return;
            }
            Response.Redirect(ResolveUrl(SuccessRedirect + "?ReturnUrl=" + ThisPage + "&SeriesId=" + Request.QueryString["SeriesId"] +
                "&Checksum=" + CryptoHandler.GetHash(Request.QueryString["SeriesId"])));
        }

        /// <summary>
        /// Method for initializing the various data controls in the page on first load
        /// </summary>
        private void Initialize(int seriesId)
        {
            NewButton.PostBackUrl = ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home));
            SeriesID.Text = seriesId.ToString(CultureInfo.InvariantCulture);

            // Hide/display the text label
            var hasImages = ImageHandler.HasImages(seriesId);
            None.Visible = !hasImages;

            // Code to fetch thumbnails and display in page
            if (!hasImages)
                return;

            foreach (var link in ImageHandler.GetLinkedImageId(seriesId).SelectMany(JpegImageHandler.GetLinks).ToList())
            {
                // Build a hyperlink
                var hyperlink = new HyperLink
                {
                    CssClass = "Thumbnail",
                    ImageUrl = ResolveUrl(link + ResizeString),
                    NavigateUrl = ResolveUrl(link),
                };

                // Add panel to container
                Images.Controls.Add(hyperlink);
            }
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

            // Must have patient NRIC
            if (Session["Nric"] == null)
                Server.Transfer(ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home)));

            // Must have Series ID and return URL
            if (String.IsNullOrEmpty(Request.QueryString["SeriesId"]) || String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                Server.Transfer(ResolveUrl(Home));

            // Make sure query string has not been ilegally modified
            if (!CryptoHandler.IsHashValid(Request.QueryString["Checksum"], Request.QueryString["SeriesId"]))
                Server.Transfer(ResolveUrl(HashFailure));

            // Make sure series ID exists
            var seriesId = int.Parse(Request.QueryString["SeriesId"]);
            if (!SeriesHandler.SeriesExists(seriesId))
                Server.Transfer(ResolveUrl(HashFailure));

            Initialize(seriesId);
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
        /// Event that triggers when the start over button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void StartOver(object sender, EventArgs e)
        {
            Session.Clear();
            Server.Transfer(ResolveUrl(Beginning + "?ReturnUrl=" + Home + "&Checksum=" + CryptoHandler.GetHash(Home)));
        }
    }
}