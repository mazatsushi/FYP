using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Modalities table in database.
    /// </summary>
    public class ModalityHandler
    {
        /// <summary>
        /// Gets all imaging modalities.
        /// </summary>
        /// <returns>A list containing the names of all imaging modalities.</returns>
        public static IList<string> GetAllModalities()
        {
            var list = new List<string>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    return (from m in db.Modalities
                            orderby m.Description ascending
                            select m.Description).ToList();
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return list;
        }

        /// <summary>
        /// Gets a modality's ID given its name.
        /// </summary>
        /// <param name="desc">Modality name.</param>
        /// <returns>Modality ID</returns>
        public static int GetModalityId(string desc)
        {
            var id = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    id = db.Modalities.Single(b => b.Description.Equals(new CultureInfo("en-SG").TextInfo.ToTitleCase(desc))).ModalityId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return id;
        }

        /// <summary>
        /// Gets a modality's name given its ID.
        /// </summary>
        /// <param name="modalityId">Modality ID.</param>
        /// <returns>Modality name.</returns>
        public static string GetModality(int modalityId)
        {
            var name = string.Empty;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    name = db.Modalities.Single(b => b.ModalityId == modalityId).Description;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return name;
        }
    }
}