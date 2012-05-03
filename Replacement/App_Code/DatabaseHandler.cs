using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// This class handles database queries on behalf of the entire application.
/// No error checking will be performed at this level, as it is expected for all incoming data from the controllers
/// to be fully checked and desensitized.
/// 
/// This class can be improved by splitting it into various sub-components, each dealing with only one table in the
/// databae.
/// </summary>

public class DatabaseHandler
{
    private const string WorkDirectory = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads\";
    private const string PngExtension = ".png";
    private const string DicomExtension = ".dcm";

    /// <summary>
    /// Creates a new imaging order in the database
    /// </summary>
    /// <param name="desc">A description of the imaging order</param>
    /// <param name="staffId">The physician that referred the patient</param>
    /// <returns>The study ID if the imaging order was created. -1 otherwise.</returns>
    public static int CreateImagingOrder(string desc, Guid staffId)
    {
        // TODO: Change Guid & review method
        var id = -1;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var study = new Study
                {
                    IsCompleted = false,
                    Diagnosis = null,
                    DateStarted = DateTime.Now,
                    Description = desc.ToLowerInvariant(),
                    ReferredBy = staffId
                };
                db.Studies.InsertOnSubmit(study);
                db.SubmitChanges();
                id = study.StudyId;
            }
        }
        catch (InvalidOperationException) { }
        return id;
    }

    /// <summary>
    /// Creates a new series
    /// </summary>
    /// <param name="modId">The modality ID</param>
    /// <param name="studyId">The study ID</param>
    /// <returns>True if the record was created. False otherwise.</returns>
    public static int CreateSeries(int modId, int studyId)
    {
        var created = 0;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var s = new Series
                {
                    ModalityId = modId,
                    StudyId = studyId
                };
                db.Series.InsertOnSubmit(s);
                db.SubmitChanges();
                created = s.SeriesId;
            }
        }
        catch (InvalidOperationException) { }
        return created;
    }

    public static bool SaveImages(string fileNameOnly)
    {
        // TODO: Finish this method
        throw new NotImplementedException();
        var success = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                // Save DICOM
                //var binData = File.ReadAllBytes(WorkDirectory + fileNameOnly + DicomExtension);
                //var dicomFile = new DicomImage
                //                {
                //                    Image = new Binary(binData)
                //                };
                //db.DicomImages.InsertOnSubmit(dicomFile);
                //var dicomUid = dicomFile.DicomUID;
                //// Save PNGs
                //var files = new DirectoryInfo(WorkDirectory).GetFiles("*.png");
                //foreach (var fileInfo in files)
                //{
                //    var binData2 = File.ReadAllBytes(fileInfo.FullName);
                //    var pngFile = new PngImage
                //                      {
                //                          Image = new Binary(binData2)
                //                      };
                //    db.PngImages.InsertOnSubmit(pngFile);
                //}
                //db.SubmitChanges();

                // Delete DICOM
                var files = new DirectoryInfo(WorkDirectory).GetFiles("*.dcm");
                foreach (var fileInfo in files)
                {
                    fileInfo.Delete();
                }
                // Delete PNGs
                files = new DirectoryInfo(WorkDirectory).GetFiles("*.png");
                foreach (var fileInfo in files)
                {
                    fileInfo.Delete();
                }
                success = true;
            }
        }
        catch (Exception) { }
        return success;
    }
    /// <summary>
    /// Checks if a series exists
    /// </summary>
    /// <param name="seriesId">The series id</param>
    /// <returns>True if the series exists. False otherwise</returns>
    public static bool SeriesExists(int seriesId)
    {
        // TODO: Review this method
        var found = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                found = (from d in db.Series
                         where d.SeriesId == seriesId
                         select d).Any();
            }
        }
        catch (ArgumentNullException) { }
        catch (InvalidOperationException) { }
        return found;
    }
}