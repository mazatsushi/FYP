using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Patient table in database.
    /// </summary>
    public class PatientHandler
    {
        /// <summary>
        /// Creates a new medical record tied to a patient's account.
        /// This only happens once per patient.
        /// </summary>
        /// <param name="patientId">Patient Guid.</param>
        /// <param name="bloodType">Patient's blood type name.</param>
        /// <returns>True if the record was created. False otherwise.</returns>
        public static bool CreateMedicalRecord(Guid patientId, string bloodType)
        {
            var created = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    db.Patients.InsertOnSubmit(new Patient
                    {
                        BloodTypeId = BloodHandler.GetBloodTypeId(bloodType),
                        PatientId = patientId
                    });
                    db.SubmitChanges();
                }
                created = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return created;
        }

        /// <summary>
        /// Checks whether the patient has prior medical records in the system.
        /// </summary>
        /// <param name="nric">Patient's NRIC.</param>
        /// <returns>True if prior medical records are found. False otherwise.</returns>
        public static bool HasMedicalRecords(string nric)
        {
            var found = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    found = (db.Patients.Any(p => p.PatientId.Equals(UserParticularsHandler.GetGuidFromNric(nric))));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return found;
        }
    }
}