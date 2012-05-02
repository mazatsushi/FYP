﻿using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Security;
using BusinessObjects;

/// <summary>
/// This class handles database queries on behalf of the entire application.
/// No error checking will be performed at this level, as it is expected for all incoming data from the controllers
/// to be fully checked and desensitized.
/// </summary>

public class DatabaseHandler
{
    private static readonly string[] RolesList = { "Admin", "Patient", "Physician", "Radiologist" };
    private const int PasswordLength = 6;
    private const int NonAlphaNumeric = 1;
    private const string WorkDirectory = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads\";
    private const string PngExtension = ".png";
    private const string DicomExtension = ".dcm";

    /// <summary>
    /// Adds a new drug to the database
    /// </summary>
    /// <param name="drugName">The drug name</param>
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
                    DrugName = drugName
                });
                db.SubmitChanges();
                added = true;
            }
        }
        catch (InvalidOperationException) { }
        return added;
    }

    /// <summary>
    /// Adds a staff member to the database
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <param name="department">The department name</param>
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
                    DepartmentId = GetDepartmentId(department.ToLowerInvariant()),
                    StaffId = userId
                });
                db.SubmitChanges();
                added = true;
            }
        }
        catch (InvalidOperationException) { }
        return added;
    }

    /// <summary>
    /// Adds a user to a role
    /// </summary>
    /// <param name="username">The user name</param>
    /// <param name="rolename">The role name</param>
    /// <returns>True if the add was successful. False otherwise.</returns>
    public static bool AddUserToRole(string username, string rolename)
    {
        var addStatus = false;
        try
        {
            Roles.AddUserToRole(username.ToLowerInvariant(), rolename);
            addStatus = true;
        }
        catch (ArgumentNullException) { }
        catch (ArgumentException) { }
        catch (ProviderException) { }
        return addStatus;
    }

    /// <summary>
    /// Checks whether the patient has any known drug allergies.
    /// </summary>
    /// <param name="nric">The patient's NRIC</param>
    /// <returns>True if the patient has any known existing allergies. False otherwise.</returns>
    private static bool AllergyExists(string nric)
    {
        var found = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                found = (db.PatientsWithDrugAllergies.Any(d => d.PatientId.Equals(GetGuidFromNric(nric))));
            }
        }
        catch (InvalidOperationException) { }
        return found;
    }

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
        catch (InvalidOperationException) { }
        return found;
    }

    /// <summary>
    /// Change an user's password reset question and answer
    /// </summary>
    /// <param name="username">The user name</param>
    /// <param name="password">The password</param>
    /// <param name="question">The security question</param>
    /// <param name="answer">The answer to the security question</param>
    /// <returns></returns>
    public static bool ChangeQuestionAndAnswer(string username, string password, string question, string answer)
    {
        var user = Membership.GetUser(username);
        return user != null && user.ChangePasswordQuestionAndAnswer(password, question, answer);
    }

    /// <summary>
    /// Creates a new imaging appointment in the database
    /// </summary>
    /// <param name="time">The date and time of the appointment</param>
    /// <param name="studyId">The study Id</param>
    /// <param name="nric">Patient's NRIC</param>
    /// <returns>True if appointment is created. False otherwise.</returns>
    public static bool CreateAppointment(DateTime time, int studyId, string nric)
    {
        var created = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var guid = GetGuidFromNric(nric.ToUpperInvariant());
                if (guid != Guid.Empty)
                {
                    db.Appointments.InsertOnSubmit(new Appointment
                    {
                        AppointmentDate = time,
                        StudyId = studyId,
                        PatientId = guid
                    });
                }
                db.SubmitChanges();
            }
            created = true;
        }
        catch (InvalidOperationException) { }
        return created;
    }

    /// <summary>
    /// Creates a new imaging order in the database
    /// </summary>
    /// <param name="desc">A description of the imaging order</param>
    /// <param name="staffId">The physician that referred the patient</param>
    /// <returns>The study ID if the imaging order was created. -1 otherwise.</returns>
    public static int CreateImagingOrder(string desc, Guid staffId)
    {
        // TODO: Change Guid & review method
        var id = -1;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var study = new Study
                {
                    IsCompleted = false,
                    Diagnosis = null,
                    DateStarted = DateTime.Now,
                    Description = desc.ToLowerInvariant(),
                    ReferredBy = staffId
                };
                db.Studies.InsertOnSubmit(study);
                db.SubmitChanges();
                id = study.StudyId;
            }
        }
        catch (InvalidOperationException) { }
        return id;
    }

    /// <summary>
    /// Creates a new medical record tied to a patient's account.
    /// This only happens once per patient.
    /// </summary>
    /// <param name="patientId">The patient's Guid</param>
    /// <param name="bloodType">The name of the patient's blood type</param>
    /// <returns>True if the record was created. False otherwise.</returns>
    private static bool CreateMedicalRecord(Guid patientId, string bloodType)
    {
        var created = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                db.Patients.InsertOnSubmit(new Patient
                {
                    BloodTypeId = GetBloodTypeId(bloodType.ToUpperInvariant()),
                    PatientId = patientId
                });
                db.SubmitChanges();
                created = true;
            }
        }
        catch (InvalidOperationException) { }
        return created;
    }

    /// <summary>
    /// Creates a new series
    /// </summary>
    /// <param name="modId">The modality ID</param>
    /// <param name="studyId">The study ID</param>
    /// <returns>True if the record was created. False otherwise.</returns>
    public static int CreateNewSeries(int modId, int studyId)
    {
        var created = 0;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var s = new Series
                {
                    ModalityId = modId,
                    StudyId = studyId
                };
                db.Series.InsertOnSubmit(s);
                db.SubmitChanges();
                created = s.SeriesId;
            }
        }
        catch (InvalidOperationException) { }
        return created;
    }

    /// <summary>
    /// Creates a new study
    /// </summary>
    /// <param name="desc">Description of the study (purpose etc.)</param>
    /// <param name="start">Date of first appointment to commence study</param>
    /// <param name="staffUserName">Staff's account username</param>
    /// <returns>True if the record was created. False otherwise.</returns>
    public static int CreateNewStudy(string desc, DateTime start, string staffUserName)
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
                    ReferredBy = GetGuidFromUserName(staffUserName)
                };
                db.Studies.InsertOnSubmit(s);
                db.SubmitChanges();
                created = s.StudyId;
            }
        }
        catch (InvalidOperationException) { }
        return created;
    }

    /// <summary>
    /// Creates a new user account
    /// </summary>
    /// <param name="username">The user name</param>
    /// <param name="password">The password</param>
    /// <param name="email">The email address</param>
    /// <param name="question">The security question for password reset</param>
    /// <param name="answer">Answer to the security question</param>
    /// <param name="isApproved">Whether the account is approved upon creation</param>
    /// <param name="status">A return parameter for detailed error checking</param>
    /// <returns>A reference to a new Memebership User if an account is created. Null otherwise.</returns>
    public static MembershipUser CreateUser(string username, string password, string email, string question, string answer, bool isApproved, out MembershipCreateStatus status)
    {
        return Membership.CreateUser(username, password, email, question, answer, isApproved, out status);
    }

    /// <summary>
    /// Checks whether a drug already exists in the database
    /// </summary>
    /// <param name="drugName">The drug name</param>
    /// <returns>True if the drug name is found in the database. False otherwise.</returns>
    public static bool DrugExists(string drugName)
    {
        var found = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                found = (db.DrugAllergies.Any(d => d.DrugName.Equals(drugName)));
            }
        }
        catch (ArgumentNullException) { }
        catch (InvalidOperationException) { }
        return found;
    }

    /// <summary>
    /// Checks whether email address is already in use
    /// </summary>
    /// <param name="email">The web element that triggered the event</param>
    /// <returns>True if the email is currently in use. False otherwise.</returns>
    public static bool EmailInUse(string email)
    {
        return (Membership.GetUserNameByEmail(email.ToLowerInvariant()) != null);
    }

    /// <summary>
    /// Finds the most privileged role that the current user is assigned to
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>A numeric value representing the highest role of the user</returns>
    public static int FindMostPrivilegedRole(string username)
    {
        var roleNum = -1;
        /*
         * It is entirely possible that the user belongs to more than one role.
         * Regardless we shall just search for the most privileged role assigned.
         */
        var roles = Roles.GetRolesForUser(username.ToLowerInvariant());
        for (var i = 0; i < RolesList.Length; ++i)
        {
            if (!roles.Contains(RolesList[i]))
                continue;

            roleNum = i;
            break;
        }
        return roleNum;
    }

    /// <summary>
    /// Generates a random password
    /// </summary>
    /// <returns>A randomly generated password</returns>
    public static string GeneratePassword()
    {
        return Membership.GeneratePassword(PasswordLength, NonAlphaNumeric);
    }

    /// <summary>
    /// Gets a list of all blood types
    /// </summary>
    /// <returns>A string array containing the names of all blood types stored in the database.</returns>
    public static IList<string> GetAllBloodTypes()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from b in db.BloodTypes
                    orderby b.BloodTypeName ascending
                    select b.BloodTypeName).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all countries
    /// </summary>
    /// <returns>A string array containing the names of all countries stored in the database.</returns>
    public static IList<string> GetAllCountries()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from c in db.Countries
                    orderby c.CountryName ascending
                    select c.CountryName).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all departments
    /// </summary>
    /// <returns>A string array containing the names of all departments stored in the database.</returns>
    public static IList<string> GetAllDepartments()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from d in db.Departments
                    orderby d.DepartmentName ascending
                    select d.DepartmentName).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all medical drugs
    /// </summary>
    /// <returns>A string array containing the names of medical drugs stored in the database.</returns>
    public static IList<string> GetAllDrugs()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from d in db.DrugAllergies
                    orderby d.DrugName ascending
                    select d.DrugName).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all modalities
    /// </summary>
    /// <returns>A string array containing the names of all departments stored in the database.</returns>
    public static IList<string> GetAllModalities()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from m in db.Modalities
                    orderby m.Description ascending
                    select m.Description).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all roles for RIS
    /// </summary>
    /// <returns>A string array containing the names of all the roles stored in the data source.</returns>
    public static IList<string> GetAllRoles()
    {
        return Roles.GetAllRoles().ToList();
    }

    /// <summary>
    /// Gets the blood type Id given its string value
    /// </summary>
    /// <param name="bloodType">The blood type name</param>
    /// <returns>A foreign key value of the specified blood type</returns>
    private static int GetBloodTypeId(string bloodType)
    {
        var id = -1;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                id = db.BloodTypes.Single(b => b.BloodTypeName.Equals(bloodType.ToUpperInvariant())).BloodTypeId;
            }
        }
        catch (InvalidOperationException) { }
        return id;
    }

    /// <summary>
    /// Gets a country name given its id
    /// </summary>
    /// <param name="id">The country id</param>
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
        catch (InvalidOperationException) { }
        return countryName;
    }

    /// <summary>
    /// Queries database to retrieve a country Id value
    /// </summary>
    /// <param name="countryName">The country name</param>
    /// <returns>A numeric value representing the country name</returns>
    public static int GetCountryId(string countryName)
    {
        int id;
        using (var db = new RIS_DB_Entities())
        {
            id = (db.Countries.Single(c => c.CountryName.Equals(countryName.ToLowerInvariant())).CountryId);
        }
        return id;
    }

    /// <summary>
    /// Gets a department id given its name
    /// </summary>
    /// <param name="department">The department name</param>
    /// <returns>A foreign key value of the specified department</returns>
    private static int GetDepartmentId(string department)
    {
        var id = -1;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                id = db.Departments.Single(d => d.DepartmentName.Equals(department.ToLowerInvariant())).DepartmentId;
            }
        }
        catch (InvalidOperationException) { }
        return id;
    }

    /// <summary>
    /// Gets the ID of a drug given its name
    /// </summary>
    /// <param name="drugName">The drug name</param>
    /// <returns>The drug ID</returns>
    private static int GetDrugId(string drugName)
    {
        var id = -1;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                id = (db.DrugAllergies.Single(d => d.DrugName.Equals(drugName)).DrugAllergyId);
            }
        }
        catch (InvalidOperationException) { }
        return id;
    }

    /// <summary>
    /// Gets all of a user particulars given their NRIC
    /// </summary>
    /// <param name="nric">User's NRIC</param>
    /// <returns>A class representing a tuple in the UserParticulars table</returns>
    public static UserParticularsBO GetFullName(string nric)
    {
        var particulars = new UserParticularsBO();
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var temp = (db.UserParticulars.Single(p => p.NRIC.Equals(nric.ToUpperInvariant())));
                particulars.FirstName = temp.FirstName;
                particulars.LastName = temp.LastName;
                particulars.Prefix = temp.Prefix;
            }
        }
        catch (InvalidOperationException) { }
        return particulars;
    }

    /// <summary>
    /// Gets the user GUID given the nric
    /// </summary>
    /// <param name="nric">The username</param>
    /// <returns>The user GUID if found.</returns>
    private static Guid GetGuidFromNric(string nric)
    {
        var guid = new Guid(new byte[16]);
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                guid = (db.UserParticulars.Single(u => u.NRIC.Equals(nric.ToUpperInvariant()))).UserId;
            }
        }
        catch (InvalidOperationException) { }
        return guid;
    }

    /// <summary>
    /// Gets the user GUID given the user's name
    /// </summary>
    /// <param name="username">The username</param>
    /// <returns>The user GUID if found.</returns>
    private static Guid GetGuidFromUserName(string username)
    {
        var guid = new Guid(new byte[16]);
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                guid = (db.aspnet_Users.Single(u => u.UserName.Equals(username.ToLowerInvariant()))).UserId;
            }
        }
        catch (InvalidOperationException) { }
        return guid;
    }

    /// <summary>
    /// Gets a modality's Id given its description
    /// </summary>
    /// <param name="desc"></param>
    /// <returns></returns>
    public static int GetModalityId(string desc)
    {
        using (var db = new RIS_DB_Entities())
        {
            return db.Modalities.Single(b => b.Description.Equals(desc.ToLowerInvariant())).ModalityId;
        }
    }

    /// <summary>
    /// Gets all open studies of a patient
    /// </summary>
    /// <param name="nric">Patient NRIC</param>
    /// <returns>A list of oprn studies patient is involved in, if any.</returns>
    public static int GetOpenStudy(string nric)
    {
        var temp = -1;
        try
        {
            var patientId = GetGuidFromNric(nric.ToUpperInvariant());
            using (var db = new RIS_DB_Entities())
            {
                temp = (from s in db.Studies
                        join a in db.Appointments on s.StudyId equals a.StudyId
                        where (a.PatientId.Equals(patientId) && s.IsCompleted == false)
                        orderby s.StudyId
                        select s.StudyId).Single();
            }

        }
        catch (InvalidOperationException) { }
        return temp;
    }

    /// <summary>
    /// Gets all of a user particulars given their user id
    /// </summary>
    /// <param name="username">Username</param>
    /// <returns>A class representing a tuple in the UserParticulars table</returns>
    public static UserParticular GetParticularsFromUsername(string username)
    {
        UserParticular temp = null;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                temp = db.UserParticulars.Single(u => u.UserId.Equals(GetGuidFromUserName(username)));
            }
        }
        catch (InvalidOperationException) { }
        return temp;
    }

    /// <summary>
    /// Gets of all a patient's allergies
    /// </summary>
    /// <param name="nric">The patient's NRIC</param>
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
                                  where a.PatientId.Equals(GetGuidFromNric(nric))
                                  select a.DrugAllergyId);
                allergies.AddRange(allergyIds.Select(allergyId => db.DrugAllergies.Single(a => a.DrugAllergyId == allergyId).DrugName));
            }
        }
        catch (InvalidOperationException) { }
        return allergies;
    }

    /// <summary>
    /// Gets the blood type of an user via their patient ID
    /// </summary>
    /// <param name="nric">The patient's NRIC</param>
    /// <returns>A string representation of their blood type</returns>
    public static string GetPatientBloodType(string nric)
    {
        var bloodType = string.Empty;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                bloodType = db.BloodTypes.Single(b => b.BloodTypeId ==
                    db.Patients.Single(p => p.PatientId.Equals(GetGuidFromNric(nric))).BloodTypeId).BloodTypeName;
            }
        }
        catch (InvalidOperationException) { }
        return bloodType;
    }

    /// <summary>
    /// Gets the password reset question of a user given the user name
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>The string containing the password reset question if account exists. Null otherwise.</returns>
    public static string GetQuestion(string username)
    {
        var question = string.Empty;
        var membershipUser = Membership.GetUser(username.ToLowerInvariant());
        if (membershipUser != null)
            question = membershipUser.PasswordQuestion;
        return question;
    }

    /// <summary>
    /// Gets all the studies that patient has been involved in
    /// </summary>
    /// <param name="nric">Patient NRIC</param>
    /// <returns>A list of all studies patient is involved in, if any.</returns>
    public static IList<Study> GetStudies(string nric)
    {
        var temp = new List<Study>();
        try
        {
            var patientId = GetGuidFromNric(nric.ToUpperInvariant());
            using (var db = new RIS_DB_Entities())
            {
                temp = (from s in db.Studies
                        join a in db.Appointments on s.StudyId equals a.StudyId
                        where (a.PatientId.Equals(patientId))
                        select s).ToList();
            }

        }
        catch (InvalidOperationException) { }
        return temp;
    }

    /// <summary>
    /// Queries the Membership API to get the user email via the username
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>The user's email address</returns>
    public static string GetUserEmail(string username)
    {
        var user = Membership.GetUser(username.ToLowerInvariant());
        var email = string.Empty;
        if (user != null)
            email = user.Email;
        return email;
    }

    /// <summary>
    /// Gets patient's username given their nric
    /// </summary>
    /// <param name="nric">The user NRIC</param>
    /// <returns>A string containing the user's real name if found. Null otherwise.</returns>
    public static string GetUserNameFromNric(string nric)
    {
        var temp = string.Empty;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                temp = db.aspnet_Users.Single(u => u.UserId.Equals(GetGuidFromNric(nric))).UserName;
            }
        }
        catch (InvalidOperationException) { }
        return temp;
    }

    /// <summary>
    /// Gets a list of the roles that a user is in.
    /// </summary>
    /// <param name="username">The user to return a list of roles for.</param>
    /// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
    public static IList<string> GetUserRoles(string username)
    {
        return Roles.GetRolesForUser(username);
    }

    /// <summary>
    /// Checks whether the patient has prior medical records in the database.
    /// </summary>
    /// <param name="nric">The patient's NRIC</param>
    /// <returns>True if prior medical records are found. False otherwise.</returns>
    public static bool HasMedicalRecords(string nric)
    {
        var found = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                found = (db.Patients.Any(p => p.PatientId.Equals(GetGuidFromNric(nric))));
            }
        }
        catch (InvalidOperationException) { }
        return found;
    }

    /// <summary>
    /// Checks whether a user is in the specified role given their NRIC
    /// </summary>
    /// <param name="nric">User NRIC</param>
    /// <param name="role">The role name</param>
    /// <returns>True if user is in the specified role. False otherwise.</returns>
    public static bool IsInRole(string nric, string role)
    {
        var isInRole = false;
        var userGuid = GetGuidFromNric(nric.ToUpperInvariant());
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var username = db.aspnet_Users.Single(u => u.UserId.Equals(userGuid)).UserName;
                isInRole = Roles.IsUserInRole(username, role);
            }
        }
        catch (InvalidOperationException) { }
        return isInRole;
    }

    /// <summary>
    /// Checks whether NRIC already exists
    /// </summary>
    /// <param name="nric">User NRIC</param>
    /// <returns>True if the NRIC is currently in use. False otherwise.</returns>
    public static bool NricExists(string nric)
    {
        var exists = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                exists = (db.UserParticulars.Any(u => u.NRIC.Equals(nric)));
            }
        }
        catch (InvalidOperationException) { }
        return exists;
    }

    /// <summary>
    /// Removes the specified user from the specified role.
    /// </summary>
    /// <param name="username">The user to remove from the specified role.</param>
    /// <param name="rolename">The role to remove the specified user from.</param>
    /// <returns>True if the user is successfully removed from a role. False otherwise.</returns>
    public static bool RemoveUserFromRole(string username, string rolename)
    {
        var removed = false;
        try
        {
            Roles.RemoveUserFromRole(username, rolename);
            removed = true;
        }
        catch (ArgumentNullException) { }
        catch (ArgumentException) { }
        catch (ProviderException) { }
        return removed;
    }

    /// <summary>
    /// Reset the user's password.
    /// </summary>
    /// <param name="username">The user name</param>
    /// <param name="answer">The answer to user's security question</param>
    /// <param name="newPassword">A string containing the new password</param>
    /// <returns>True if the password is successfully reset. False otherwise.</returns>
    public static bool ResetPassword(string username, string answer, out string newPassword)
    {
        var success = false;
        newPassword = string.Empty;

        var user = Membership.GetUser(username);

        if (user == null)
            return false;

        try
        {
            newPassword = user.ResetPassword(answer);
            success = true;
        }
        catch (MembershipPasswordException e)
        {
            newPassword = e.Message;
        }

        return success;
    }

    public static bool SaveImages(string fileNameOnly)
    {
        // TODO: Finish this method
        throw new NotImplementedException();
        var success = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                // Save DICOM
                //var binData = File.ReadAllBytes(WorkDirectory + fileNameOnly + DicomExtension);
                //var dicomFile = new DicomImage
                //                {
                //                    Image = new Binary(binData)
                //                };
                //db.DicomImages.InsertOnSubmit(dicomFile);
                //var dicomUid = dicomFile.DicomUID;
                //// Save PNGs
                //var files = new DirectoryInfo(WorkDirectory).GetFiles("*.png");
                //foreach (var fileInfo in files)
                //{
                //    var binData2 = File.ReadAllBytes(fileInfo.FullName);
                //    var pngFile = new PngImage
                //                      {
                //                          Image = new Binary(binData2)
                //                      };
                //    db.PngImages.InsertOnSubmit(pngFile);
                //}
                //db.SubmitChanges();

                // Delete DICOM
                var files = new DirectoryInfo(WorkDirectory).GetFiles("*.dcm");
                foreach (var fileInfo in files)
                {
                    fileInfo.Delete();
                }
                // Delete PNGs
                files = new DirectoryInfo(WorkDirectory).GetFiles("*.png");
                foreach (var fileInfo in files)
                {
                    fileInfo.Delete();
                }
                success = true;
            }
        }
        catch (Exception) { }
        return success;
    }

    /// <summary>
    /// Checks if a series exists
    /// </summary>
    /// <param name="seriesId">The series id</param>
    /// <returns>True if the series exists. False otherwise</returns>
    public static bool SeriesExists(int seriesId)
    {
        // TODO: Review this method
        var found = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                found = (from d in db.Series
                         where d.SeriesId == seriesId
                         select d).Any();
            }
        }
        catch (ArgumentNullException) { }
        catch (InvalidOperationException) { }
        return found;
    }

    /// <summary>
    /// Updates the account information of a user
    /// </summary>
    /// <param name="username">Username</param>
    /// /// <param name="email">User's new email address</param>
    /// <returns>True if update is successful. False otherwise.</returns>
    public static bool UpdateAccount(string username, string email)
    {
        var update = false;
        try
        {
            var user = Membership.GetUser(username);
            user.Email = email;
            Membership.UpdateUser(user);
            update = true;
        }
        catch (ProviderException) { }
        return update;
    }

    /// <summary>
    /// Updates the drug allergies of a patient.
    /// </summary>
    /// <param name="nric">The patient's NRIC</param>
    /// <param name="drugName">The drug name</param>
    /// <param name="toRemove">True to indicate remove allergy. False to indicate add allergy.</param>
    /// <returns>True if the allergy has been added/removed. False otherwise.</returns>
    public static bool UpdateAllergy(string nric, string drugName, bool toRemove)
    {
        var update = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var patientId = GetGuidFromNric(nric);
                var drugId = GetDrugId(drugName);
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

    /// <summary>
    /// Updates a patient's blood type
    /// </summary>
    /// <param name="nric">The username</param>
    /// <param name="bloodType">The name of the patient's blood type</param>
    /// <returns>True if the record was created. False otherwise.</returns>
    public static bool UpdateBloodType(string nric, string bloodType)
    {
        var updated = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var patientId = GetGuidFromNric(nric);
                if (!HasMedicalRecords(nric))
                {
                    // No prior medical records, create one
                    updated = CreateMedicalRecord(patientId, bloodType);
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
        catch (InvalidOperationException) { }
        return updated;
    }

    /// <summary>
    /// Updates the personal information of a user
    /// </summary>
    /// <param name="username">The username</param>
    /// <param name="firstName">User's first name</param>
    /// <param name="middleName">User's middle name</param>
    /// <param name="lastName">User's last name</param>
    /// <param name="namePrefix">User's name prefix</param>
    /// <param name="nameSuffix">User's name suffix</param>
    /// <param name="address">User address</param>
    /// <param name="contact">User contact number</param>
    /// <param name="postalCode">The postal code</param>
    /// <param name="countryId">The country ID</param>
    /// <param name="nationality">The user's nationality</param>
    /// <returns>True if the update is successful. False otherwise.</returns>
    public static bool UpdateParticulars(string username, string firstName, string middleName, string lastName, string namePrefix, string nameSuffix, string address, string contact, string postalCode, int countryId, string nationality)
    {
        var success = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var user = db.UserParticulars.Single(u => u.UserId.Equals(GetGuidFromUserName(username)));
                user.FirstName = firstName;
                user.MiddleName = middleName;
                user.LastName = lastName;
                user.Prefix = namePrefix;
                user.Suffix = nameSuffix;
                user.Address = address;
                user.ContactNumber = contact;
                user.PostalCode = postalCode;
                user.CountryOfResidence = countryId;
                user.Nationality = nationality;
                db.SubmitChanges();
            }
            success = true;
        }
        catch (InvalidOperationException) { }
        return success;
    }

    /// <summary>
    /// Adds user particulars to database
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <param name="nric">The NRIC</param>
    /// <param name="firstName">The first name</param>
    /// <param name="middleName">The middle name</param>
    /// <param name="lastName">The last name</param>
    /// <param name="gender">The gender</param>
    /// <param name="namePrefix">The salutation</param>
    /// <param name="nameSuffix">The name suffix</param>
    /// <param name="dob">The date of birth</param>
    /// <param name="address">The address</param>
    /// <param name="contact">The contact number</param>
    /// <param name="postalCode">The postal code</param>
    /// <param name="countryId">The country ID</param>
    /// <param name="nationality">The nationality</param>
    /// <returns>True if the insert is successful. False otherwise.</returns>
    public static bool UpdateParticulars(Guid userId, string nric, string firstName, string middleName, string lastName, string gender, string namePrefix, string nameSuffix, DateTime dob, string address, string contact, string postalCode, int countryId, string nationality)
    {
        var addStatus = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                // Insert all data into user particulars table
                db.UserParticulars.InsertOnSubmit(new UserParticular
                {
                    NRIC = nric,
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName,
                    Gender = Char.Parse(gender),
                    Prefix = namePrefix,
                    Suffix = nameSuffix,
                    DateOfBirth = dob,
                    Address = address,
                    ContactNumber = contact,
                    PostalCode = postalCode,
                    CountryOfResidence = countryId,
                    Nationality = nationality,
                    UserId = userId
                });
                db.SubmitChanges();
                addStatus = true;
            }
        }
        catch (InvalidOperationException) { }
        return addStatus;
    }

    /// <summary>
    /// Queries database to check whether username already exists
    /// </summary>
    /// <param name="username">The username</param>
    /// <returns>True if the username is currently in use. False otherwise.</returns>
    public static bool UserNameExists(string username)
    {
        return (Membership.GetUser(username) != null);
    }
}