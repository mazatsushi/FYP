﻿using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;

/// <summary>
/// This class handles database queries on behalf of the application.
/// It is the closest we get to a dedicated data access layer.
/// </summary>
public class DatabaseHandler
{
    /// <summary>
    /// Adds a user to a pre-specified role name
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
        catch (ArgumentNullException) {}
        catch (ArgumentException) {}
        catch (ProviderException) {}
        return addStatus;
    }


    /// <summary>
    /// Queries the database to insert user particulars
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
            using (var db = new RIS_DB())
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
        catch {}
        return addStatus;
    }

    /// <summary>
    /// Calls the Membership provider and creates a new user account
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
    /// Finds the most privileged role that the current user is assigned to
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>A numeric value presenting the highest role of the user</returns>
    public static int FindMostPrivilegedRole(string username)
    {
        int roleNum;
        /*
         * It is entirely possible that the user belongs to more than one role.
         * Regardless we shall just search for the most privileged role assigned.
         */
        var roles = Roles.GetRolesForUser(username);
        if (roles.Any().Equals("Admin"))
        {
            // User has administrator privileges
            roleNum = 1;
        }
        else if (roles.Any().Equals("Physician"))
        {
            // User has physician privileges
            roleNum = 2;
        }
        else if (roles.Any().Equals("Radiologist"))
        {
            // User has radiologist privileges
            roleNum = 3;
        }
        else if (roles.Any().Equals("Staff"))
        {
            // User has staff privileges
            roleNum = 4;
        }
        else
        {
            // User has patient privileges
            roleNum = 5;
        }
        return roleNum;
    }

    /// <summary>
    /// Queries database to retrieve a country Id value
    /// </summary>
    /// <param name="countryName">The country name</param>
    /// <returns>A numeric value representing the country name</returns>
    public static int GetCountryId(string countryName)
    {
        int id;
        using (var db = new RIS_DB())
        {
            id = (from cty in db.Countries
                  where cty.CountryName.Equals(countryName)
                  select cty).First<Country>().CountryId;
        }
        return id;
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
    /// Gets the Guid for a user given the user name
    /// </summary>
    /// <param name="username">The user name</param>
    /// <returns>The user guid if the username exists. Null otherwise.</returns>
    public static string GetResetQuestion(string username)
    {
        string question = null;
        var user = Membership.GetUser(username);
        if (user != null)
        {
            question = user.PasswordQuestion;
        }
        return question;
    }

    /// <summary>
    /// Queries database to check whether NRIC already exists
    /// </summary>
    /// <param name="nric">The web element that triggered the event</param>
    /// <returns>True if the NRIC is currently in use. False otherwise.</returns>
    public static bool NricExists(string nric)
    {
        bool exists;
        using (var db = new RIS_DB())
        {
            var query = from record in db.UserParticulars
                        where (record.NRIC.Equals(nric))
                        select record.NRIC;
            exists = query.Any();
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
    /// Queries database to check whether username already exists
    /// </summary>
    /// <param name="username">The web element that triggered the event</param>
    /// <returns>True if the username is currently in use. False otherwise.</returns>
    public static bool UserNameExists(string username)
    {
        return (Membership.GetUser(username) != null);
    }

    /// <summary>
    /// Method to check whether user email already exists
    /// </summary>
    /// <param name="email">The web element that triggered the event</param>
    /// <returns>True if the email is currently in use. False otherwise.</returns>
    public static bool EmailInUse(string email)
    {
        return (Membership.GetUserNameByEmail(email) != null);
    }
}