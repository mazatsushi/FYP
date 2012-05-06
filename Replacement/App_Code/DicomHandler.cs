using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DB_Handlers;
using EvilDicom.Image;
using ImageResizer;

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
    private const string ConvertSettings = "maxwidth=600&maxheight=600";
    private const string JpegExt = ".jpg";
    private const string DestDir = @"E:\Temp\Projects\FYP\Replacement\Uploads\";

    /// <summary>
    /// The main entry point of this class.
    /// </summary>
    /// <param name="filePath">Fully qualified path leading to the uploaded DICOM file on hard disk.</param>
    /// <param name="seriesId">Series ID the DICOM file is associated with.</param>
    /// <param name="staffUsername">Username of the staff member who uploaded the DICOM image.</param>
    /// <returns></returns>
    public static bool Convert(string filePath, int seriesId, string staffUsername)
    {
        var success = false;

        // Create the directory where converted files will reside
        var folder = new DirectoryInfo(DestDir + seriesId);
        folder.Create();

        try
        {
            // Open the DICOM file with Evil Dicom
            var matrix = new ImageMatrix(filePath);
            var list = new List<string>();

            // There might be more than one frame in a DICOM file; convert all of them
            for (var i = 0; i <= matrix.Properties.NumberOfFrames; i++)
            {
                var destination = folder + @"\" + Guid.NewGuid() + JpegExt;
                ImageBuilder.Current.Build(matrix.GetImage(i), destination, new ResizeSettings(ConvertSettings));
                list.Add(destination);
            }
            /*
             * At this point it is confirmed that the DICOM file is not giving us any problems.
             * Next step is to save all converted JPEG file path(s) to SQL Server.
             * There are no guarantees that the rendering is correct.
             */
            var imageId = ImageHandler.CreateImage(seriesId, staffUsername);
            if (imageId == -1)
                throw new Exception();
            if (list.Any(fileUri => !JpegImageHandler.Save(imageId, fileUri)))
                throw new Exception();

            // Save the DICOM file to database filestream and everything is done
            matrix = null;
            if (!DicomImageHandler.Save(imageId, File.ReadAllBytes(filePath)))
                throw new Exception();
            success = true;
        }
        catch (Exception)
        {
            // Abort everything should there be any exception
            folder.Delete(true);
        }
        finally
        {
            // DICOM file is now useless
            File.Delete(filePath);
        }
        return success;
    }
}