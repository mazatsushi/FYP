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
    } 
}