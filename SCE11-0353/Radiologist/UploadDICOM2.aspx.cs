using System;

namespace Radiologist
{
    /// <summary>
    /// Code behind for the ~/Radiologist/UploadDicom2.aspx page
    /// </summary>

    public partial class UploadDicom2 : System.Web.UI.Page
    {
        // TODO: Rewrite this class to fit in with its predecessor (most probably removing)
        private const string Redirect = "~/Radiologist/UploadDicomSuccess.aspx";

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var seriesId = Request.QueryString["SeriesID"];
            if (string.IsNullOrEmpty(seriesId))
                Response.Redirect("~/Radiologist/UploadDicom.aspx");

            Session["SeriesID"] = seriesId;
        }

        /// <summary>
        /// Event that triggers when the upload button is clicked.
        /// </summary>
        /// <param name="sender">The web element that triggered the event</param>
        /// <param name="e">Event parameters</param>
        protected void UploadButtonClick(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile)
            {
                ErrorMessage.Text = "Please select a file to upload.";
                return;
            }

            if (!DicomHandler.IsDicomFile(FileUpload.PostedFile))
            {
                ErrorMessage.Text = "Please select a DICOM file to upload.";
                return;
            }

            // Save the DICOM file in file system
            var dcmFileName = FileUpload.FileName;
            FileUpload.SaveAs(@"E:\Temp\Projects\FYP\SCE11-0353\Uploads\" + dcmFileName);

            string fileNameOnly;
            if (!DicomHandler.Convert(dcmFileName, out fileNameOnly))
            {
                ErrorMessage.Text = "The uploaded DICOM file could not be processed.";
                return;
            }

            // Now save it in the database
            if (!DatabaseHandler.SaveImages(fileNameOnly))
            {
                ErrorMessage.Text = "An error occured while saving the images to database.";
                return;
            }
        }
    }
}