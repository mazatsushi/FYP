using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Studies (and a bit of Series) table in database.
    /// </summary>
    public class StudyHandler
    {
        /// <summary>
        /// Closes an existing study.
        /// </summary>
        /// <param name="studyId">Study ID.</param>
        /// <param name="end">Date study ended.</param>
        /// <param name="diagnosis">Diagnosis.</param>
        /// <returns>True if the record was created. False otherwise.</returns>
        public static bool CloseStudy(int studyId, DateTime end, string diagnosis)
        {
            var closed = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var study = db.Studies.Single(s => s.StudyId == studyId);
                    study.DateCompleted = end;
                    study.Diagnosis = diagnosis;
                    study.IsCompleted = true;
                    db.SubmitChanges();
                }
                closed = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return closed;
        }

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
        /// Gets study information given the study ID.
        /// </summary>
        /// <param name="studyId">Study ID.</param>
        /// <returns>A study DAO if study ID is found.</returns>
        public static IList<Study> GetStudy(int studyId)
        {
            var list = new List<Study>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list.Add(db.Studies.Single(s => s.StudyId == studyId));
                }

            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return list;
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

        /// <summary>
        /// Checks whether a study has any images.
        /// </summary>
        /// <param name="studyId">Study ID.</param>
        /// <returns>True if the study has images. False otherwise.</returns>
        public static bool HasImages(int studyId)
        {
            var temp = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    temp = (from a in db.Studies
                            join b in db.Series on a.StudyId equals b.StudyId
                            where a.StudyId == studyId
                            select b.StudyId).Any();
                }

            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return temp;
        }

        /// <summary>
        /// Checks whether a study is uncompleted.
        /// </summary>
        /// <param name="studyId">Study ID.</param>
        /// <returns>True if the study is uncompleted. False otherwise.</returns>
        public static bool IsStudyOpen(int studyId)
        {
            var temp = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    temp = (db.Studies.Single(s => s.StudyId == studyId).IsCompleted);
                }

            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return !temp;
        }

        /// <summary>
        /// Checks whether a study ID exists in the system.
        /// </summary>
        /// <param name="studyId">Study ID.</param>
        /// <returns>True if the study ID is found. False otherwise.</returns>
        public static bool StudyExists(int studyId)
        {
            var temp = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    temp = (db.Studies.Any(s => s.StudyId == studyId));
                }

            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return temp;
        }
    }
}