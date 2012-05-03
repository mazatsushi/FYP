using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Department table in database.
    /// </summary>
    public class DepartmentHandler
    {
        /// <summary>
        /// Gets all departments.
        /// </summary>
        /// <returns>A list containing the names of all departments.</returns>
        public static IList<string> GetAllDepartments()
        {
            var list = new List<string>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list = (from d in db.Departments
                            orderby d.DepartmentName ascending
                            select d.DepartmentName).ToList();
                }
            }
            catch (InvalidOperationException) { }
            return list;
        }

        /// <summary>
        /// Gets the ID of a department using its name.
        /// </summary>
        /// <param name="department">Department name.</param>
        /// <returns>The department ID if found.</returns>
        public static int GetDepartmentId(string department)
        {
            var id = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    id = db.Departments.Single(d => d.DepartmentName.Equals(new CultureInfo("en-SG").TextInfo.ToTitleCase(department))).DepartmentId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return id;
        }
    }
}