using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Images table in database.
    /// </summary>
    public class ImageHandler
    {
        /// <summary>
        /// Checks whether a series has any images.
        /// </summary>
        /// <param name="seriesId">Series ID.</param>
        /// <returns>True if images are found. False otherwise.</returns>
        public static bool HasImages(int seriesId)
        {
            var exist = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    exist = (db.Images.Any(s => s.SeriesId == seriesId));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return exist;
        }

        /// <summary>
        /// Saves a JPEG file to the database using its local file system URI.
        /// </summary>
        /// <param name="seriesId">Series ID.</param>
        /// <param name="username">Staff username.</param>
        /// <returns>The Image ID if save is successful. -1 otherwise.</returns>
        public static int CreateImage(int seriesId, string username)
        {
            var saved = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var image = new Image
                    {
                        SeriesId = seriesId,
                        TakenBy = MembershipHandler.GetGuidFromUserName(username),
                    };
                    db.Images.InsertOnSubmit(image);
                    db.SubmitChanges();
                    saved = image.ImageId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return saved;
        }

        /// <summary>
        /// Gets the image IDs linked to a series ID.
        /// </summary>
        /// <param name="seriesId">Series ID.</param>
        /// <returns>The Image ID if save is successful. -1 otherwise.</returns>
        public static IList<int> GetLinkedImageId(int seriesId)
        {
            var saved = new List<int>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    saved = (from i in db.Images
                         where i.SeriesId == seriesId
                         orderby i.SeriesId
                         select i.ImageId).ToList();
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return saved;
        }
    }
}