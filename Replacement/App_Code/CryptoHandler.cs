using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// This class handles hashing on behalf of the entire application.
/// </summary>

public class CryptoHandler
{
    private const string Salt = "RISDB";

    /// <summary>
    /// Hashes a string using a secret salt
    /// </summary>
    /// <param name="data">The string(s) to hash</param>
    /// <returns>The string hashed with the secret salt</returns>
    public static string GetHash(params string[] data)
    {
        using (var hashFunction = MD5.Create())
        {
            // Simply concatenate all the strings together
            var toEncode = new StringBuilder();
            foreach (var s in data)
                toEncode.Append(s);

            // Then calculate a salted MD5 hash using the data
            var hash = new StringBuilder();
            foreach (int i in hashFunction.ComputeHash(Encoding.UTF8.GetBytes(Salt + data + Salt)))
                hash.Append(i.ToString("x2"));
            return hash.ToString();
        }
    }

    /// <summary>
    /// Checks whether a data value checksum is valid.
    /// </summary>
    /// <param name="data">The value(s) of the target data obtained from query string.</param>
    /// <param name="checkSum">The checksum value provided in the query string.</param>
    /// <returns>True if the hash is determined to be valid. False otherwise.</returns>
    public static bool IsHashValid(string checkSum, params string[] data)
    {
        var valid = false;
        var hash = GetHash(data);
        var comparer = StringComparer.OrdinalIgnoreCase;
        if (comparer.Compare(checkSum, hash) == 0)
            valid = true;
        return valid;
    }
}