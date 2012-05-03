using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the BloodTypes table in database.
    /// </summary>
    public class BloodHandler
    {
        /// <summary>
        /// Checks whether the blood type exists.
        /// </summary>
        /// <param name="bloodName">The name of the blood type</param>
        /// <returns>True if the blood type is found. False otherwise.</returns>
        public static bool BloodTypeExists(string bloodName)
        {
            var found = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    found = (db.BloodTypes.Any(b => b.BloodTypeName.Equals(bloodName.ToUpperInvariant())));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return found;
        }

        /// <summary>
        /// Gets all blood types.
        /// </summary>
        /// <returns>List containing the names of all blood types.</returns>
        public static IList<string> GetAllBloodTypes()
        {
            var list = new List<string>();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    list = (from b in db.BloodTypes
                            orderby b.BloodTypeName ascending
                            select b.BloodTypeName).ToList();
                }
            }
            catch (InvalidOperationException) { }
            return list;
        }

        /// <summary>
        /// Gets blood type ID given its name.
        /// </summary>
        /// <param name="bloodType">Blood type name.</param>
        /// <returns>Blood type ID.</returns>
        public static int GetBloodTypeId(string bloodType)
        {
            var id = -1;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    id = db.BloodTypes.Single(b => b.BloodTypeName.Equals(bloodType.ToUpperInvariant())).BloodTypeId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return id;
        }

        /// <summary>
        /// Gets blood type of a patient using their NRIC.
        /// </summary>
        /// <param name="nric">Patient NRIC.</param>
        /// <returns>Blood type name</returns>
        public static string GetPatientBloodType(string nric)
        {
            var bloodType = string.Empty;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    bloodType = db.BloodTypes.Single(b => b.BloodTypeId ==
                        db.Patients.Single(p => p.PatientId.Equals(UserParticularsHandler.GetGuidFromNric(nric))).BloodTypeId).BloodTypeName;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return bloodType;
        }

        /// <summary>
        /// Updates a patient's blood type.
        /// </summary>
        /// <param name="nric">Patient NRIC.</param>
        /// <param name="bloodType">Blood type name.</param>
        /// <returns>True if update was successful. False otherwise.</returns>
        public static bool UpdateBloodType(string nric, string bloodType)
        {
            var updated = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var patientId = UserParticularsHandler.GetGuidFromNric(nric);
                    if (!PatientHandler.HasMedicalRecords(nric))
                    {
                        // No prior medical records, create one
                        updated = PatientHandler.CreateMedicalRecord(patientId, bloodType);
                    }
                    else
                    {
                        // Update existing medical records
                        var patient = db.Patients.Single(p => p.PatientId.Equals(patientId));
                        patient.BloodTypeId = GetBloodTypeId(bloodType);
                        db.SubmitChanges();
                        updated = true;
                    }
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return updated;
        }
    }
}