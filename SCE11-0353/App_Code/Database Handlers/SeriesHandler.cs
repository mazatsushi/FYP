using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Series table in database.
    /// </summary>
    public class SeriesHandler
    {
        /// <summary>
        /// Creates a new series.
        /// </summary>
        /// <param name="modality">Modality name.</param>
        /// <param name="studyId">Study ID.</param>
        /// <returns>The ID of the newly created series if successful. -1 otherwise.</returns>
        public static int CreateSeries(string modality, int studyId)
        {
            var seriesId = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var s = new Series
                    {
                        ModalityId = ModalityHandler.GetModalityId(modality.Trim()),
                        StudyId = studyId,
                    };
                    db.Series.InsertOnSubmit(s);
                    db.SubmitChanges();
                    seriesId = s.SeriesId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return seriesId;
        }

        /// <summary>
        /// Gets all series associated with the study ID.
        /// </summary>
        /// <param name="studyId">Study ID.</param>
        /// <returns>A list of all series associated with the study ID if found.</returns>
        public static IList<Series> GetAllSeries(int studyId)
        {
            var list = new List<Series>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list = (from s in db.Series
                            where s.StudyId == studyId
                            orderby s.StudyId
                            select s).ToList();
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return list;
        }

        /// <summary>
        /// Checks whether a series exists.
        /// </summary>
        /// <param name="seriesId">Series ID.</param>
        /// <returns>True if series is found. False otherwise.</returns>
        public static bool SeriesExists(int seriesId)
        {
            var exist = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    exist = (db.Series.Any(s => s.SeriesId == seriesId));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return exist;
        }
    }
}