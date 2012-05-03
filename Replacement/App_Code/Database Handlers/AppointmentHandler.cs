using System;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the Appointments table in database.
    /// </summary>
    public class AppointmentHandler
    {
        /// <summary>
        /// Creates a new appointment in the database.
        /// </summary>
        /// <param name="time">The date and time of the appointment.</param>
        /// <param name="studyId">The study ID.</param>
        /// <param name="nric">Patient's NRIC.</param>
        /// <returns>True if appointment is created. False otherwise.</returns>
        public static bool CreateAppointment(DateTime time, int studyId, string nric)
        {
            var created = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    db.Appointments.InsertOnSubmit(new Appointment
                    {
                        AppointmentDate = time,
                        StudyId = studyId,
                        PatientId = UserParticularsHandler.GetGuidFromNric(nric)
                    });
                    db.SubmitChanges();
                }
                created = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return created;
        }
    }
}