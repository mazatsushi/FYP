using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Globalization;
using System.Linq;
using System.Web.Security;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the ASP.NET Membership tables in database.
    /// </summary>
    public class MembershipHandler
    {
        private const int NonAlphaNumeric = 1;
        private const int PasswordLength = 6;
        private static readonly string[] RolesList = { "Admin", "Patient", "Physician", "Radiologist" };

        /// <summary>
        /// Adds a user to a role.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="rolename">Role name.</param>
        /// <returns>True if the add was successful. False otherwise.</returns>
        public static bool AddUserToRole(string username, string rolename)
        {
            var addStatus = false;
            try
            {
                Roles.AddUserToRole(username.ToLowerInvariant(), rolename);
                addStatus = true;
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return addStatus;
        }

        /// <summary>
        /// Change user's password reset question and answer.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="question">Security question.</param>
        /// <param name="answer">Answer to the security question.</param>
        /// <returns></returns>
        public static bool ChangeQuestionAndAnswer(string username, string password, string question, string answer)
        {
            var changed = false;
            try
            {
                var user = Membership.GetUser(username.ToLowerInvariant());
                if (user != null)
                    changed = user.ChangePasswordQuestionAndAnswer(password, new CultureInfo("en-SG").TextInfo.ToTitleCase(question), answer.ToLowerInvariant());
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return changed;
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email address.</param>
        /// <param name="question">Security question for password reset.</param>
        /// <param name="answer">Answer to the security question.</param>
        /// <param name="isApproved">Whether the account is pre-approved.</param>
        /// <param name="status">A return parameter for detailed error checking.</param>
        /// <returns>A reference to a new Memebership User if an account is created. Null otherwise.</returns>
        public static MembershipUser CreateUser(string username, string password, string email, string question, string answer, bool isApproved, out MembershipCreateStatus status)
        {
            return Membership.CreateUser(username, password, email, question, answer, isApproved, out status);
        }

        /// <summary>
        /// Checks whether email address is already in use.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>True if the email is currently in use. False otherwise.</returns>
        public static bool EmailInUse(string email)
        {
            var found = true;
            try
            {
                found = (Membership.GetUserNameByEmail(email.ToLowerInvariant()) != null);
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return found;
        }

        /// <summary>
        /// Finds the most privileged role that the current user is assigned to.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>Role name ID.</returns>
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
        /// Generates a random password.
        /// </summary>
        /// <returns>A randomly generated password.</returns>
        public static string GeneratePassword()
        {
            var newPassword = string.Empty;
            try
            {
                newPassword = Membership.GeneratePassword(PasswordLength, NonAlphaNumeric);
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return newPassword;
        }

        /// <summary>
        /// Gets all roles in system.
        /// </summary>
        /// <returns>A list containing all role names in system.</returns>
        public static IList<string> GetAllRoles()
        {
            var list = new List<string>();
            try
            {
                list = Roles.GetAllRoles().ToList();
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return list;
        }

        /// <summary>
        /// Gets user GUID given their username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>User GUID if found.</returns>
        public static Guid GetGuidFromUserName(string username)
        {
            var guid = new Guid(new byte[16]);
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    guid = (db.aspnet_Users.Single(u => u.UserName.Equals(username.ToLowerInvariant()))).UserId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return guid;
        }

        /// <summary>
        /// Gets the password reset question of a user account.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>Password reset question if account exists.</returns>
        public static string GetQuestion(string username)
        {
            var question = string.Empty;
            try
            {
                var membershipUser = Membership.GetUser(username.ToLowerInvariant());
                if (membershipUser != null)
                    question = membershipUser.PasswordQuestion;
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return question;
        }

        /// <summary>
        /// Get user's email given their username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>User's email address if found.</returns>
        public static string GetUserEmail(string username)
        {
            var email = string.Empty;
            try
            {
                var user = Membership.GetUser(username.ToLowerInvariant());
                if (user != null)
                    email = user.Email;
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return email;
        }

        /// <summary>
        /// Gets patient's username given their nric.
        /// </summary>
        /// <param name="nric">User NRIC.</param>
        /// <returns>A string containing the user's real name if found.</returns>
        public static string GetUserNameFromNric(string nric)
        {
            var temp = string.Empty;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    temp = db.aspnet_Users.Single(u => u.UserId.Equals(UserParticularsHandler.GetGuidFromNric(nric))).UserName;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return temp;
        }

        /// <summary>
        /// Gets roles that a user is in.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
        public static IList<string> GetUserRoles(string username)
        {
            var list = new List<string>();
            try
            {
                list = Roles.GetRolesForUser(username.ToLowerInvariant()).ToList();
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return list;
        }

        /// <summary>
        /// Checks whether user is in specified role given their NRIC.
        /// </summary>
        /// <param name="nric">User NRIC.</param>
        /// <param name="role">Role name.</param>
        /// <returns>True if user is in the specified role. False otherwise.</returns>
        public static bool IsInRole(string nric, string role)
        {
            var isInRole = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var username = db.aspnet_Users.Single(u => u.UserId.Equals(UserParticularsHandler.GetGuidFromNric(nric))).UserName;
                    isInRole = Roles.IsUserInRole(username, role);
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return isInRole;
        }

        /// <summary>
        /// Removes user from specified role.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="rolename">Role to remove user from.</param>
        /// <returns>True if the user is successfully removed from a role. False otherwise.</returns>
        public static bool RemoveUserFromRole(string username, string rolename)
        {
            var removed = false;
            try
            {
                Roles.RemoveUserFromRole(username.ToLowerInvariant(), rolename);
                removed = true;
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return removed;
        }

        /// <summary>
        /// Reset the user's password.
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="answer">Answer to user's security question.</param>
        /// <param name="newPassword">String to hold new password.</param>
        /// <returns>True if the password is successfully reset. False otherwise.</returns>
        public static bool ResetPassword(string username, string answer, out string newPassword)
        {
            var success = false;
            newPassword = string.Empty;

            var user = Membership.GetUser(username.ToLowerInvariant());

            if (user == null)
                return false;

            try
            {
                newPassword = user.ResetPassword(answer);
                success = true;
            }
            catch (ArgumentException e)
            {
                newPassword = e.Message;
            }
            catch (MembershipPasswordException e)
            {
                newPassword = e.Message;
            }
            catch (ProviderException e)
            {
                newPassword = e.Message;
            }

            return success;
        }

        /// <summary>
        /// Updates the account information of a user.
        /// </summary>
        /// <param name="username">Username.</param>
        /// /// <param name="email">User's new email address.</param>
        /// <returns>True if update is successful. False otherwise.</returns>
        public static bool UpdateAccount(string username, string email)
        {
            var update = false;
            try
            {
                var user = Membership.GetUser(username.ToLowerInvariant());
                if (user != null)
                {
                    user.Email = email;
                    Membership.UpdateUser(user);
                    update = true;
                }
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return update;
        }

        /// <summary>
        /// Check whether username already exists.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>True if the username is currently in use. False otherwise.</returns>
        public static bool UserNameExists(string username)
        {
            var found = true;
            try
            {
                found = (Membership.GetUser(username.ToLowerInvariant()) != null);
            }
            catch (ArgumentException) { }
            catch (ProviderException) { }
            return found;
        }
    }
}