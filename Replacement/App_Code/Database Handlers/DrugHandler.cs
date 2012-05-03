using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Drug table in database.
    /// </summary>
    public class DrugHandler
    {
        /// <summary>
        /// Adds a new drug to the database.
        /// </summary>
        /// <param name="drugName">Drug name.</param>
        /// <returns>True if the drug was added. False otherwise.</returns>
        public static bool AddNewDrug(string drugName)
        {
            var added = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    db.DrugAllergies.InsertOnSubmit(new DrugAllergy
                    {
                        DrugName = new CultureInfo("en-SG").TextInfo.ToTitleCase(drugName)
                    });
                    db.SubmitChanges();
                }
                added = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return added;
        }

        /// <summary>
        /// Gets a list of all medical drugs.
        /// </summary>
        /// <returns>A string array containing the names of medical drugs stored in the database.</returns>
        public static IList<string> GetAllDrugs()
        {
            var list = new List<string>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list = (from d in db.DrugAllergies
                            orderby d.DrugName ascending
                            select d.DrugName).ToList();
                }
            }
            catch (InvalidOperationException) { }
            return list;
        }

        /// <summary>
        /// Checks whether a drug already exists in the database.
        /// </summary>
        /// <param name="drugName">Drug name.</param>
        /// <returns>True if the drug name is found in the database. False otherwise.</returns>
        public static bool DrugExists(string drugName)
        {
            var found = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    found = (db.DrugAllergies.Any(d => d.DrugName.Equals(new CultureInfo("en-SG").TextInfo.ToTitleCase(drugName))));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return found;
        }

        /// <summary>
        /// Gets the ID of a drug given its name.
        /// </summary>
        /// <param name="drugName">Drug name.</param>
        /// <returns>Drug ID.</returns>
        public static int GetDrugId(string drugName)
        {
            var id = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    id = (db.DrugAllergies.Single(d => d.DrugName.Equals(new CultureInfo("en-SG").TextInfo.ToTitleCase(drugName))).DrugAllergyId);
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return id;
        }
    }
}