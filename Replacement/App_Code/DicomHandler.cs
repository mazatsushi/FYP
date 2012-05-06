/// <summary>
/// This class handles DICOM files uploaded to the server.
/// Its workflow is documented as follows:
/// 
/// 1) Convert the DICOM to JPEG using a combination of Evil Dicom & ImageResizer
/// 2) Save DICOM file into database filestreaming table (DicomImages)
/// 3) Delete DICOM file
/// 4) Create a new folder with the Series ID in Uploads folder, and move JPEG file there
/// 5) Save the local file link into the database JpegImages table
/// 
/// At any point in the above process, should an exception be thrown, it means that we should abort.
/// </summary>

public class DicomHandler
{
    private const string JpegExt = ".jpg";
    private const string DestDir = @"E:\Temp\Projects\FYP\Replacement\Uploads\";
    private const string DicomExt = ".dcm";
    private const string WorkDir = @"E:\Temp\Projects\FYP\Replacement\Holding\";

    /// <summary>
    /// The main entry point of this class.
    /// </summary>
    /// <param name="filePath">Fully qualified path leading to the file on local file system.</param>
    /// <param name="seriesId">Series ID the DICOM file is associated with.</param>
    /// <param name="staffUsername">Username of the staff member who uploaded the DICOM image.</param>
    /// <returns></returns>
    public static bool Run(string filePath, int seriesId, string staffUsername)
    {
        var success = false;
        return success;
    }
}