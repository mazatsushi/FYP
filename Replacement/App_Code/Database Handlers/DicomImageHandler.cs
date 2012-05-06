using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the DicomImages table in database.
    /// </summary>
    public class DicomImageHandler
    {
        /// <summary>
        /// Saves a DICOM file to the database.
        /// </summary>
        /// <param name="imageId">Image ID.</param>
        /// <param name="data">A byte array containing the raw data of the DICOM file.</param>
        /// <returns>True if save is successful. False otherwise.</returns>
        public static bool Save(int imageId, Byte[] data)
        {
            var saved = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    db.DicomImages.InsertOnSubmit(new DicomImage
                    {
                        ImageId = imageId,
                        DicomImage1 = data
                    });
                    db.SubmitChanges();
                }
                saved = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return saved;
        }
    }
}