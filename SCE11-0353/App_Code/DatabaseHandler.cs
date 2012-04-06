using RIS_DB_Model;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Security;

/// <summary>
/// This class handles database queries on behalf of the entire application.
/// It is the closest we get to a dedicated data access layer.
/// </summary>

public class DatabaseHandler
{
    private static readonly string[] RolesList = { "Admin", "Patient", "Physician", "Radiologist" };
    private const int PasswordLength = 6;
    private const int NonAlphaNumeric = 1;
    private const string WorkDirectory = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads\";
    private const string PngExtension = ".png";
    private const string DicomExtension = ".dcm";

    public static bool SaveImages(string fileNameOnly)
    {
        // TODO: Finish this method
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
    /// Adds a new drug allergy to the database
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
                var drug = new DrugAllergy
                               {
                                   DrugName = drugName
                               };
                db.DrugAllergies.InsertOnSubmit(drug);
                db.SubmitChanges();
                added = true;
            }
        }
        catch (Exception) { }
        return added;
    }

    /// <summary>
    /// Adds a staff member to the database
    /// </summary>
    /// <param name="userGuid">The user guid</param>
    /// <param name="department">The department name</param>
    /// <param name="isFellow">Whether the staff member is a fellow</param>
    /// <returns>True if the staff was added. False otherwise.</returns>
    public static bool AddStaff(string userGuid, string department, bool isFellow)
    {
        var added = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var staff = new Staff
                {
                    DepartmentId = GetDepartmentId(department),
                    IsFellow = isFellow,
                    UserId = Guid.Parse(userGuid)
                };
                db.Staffs.InsertOnSubmit(staff);
                db.SubmitChanges();
                added = true;
            }
        }
        catch (Exception) { }
        return added;
    }

    /// <summary>
    /// Adds user particulars to database
    /// </summary>
    /// <param name="userGuid">The Guid of the user in Membership</param>
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
    public static bool AddUserParticulars(object userGuid, string nric, string firstName, string middleName, string lastName, char gender, string namePrefix, string nameSuffix, DateTime dob, string address, string contact, string postalCode, int countryId, string nationality)
    {
        var addStatus = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                // Insert all data into user particulars table
                var userParticulars = new UserParticular
                                          {
                                              NRIC = nric,
                                              FirstName = firstName,
                                              MiddleName = middleName,
                                              LastName = lastName,
                                              Gender = gender,
                                              Prefix = namePrefix,
                                              Suffix = nameSuffix,
                                              DateOfBirth = dob,
                                              Address = address,
                                              ContactNumber = contact,
                                              PostalCode = postalCode,
                                              CountryOfResidence = countryId,
                                              Nationality = nationality,
                                              UserId = Guid.Parse(userGuid.ToString())
                                          };

                db.UserParticulars.InsertOnSubmit(userParticulars);
                db.SubmitChanges();
                addStatus = true;
            }
        }
        catch (Exception)
        { }
        return addStatus;
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
            Roles.AddUserToRole(username, rolename);
            addStatus = true;
        }
        catch (ArgumentNullException) { }
        catch (ArgumentException) { }
        catch (ProviderException) { }
        return addStatus;
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
    /// <param name="patientNric">Patient's NRIC</param>
    /// <returns>True if appointment is created. False otherwise.</returns>
    public static bool CreateAppointment(DateTime time, int studyId, string patientNric)
    {
        var created = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var guid = GetGuidFromNric(patientNric);
                if (!string.IsNullOrEmpty(guid))
                {
                    var patientId = GetPatientIdFromGuid(guid);
                    var app = new Appointment
                                  {
                                      AppointmentDate = time,
                                      StudyId = studyId,
                                      PatientId = patientId
                                  };
                    db.Appointments.InsertOnSubmit(app);
                    db.SubmitChanges();
                    created = true;
                }
            }
        }
        catch (InvalidOperationException) { }
        return created;
    }

    /// <summary>
    /// Creates a new imaging order in the database
    /// </summary>
    /// <param name="desc">A description of the imaging order</param>
    /// <param name="staffId">The physician that referred the patient</param>
    /// <returns>The study ID if the imaging order was created. 0 otherwise.</returns>
    public static int CreateImagingOrder(string desc, int staffId)
    {
        var id = 0;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var study = new Study
                                {
                                    IsCompleted = false,
                                    Diagnosis = null,
                                    DateStarted = DateTime.Now,
                                    Description = desc,
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
    /// Creates a new medical record tied to a patient's account
    /// </summary>
    /// <param name="nric">The patient's NRIC</param>
    /// <param name="bloodType">The name of the patient's blood type</param>
    /// <returns>True if the record was created. False otherwise.</returns>
    public static bool CreateMedicalRecord(string nric, string bloodType)
    {
        var created = false;
        try
        {
            var guid = GetGuidFromNric(nric);
            using (var db = new RIS_DB_Entities())
            {
                var p = new Patient
                            {
                                BloodTypeId = GetBloodTypeId(bloodType),
                                UserId = Guid.Parse(guid)
                            };
                db.Patients.InsertOnSubmit(p);
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
                    ModalityType = modId,
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
        using (var db = new RIS_DB_Entities())
        {
            var query = (from d in db.DrugAllergies
                         where d.DrugName.Equals(drugName)
                         select d);
            if (query.Any())
                found = true;
        }
        return found;
    }

    /// <summary>
    /// Checks whether email address is already in use
    /// </summary>
    /// <param name="email">The web element that triggered the event</param>
    /// <returns>True if the email is currently in use. False otherwise.</returns>
    public static bool EmailInUse(string email)
    {
        return (Membership.GetUserNameByEmail(email) != null);
    }

    /// <summary>
    /// Checks if a series exists
    /// </summary>
    /// <param name="seriesId">The series id</param>
    /// <returns>True if the series exists. False otherwise</returns>
    public static bool SeriesExists(int seriesId)
    {
        var found = false;
        using (var db = new RIS_DB_Entities())
        {
            var query = (from d in db.Series
                         where d.SeriesId == seriesId
                         select d);
            if (query.Any())
                found = true;
        }
        return found;
    }

    /// <summary>
    /// Finds the most privileged role that the current user is assigned to
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>A numeric value presenting the highest role of the user</returns>
    public static int FindMostPrivilegedRole(string username)
    {
        var roleNum = -1;
        /*
         * It is entirely possible that the user belongs to more than one role.
         * Regardless we shall just search for the most privileged role assigned.
         */
        var roles = Roles.GetRolesForUser(username);
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
    /// <returns>A random password.</returns>
    public static string GeneratePassword()
    {
        var temp = Membership.GeneratePassword(PasswordLength, NonAlphaNumeric);
        return temp;
    }

    /// <summary>
    /// Gets a list of all blood types
    /// </summary>
    /// <returns>A string array containing the names of all blood types stored in the database.</returns>
    public static List<string> GetAllBloodTypes()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from b in db.BloodTypes
                    select b.BloodType1).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all countries
    /// </summary>
    /// <returns>A string array containing the names of all countries stored in the database.</returns>
    public static IQueryable<string> GetAllCountries()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from c in db.Countries
                    orderby c.CountryName ascending
                    select c.CountryName);
        }
    }

    /// <summary>
    /// Gets a list of all departments
    /// </summary>
    /// <returns>A string array containing the names of all departments stored in the database.</returns>
    public static List<string> GetAllDepartments()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from d in db.Departments
                    select d.DepartmentName).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all departments
    /// </summary>
    /// <returns>A string array containing the names of all departments stored in the database.</returns>
    public static List<string> GetAllModalities()
    {
        using (var db = new RIS_DB_Entities())
        {
            return (from m in db.Modalities
                    select m.Description).ToList();
        }
    }

    /// <summary>
    /// Gets a list of all roles for RIS
    /// </summary>
    /// <returns>A string array containing the names of all the roles stored in the data source.</returns>
    public static string[] GetAllRoles()
    {
        return Roles.GetAllRoles();
    }

    /// <summary>
    /// Gets the blood type Id given its string value
    /// </summary>
    /// <param name="bloodType">The blood type name</param>
    /// <returns>A foreign key value of the specified blood type</returns>
    private static int GetBloodTypeId(string bloodType)
    {
        using (var db = new RIS_DB_Entities())
        {
            return db.BloodTypes.Single(b => b.BloodType1.Equals(bloodType)).BloodTypeId;
        }
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
            id = (db.Countries.Single(c => c.CountryName.Equals(countryName)).CountryId);
        }
        return id;
    }

    public static int GetModalityId(string desc)
    {
        using (var db = new RIS_DB_Entities())
        {
            return db.Modalities.Single(b => b.Description.Equals(desc)).ModalityId;
        }
    }

    /// <summary>
    /// Gets a country name given its id
    /// </summary>
    /// <param name="id">The country id</param>
    /// <returns>A string containing the country name. Null otherwise.</returns>
    public static string GetCountryName(int id)
    {
        using (var db = new RIS_DB_Entities())
        {
            return (db.Countries.Single(c => c.CountryId == id).CountryName);
        }
    }

    /// <summary>
    /// Gets a department id given its name
    /// </summary>
    /// <param name="department">The department name</param>
    /// <returns>A foreign key value of the specified department</returns>
    public static int GetDepartmentId(string department)
    {
        using (var db = new RIS_DB_Entities())
        {
            return db.Departments.Single(d => d.DepartmentName.Equals(department)).DepartmentId;
        }
    }

    /// <summary>
    /// Gets the user GUID given the nric
    /// </summary>
    /// <param name="nric">The username</param>
    /// <returns>The user GUID if found.</returns>
    private static string GetGuidFromNric(string nric)
    {
        var guid = string.Empty;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                guid = (db.UserParticulars.Single(u => u.NRIC.Equals(nric))).UserId.ToString();
            }
        }
        catch (InvalidOperationException) { }
        return guid;
    }

    /// <summary>
    /// Gets the user GUID given the username
    /// </summary>
    /// <param name="username">The username</param>
    /// <returns>The user GUID if found.</returns>
    private static string GetGuidFromUsername(string username)
    {
        var guid = string.Empty;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                guid = (db.aspnet_Users.Single(u => u.UserName.Equals(username))).UserId.ToString();
            }
        }
        catch (InvalidOperationException) { }
        return guid;
    }

    public static void GetPatientDetails(string nric)
    {
        using (var db = new RIS_DB_Entities())
        {
            var details = from u in db.UserParticulars
                          where u.NRIC.Equals(nric.ToUpperInvariant())
                          select new
                                     {
                                         u.NRIC,
                                         u.Prefix,
                                         u.FirstName,
                                         u.LastName,
                                         u.Gender,
                                         u.DateOfBirth
                                     };
        }
    }

    /// <summary>
    /// Gets user's patient id given their guid
    /// </summary>
    /// <param name="guid">The user guid</param>
    /// <returns>A string containing the user's guid if found. Null otherwise.</returns>
    private static int GetPatientIdFromGuid(string guid)
    {
        var temp = -1;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                temp = (db.Patients.Single(p => p.UserId.Equals(Guid.Parse(guid)))).PatientId;
            }
        }
        catch (InvalidOperationException)
        { }
        return temp;
    }

    /// <summary>
    /// Gets user's real name given their nric
    /// </summary>
    /// <param name="nric">The user NRIC</param>
    /// <returns>A string containing the user's real name if found. Null otherwise.</returns>
    public static string GetPatientName(string nric)
    {
        var temp = string.Empty;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var user = (db.UserParticulars.Single(u => u.NRIC.Equals(nric)));
                temp = user.FirstName + " " + user.LastName;
            }
        }
        catch (InvalidOperationException)
        { }
        return temp;
    }

    /// <summary>
    /// Gets the staff ID given the username
    /// </summary>
    /// <param name="username">The staff username</param>
    /// <returns>The staff ID if found.</returns>
    public static int GetStaffId(string username)
    {
        var id = -1;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var guid = GetGuidFromUsername(username);
                if (!String.IsNullOrEmpty(guid))
                    id = db.Staffs.Single(s => s.UserId.Equals(Guid.Parse(guid))).StaffId;
            }
        }
        catch (InvalidOperationException) { }
        return id;
    }

    public static IQueryable GetStudies(string nric)
    {
        try
        {
            var patientId = GetPatientIdFromGuid(GetGuidFromNric(nric));
            var db = new RIS_DB_Entities();

            var temp = (from s in db.Studies
                        join a in db.Appointments on s.StudyId equals a.StudyId
                        where (a.PatientId.Equals(patientId))
                        select new
                                   {
                                       ID = s.StudyId,
                                       Description = s.Description,
                                       Date_Started = s.DateStarted,
                                       Completed = s.IsCompleted,
                                       Date_Completed = s.DateCompleted,
                                       Diagnosis = s.Diagnosis,
                                   });
            return temp;

        }
        catch (InvalidOperationException)
        {
            return null;
        }
        catch (SqlException)
        {
            return null;
        }
    }

    /// <summary>
    /// Queries the Membership API to get the user email via the username
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>The user's email address</returns>
    public static string GetUserEmail(string username)
    {
        var user = Membership.GetUser(username);
        var email = string.Empty;
        if (user != null)
        {
            email = user.Email;
        }
        return email;
    }

    /// <summary>
    /// Gets the username given a NRIC
    /// </summary>
    /// <param name="nric">The user's NRIC</param>
    /// <returns>A string containing the username if NRIC exists. Null otherwise</returns>
    public static string GetUserName(string nric)
    {
        using (var db = new RIS_DB_Entities())
        {
            var temp = db.UserParticulars.Single(up => up.NRIC.Equals(nric)).UserId;
            return db.aspnet_Users.Single(u => u.UserId.Equals(temp)).UserName;
        }
    }

    /// <summary>
    /// Gets all of a user particulars given their user id
    /// </summary>
    /// <param name="userGuid"></param>
    /// <returns>A class representing a tuple in the UserParticulars table</returns>
    public static UserParticular GetUserParticulars(string userGuid)
    {
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                return (db.UserParticulars.Single(u => u.UserId.Equals(Guid.Parse(userGuid))));
            }
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the password reset question of a user given the user name
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>The string containing the password reset question if account exists. Null otherwise.</returns>
    public static string GetQuestion(string username)
    {
        var question = string.Empty;
        var membershipUser = Membership.GetUser(username);
        if (membershipUser != null)
            question = membershipUser.PasswordQuestion;
        return question;
    }

    /// <summary>
    /// Gets the MembershipUser object given the username
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>An object reference to the MembershipUser if an account exists. Null otherwise.</returns>
    public static MembershipUser GetUser(string username)
    {
        return Membership.GetUser(username);
    }

    /// <summary>
    /// Gets a list of the roles that a user is in.
    /// </summary>
    /// <param name="username">The user to return a list of roles for.</param>
    /// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
    public static string[] GetUserRoles(string username)
    {
        return Roles.GetRolesForUser(username);
    }

    /// <summary>
    /// Checks whether the user has prior medical records in the database.
    /// </summary>
    /// <param name="nric">The username.</param>
    /// <returns>True if prior medical records are found. False otherwise.</returns>
    public static bool HasMedicalRecords(string nric)
    {
        var found = false;
        var guid = GetGuidFromNric(nric);
        using (var db = new RIS_DB_Entities())
        {
            var query = (from p in db.Patients
                         where p.UserId.Equals(Guid.Parse(guid))
                         select p);
            if (query.Any())
                found = true;
        }
        return found;
    }

    /// <summary>
    /// Checks whether the user has any open studies in the database.
    /// </summary>
    /// <param name="nric">The patient NRIC.</param>
    /// <returns>True if existing appointment is found. False otherwise.</returns>
    public static bool HasOpenStudies(string nric)
    {
        var found = false;
        try
        {
            var guid = GetGuidFromNric(nric);
            var patientId = GetPatientIdFromGuid(guid);
            using (var db = new RIS_DB_Entities())
            {
                var query = (from s in db.Studies
                             join a in db.Appointments on s.StudyId equals a.StudyId
                             where (a.PatientId.Equals(patientId) && s.IsCompleted == false)
                             select s.StudyId);
                if (query.Any())
                    found = true;
            }
        }
        catch (InvalidOperationException) { }
        return found;
    }

    /// <summary>
    /// Determines whether or not a user is in the specified role given their NRIC
    /// </summary>
    /// <param name="nric">User NRIC</param>
    /// <param name="role">The role name</param>
    /// <returns>True if user is in the specified role. False otherwise.</returns>
    public static bool IsInRole(string nric, string role)
    {
        var isInRole = false;
        using (var db = new RIS_DB_Entities())
        {
            // Do not perform database joins as they are very intensive
            var userGuid = db.UserParticulars.Single(u => u.NRIC.Equals(nric)).UserId.ToString();
            var username = db.aspnet_Users.Single(u => u.UserId.Equals(Guid.Parse(userGuid))).UserName;
            if (Roles.IsUserInRole(username, role))
                isInRole = true;
        }
        return isInRole;
    }

    /// <summary>
    /// Queries database to check whether NRIC already exists
    /// </summary>
    /// <param name="nric">The web element that triggered the event</param>
    /// <returns>True if the NRIC is currently in use. False otherwise.</returns>
    public static bool NricExists(string nric)
    {
        var exists = false;
        using (var db = new RIS_DB_Entities())
        {
            var result = (from user in db.UserParticulars
                          where user.NRIC.Equals(nric)
                          select user);
            if (result.Any())
                exists = true;
        }
        return exists;
    }

    /// <summary>
    /// Queries the Membership provider to reset the password.
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

    public static bool StudyExists(int studyId)
    {
        var found = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var query = db.Studies.Single(s => s.StudyId == studyId);
                found = true;
            }
        }
        catch (Exception) { }
        return found;
    }

    /// <summary>
    /// Updates the account information of a user
    /// </summary>
    /// <param name="u">The MembershipUser object reference to the user account</param>
    /// <returns>True if update is successful. False otherwise.</returns>
    public static bool UpdateAccount(MembershipUser u)
    {
        var update = false;
        try
        {
            Membership.UpdateUser(u);
            update = true;
        }
        catch (ProviderException)
        { }
        return update;
    }

    /// <summary>
    /// Updates an existing medical record of a patient
    /// </summary>
    /// <param name="nric">The username</param>
    /// <param name="bloodType">The name of the patient's blood type</param>
    /// <returns>True if the record was created. False otherwise.</returns>
    public static bool UpdateMedicalRecord(string nric, string bloodType)
    {
        var updated = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var patient = db.Patients.Single(p => p.UserId.Equals(GetGuidFromNric(nric)));
                patient.BloodTypeId = GetBloodTypeId(bloodType);
                db.SubmitChanges();
            }
            updated = true;
        }
        catch (InvalidOperationException) { }
        return updated;
    }

    /// <summary>
    /// Updates the personal information of a user
    /// </summary>
    /// <param name="userGuid">User ID</param>
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
    public static bool UpdateParticulars(object userGuid, string firstName, string middleName, string lastName, string namePrefix, string nameSuffix, string address, string contact, string postalCode, int countryId, string nationality)
    {
        var success = false;
        try
        {
            using (var db = new RIS_DB_Entities())
            {
                var user = db.UserParticulars.Single(u => u.UserId.Equals(Guid.Parse(userGuid.ToString())));
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
                success = true;
            }
        }
        catch (ChangeConflictException)
        {
        }
        return success;
    }

    /// <summary>
    /// Queries database to check whether username already exists
    /// </summary>
    /// <param name="username">The web element that triggered the event</param>
    /// <returns>True if the username is currently in use. False otherwise.</returns>
    public static bool UserNameExists(string username)
    {
        return (Membership.GetUser(username) != null);
    }
}