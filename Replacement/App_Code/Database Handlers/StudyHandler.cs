using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Studies table in database.
    /// </summary>
    public class StudyHandler
    {
        /// <summary>
        /// Creates a new study.
        /// </summary>
        /// <param name="desc">Description of the study (purpose etc.)</param>
        /// <param name="start">Date of first appointment to commence study.</param>
        /// <param name="staffUserName">Staff's account username.</param>
        /// <returns>True if the record was created. False otherwise.</returns>
        public static int CreateStudy(string desc, DateTime start, string staffUserName)
        {
            var created = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var s = new Study
                    {
                        Description = desc,
                        DateStarted = start,
                        IsCompleted = false,
                        DateCompleted = null,
                        Diagnosis = null,
                        ReferredBy = MembershipHandler.GetGuidFromUserName(staffUserName)
                    };
                    db.Studies.InsertOnSubmit(s);
                    db.SubmitChanges();
                    created = s.StudyId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return created;
        }

        /// <summary>
        /// Gets all studies in system.
        /// </summary>
        /// <returns>A list of all studies in system.</returns>
        public static IList<Study> GetAllStudies()
        {
            var list = new List<Study>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list = (from s in db.Studies
                            orderby s.StudyId
                            select s).ToList();
                }

            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return list;
        }

        /// <summary>
        /// Gets all open studies of a patient.
        /// </summary>
        /// <param name="nric">Patient NRIC.</param>
        /// <returns>A list of open studies patient is involved in, if any.</returns>
        public static int GetOpenStudy(string nric)
        {
            var temp = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    temp = (from s in db.Studies
                            join a in db.Appointments on s.StudyId equals a.StudyId
                            where (a.PatientId.Equals(UserParticularsHandler.GetGuidFromNric(nric)) && s.IsCompleted == false)
                            orderby s.StudyId
                            select s.StudyId).Single();
                }

            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return temp;
        }

        /// <summary>
        /// Gets all the studies that patient has been involved in.
        /// </summary>
        /// <param name="nric">Patient's NRIC.</param>
        /// <returns>A list of all studies patient is involved in, if any.</returns>
        public static IList<Study> GetStudyHistory(string nric)
        {
            var temp = new List<Study>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    temp = (from s in db.Studies
                            join a in db.Appointments on s.StudyId equals a.StudyId
                            where (a.PatientId.Equals(UserParticularsHandler.GetGuidFromNric(nric)))
                            select s).ToList();
                }

            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return temp;
        }
    }
}