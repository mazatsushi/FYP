using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Country table in database.
    /// </summary>
    public class CountryHandler
    {
        /// <summary>
        /// Gets a list of all countries.
        /// </summary>
        /// <returns>A string array containing the names of all countries stored in the database.</returns>
        public static IList<string> GetAllCountries()
        {
            var list = new List<string>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list = (from c in db.Countries
                            orderby c.CountryName ascending
                            select c.CountryName).ToList();
                }
            }
            catch (InvalidOperationException) { }
            return list;
        }

        /// <summary>
        /// Gets a country name given its ID.
        /// </summary>
        /// <param name="id">The country ID.</param>
        /// <returns>A string containing the country name. Null otherwise.</returns>
        public static string GetCountryName(int id)
        {
            var countryName = string.Empty;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    countryName = (db.Countries.Single(c => c.CountryId == id).CountryName);
                }
            }
            catch(ArgumentException) { }
            catch (InvalidOperationException) { }
            return countryName;
        }

        /// <summary>
        /// Gets a country's ID given its name.
        /// </summary>
        /// <param name="countryName">The country name.</param>
        /// <returns>A numeric value representing the country name.</returns>
        public static int GetCountryId(string countryName)
        {
            var id = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    id = (db.Countries.Single(c => c.CountryName.Equals(new CultureInfo("en-SG").TextInfo.ToTitleCase(countryName))).CountryId);
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return id;
        }
    }
}