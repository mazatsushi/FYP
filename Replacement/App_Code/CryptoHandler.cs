using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// This class handles hashing on behalf of the entire application.
/// </summary>

public class CryptoHandler
{
    private const string Salt = "RISDB_";

    /// <summary>
    /// Hashes a string using a secret salt
    /// </summary>
    /// <param name="data">The string to hash</param>
    /// <returns>The string hashed with the secret salt</returns>
    public static string GetHash(string data)
    {
        using (var hashFunction = MD5.Create())
        {
            var hash = new StringBuilder();
            foreach (int i in hashFunction.ComputeHash(Encoding.UTF8.GetBytes(Salt + data)))
                hash.Append(i.ToString("x2"));
            return hash.ToString();
        }
    }

    /// <summary>
    /// Checks whether a data value checksum is valid.
    /// </summary>
    /// <param name="data">The value of the target data obtained from query string.</param>
    /// <param name="checkSum">The checksum value provided in the query string.</param>
    /// <returns>True if the hash is determined to be valid. False otherwise.</returns>
    public static bool CheckHash(string data, string checkSum)
    {
        var valid = false;
        var hash = GetHash(data);
        var comparer = StringComparer.OrdinalIgnoreCase;
        if (comparer.Compare(checkSum, hash) == 0)
            valid = true;
        return valid;
    }
}