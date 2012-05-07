using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Allergies and PatientsWithDrugAllergies tables in database.
    /// </summary>
    public class AllergyHandler
    {
        /// <summary>
        /// Checks whether the patient has any known drug allergies.
        /// </summary>
        /// <param name="nric">The patient's NRIC</param>
        /// <returns>True if the patient has any known existing allergies. False otherwise.</returns>
        public static bool AllergyExists(string nric)
        {
            var found = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    found = (db.PatientsWithDrugAllergies.Any(d => d.PatientId.Equals(UserParticularsHandler.GetGuidFromNric(nric))));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return found;
        }

        /// <summary>
        /// Gets of all a patient's allergies.
        /// </summary>
        /// <param name="nric">Patient's NRIC.</param>
        /// <returns>A list of strings containing the patient's allergies.</returns>
        public static IList<string> GetPatientAllergies(string nric)
        {
            var allergies = new List<string>();
            if (!AllergyExists(nric))
                return allergies;

            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var allergyIds = (from a in db.PatientsWithDrugAllergies
                                      where a.PatientId.Equals(UserParticularsHandler.GetGuidFromNric(nric))
                                      select a.DrugAllergyId);
                    allergies.AddRange(allergyIds.Select(allergyId => db.DrugAllergies.Single(a => a.DrugAllergyId == allergyId).DrugName));
                }
            }
            catch (InvalidOperationException) { }
            return allergies;
        }

        /// <summary>
        /// Updates the drug allergies of a patient.
        /// </summary>
        /// <param name="nric">Patient's NRIC.</param>
        /// <param name="drugName">Drug name.</param>
        /// <param name="toRemove">True to remove allergy. False to add allergy.</param>
        /// <returns>True if the allergy has been added/removed. False otherwise.</returns>
        public static bool UpdateAllergy(string nric, string drugName, bool toRemove)
        {
            var update = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var patientId = UserParticularsHandler.GetGuidFromNric(nric);
                    var drugId = DrugHandler.GetDrugId(drugName);
                    switch (toRemove)
                    {
                        case true:
                            // Removing an allergy
                            db.PatientsWithDrugAllergies.DeleteOnSubmit(db.PatientsWithDrugAllergies.Single(a => a.DrugAllergyId == drugId
                                && a.PatientId.Equals(patientId)));
                            break;
                        case false:
                            // Adding an allergy
                            db.PatientsWithDrugAllergies.InsertOnSubmit(new PatientsWithDrugAllergy
                            {
                                PatientId = patientId,
                                DrugAllergyId = drugId
                            });
                            break;
                    }
                    db.SubmitChanges();
                }
                update = true;
            }
            catch (InvalidOperationException) { }
            return update;
        }
    }
}