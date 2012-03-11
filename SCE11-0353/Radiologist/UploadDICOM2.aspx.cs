using System;

public partial class Radiologist_UploadDICOM2 : System.Web.UI.Page
{
    private const string RedirectBack = "~/Radiologist/UploadDICOM.aspx";
    private const string Redirect = "~/Radiologist/UploadDICOMSuccess.aspx";
    private const string WorkDirectory = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads\";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        var seriesId = Request.QueryString["SeriesID"];
        if (string.IsNullOrEmpty(seriesId))
            Response.Redirect(RedirectBack);

        Session["SeriesID"] = seriesId;
    }

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
        FileUpload.SaveAs(WorkDirectory + dcmFileName);

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