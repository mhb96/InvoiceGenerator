using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Exception;
using System.Text.RegularExpressions;

namespace InvoiceGenerator.Common.Extensions
{
    /// <summary>
    /// Extensions for validating input.
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Validates string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strName"></param>
        /// <param name="limit"></param>
        /// <exception cref="IGException"></exception>
        public static void ValidateString(this string str, string strName, string regexPattern, int limit)
        {
            if (str == null)
                return;
            ValidateLength(str, strName, limit);
            ValidateStringPattern(str, strName, regexPattern);
        }

        /// <summary>
        /// Checks if string of valid length
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strName"></param>
        /// <param name="limit"></param>
        /// <exception cref="IGException"></exception>
        public static string ValidateLength(string str, string strName, int limit)
        {
            if (str.Length > limit)
                throw new IGException($"{strName}: {str} exceeds {limit} characters.");
            return str;
        }

        /// <summary>
        /// Checks if string contains valid characters.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strName"></param>
        /// <param name="pattern"></param>
        /// <exception cref="IGException"></exception>
        public static string ValidateStringPattern(string str, string strName, string pattern)
        {
            try
            {
                if (!Regex.IsMatch(str, pattern))
                {
                    throw new IGException($"{strName}: {str} does not contain valid characters.");
                }
            }
            catch
            {
                throw new IGException($"{strName}: {str} does not contain valid characters.");
            }
            return str;
        }

        /// <summary>
        /// Checks if email is valid.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strName"></param>
        /// <param name="pattern"></param>
        /// <exception cref="IGException"></exception>
        public static void ValidateEmail(string email)
        {
            if (email == null) 
                return;
            if (email.EndsWith(".") || email.StartsWith("."))
            {
                throw new IGException($"Email: {email} is invalid.");
            }
            ValidateLength(email, "Email", InputLengthLimits.Email);
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                throw new IGException($"Email: {email} is invalid.");
            }
        }
    }
}
