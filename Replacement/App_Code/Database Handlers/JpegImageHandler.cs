using System;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the JpegImages table in database.
    /// </summary>
    public class JpegImageHandler
    {
        /// <summary>
        /// Saves a JPEG file to the database using its local file system URI.
        /// </summary>
        /// <param name="imageId">Series ID.</param>
        /// <param name="uri">Local file system URI.</param>
        /// <returns>True if save is successful. -1 otherwise.</returns>
        public static bool Save(int imageId, string uri)
        {
            var saved = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    db.JpegImages.InsertOnSubmit(new JpegImage
                    {
                        ImageID = imageId,
                        Link = uri
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