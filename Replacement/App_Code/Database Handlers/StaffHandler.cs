using System;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Staff table in database.
    /// </summary>
    public class StaffHandler
    {
        /// <summary>
        /// Adds a staff member.
        /// </summary>
        /// <param name="userId">User Guid.</param>
        /// <param name="department">Department name.</param>
        /// <returns>True if the staff was added. False otherwise.</returns>
        public static bool AddStaff(Guid userId, string department)
        {
            var added = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    db.Staffs.InsertOnSubmit(new Staff
                    {
                        DepartmentId = DepartmentHandler.GetDepartmentId(department),
                        StaffId = userId
                    });
                    db.SubmitChanges();
                }
                added = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return added;
        }
    } 
}