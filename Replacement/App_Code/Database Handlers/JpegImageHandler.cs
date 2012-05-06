using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the JpegImages table in database.
    /// </summary>
    public class JpegImageHandler
    {
        /// <summary>
        /// Gets all image URIs associated with the Image ID.
        /// </summary>
        /// <param name="imageId">Series ID.</param>
        /// <returns>A list containing the URIs of all images of Image ID if found.</returns>
        public static IList<string> GetLinks(int imageId)
        {
            var list = new List<string>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list = (from j in db.JpegImages
                            where j.ImageID == imageId
                            orderby j.ImageID
                            select j.Link).ToList();
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return list;
        }

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