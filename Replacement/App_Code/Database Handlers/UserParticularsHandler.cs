using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DB_Handlers
{
    /// <summary>
    /// Class that interacts only with the UserParticulars table in database.
    /// </summary>
    public class UserParticularsHandler
    {
        /// <summary>
        /// Gets a user's full name given their NRIC.
        /// </summary>
        /// <param name="nric">User's NRIC.</param>
        /// <returns>The user's full name if NRIC is found.</returns>
        public static string GetFullName(string nric)
        {
            var fullName = new StringBuilder();
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var temp = (db.UserParticulars.Single(p => p.NRIC.Equals(nric.ToUpperInvariant())));
                    fullName.Append(temp.Prefix);
                    fullName.Append(" ");
                    fullName.Append(temp.FirstName);
                    fullName.Append(" ");
                    fullName.Append(temp.LastName);
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return fullName.ToString();
        }

        /// <summary>
        /// Gets user GUID given their nric.
        /// </summary>
        /// <param name="nric">User NRIC.</param>
        /// <returns>User GUID if found.</returns>
        public static Guid GetGuidFromNric(string nric)
        {
            var guid = new Guid(new byte[16]);
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    guid = (db.UserParticulars.Single(u => u.NRIC.Equals(nric.ToUpperInvariant()))).UserId;
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return guid;
        }

        /// <summary>
        /// Gets user particulars given their username.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>User's particulars if found.</returns>
        public static UserParticular GetParticularsFromUsername(string username)
        {
            UserParticular temp = null;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    temp = db.UserParticulars.Single(u => u.UserId.Equals(MembershipHandler.GetGuidFromUserName(username)));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return temp;
        }

        /// <summary>
        /// Checks whether NRIC already exists.
        /// </summary>
        /// <param name="nric">User NRIC.</param>
        /// <returns>True if the NRIC is currently in use. False otherwise.</returns>
        public static bool NricExists(string nric)
        {
            var exists = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    exists = (db.UserParticulars.Any(u => u.NRIC.Equals(nric.ToUpperInvariant())));
                }
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return exists;
        }

        /// <summary>
        /// Updates user particulars.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="middleName">Middle name (if any).</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="namePrefix">Salutation.</param>
        /// <param name="nameSuffix">Name suffix (if any).</param>
        /// <param name="address">Address.</param>
        /// <param name="contact">Contact number.</param>
        /// <param name="postalCode">Postal code.</param>
        /// <param name="countryId">Country ID.</param>
        /// <param name="nationality">Nationality.</param>
        /// <returns>True if update is successful. False otherwise.</returns>
        public static bool UpdateParticulars(string username, string firstName, string middleName, string lastName, string namePrefix, string nameSuffix, string address, string contact, string postalCode, int countryId, string nationality)
        {
            var success = false;
            try
            {
                var text = new CultureInfo("en-SG").TextInfo;
                using (var db = new RIS_DB_Entities())
                {
                    var user = db.UserParticulars.Single(u => u.UserId.Equals(MembershipHandler.GetGuidFromUserName(username)));
                    user.FirstName = text.ToTitleCase(firstName);
                    user.MiddleName = middleName;
                    user.LastName = text.ToTitleCase(lastName);
                    user.Prefix = text.ToTitleCase(namePrefix);
                    user.Suffix = nameSuffix;
                    user.Address = text.ToTitleCase(address);
                    user.ContactNumber = contact;
                    user.PostalCode = postalCode;
                    user.CountryOfResidence = countryId;
                    user.Nationality = text.ToTitleCase(nationality);
                    db.SubmitChanges();
                }
                success = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return success;
        }

        /// <summary>
        /// Updates user particulars.
        /// </summary>
        /// <param name="userId">User GUID.</param>
        /// <param name="nric">NRIC.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="middleName">Middle name (if any).</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="gender">Gender.</param>
        /// <param name="namePrefix">Salutation.</param>
        /// <param name="nameSuffix">Name suffix (if any).</param>
        /// <param name="dob">Date of Birth.</param>
        /// <param name="address">Addres.s</param>
        /// <param name="contact">Contact number.</param>
        /// <param name="postalCode">Postal code.</param>
        /// <param name="countryId">Country ID.</param>
        /// <param name="nationality">Nationality.</param>
        /// <returns>True if update is successful. False otherwise.</returns>
        public static bool UpdateParticulars(Guid userId, string nric, string firstName, string middleName, string lastName, string gender,
            string namePrefix, string nameSuffix, DateTime dob, string address, string contact, string postalCode, int countryId,
            string nationality)
        {
            var success = false;
            try
            {
                using (var db = new RIS_DB_Entities())
                {
                    var text = new CultureInfo("en-SG").TextInfo;
                    db.UserParticulars.InsertOnSubmit(new UserParticular
                    {
                        NRIC = nric.ToUpperInvariant(),
                        FirstName = text.ToTitleCase(firstName),
                        MiddleName = middleName,
                        LastName = text.ToTitleCase(lastName),
                        Gender = Char.Parse(text.ToTitleCase(gender)),
                        Prefix = text.ToTitleCase(namePrefix),
                        Suffix = nameSuffix,
                        DateOfBirth = dob,
                        Address = text.ToTitleCase(address),
                        ContactNumber = contact,
                        PostalCode = postalCode,
                        CountryOfResidence = countryId,
                        Nationality = text.ToTitleCase(nationality),
                        UserId = userId
                    });
                    db.SubmitChanges();
                }
                success = true;
            }
            catch (ArgumentException) { }
            catch (InvalidOperationException) { }
            return success;
        }
    }
}