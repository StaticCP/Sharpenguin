/**
 * @file Crypt
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * Sharpenguin security, such as password hashing.
 */
namespace Sharpenguin.Security {
    using System.Security.Cryptography;

    public static class Crypt {
        private static string salt = "!£*(s)0edU-++,<.>@";
        public static string HashPassword(string username, string password, string rndk) {
            password = InitialHash(username, password);
            // Get position of the middle of the string (rounds if the middle is not a whole number)
            int halfWay = (int) System.Math.Round((decimal) (password.Length / 2));
            // Get the first and second half of the string
            string halfOne = Hash<SHA256CryptoServiceProvider>(password.Substring(0, halfWay));
            string halfTwo = Hash<SHA256CryptoServiceProvider>(password.Substring(halfWay, halfWay));
            // Hash the password with the random key in order of: second half, random key, first half
            string hash = Hash<SHA256CryptoServiceProvider>(halfTwo + rndk + halfOne);
            return hash;
        }

        private static string InitialHash(string username, string password) {
            string hash = Hash<SHA256CryptoServiceProvider>(password);
            string halfOne = Hash<SHA256CryptoServiceProvider>(hash.Substring(0, 32));
            string halfTwo = Hash<SHA256CryptoServiceProvider>(hash.Substring(32, 32));
            return Hash<SHA256CryptoServiceProvider>(halfOne + salt + username.ToLower() + halfTwo);
        }

        /// <summary>
        /// Hashes a string with a given hash algorithm (using generic parameters)
        /// </summary>
        /// <param name="text">The string to hash.</param>
        public static string Hash<AlgorithmType>(string text) where AlgorithmType : HashAlgorithm, new() {
            string hash = "";
            AlgorithmType algorithm = new AlgorithmType();
            byte[] hashedBytes = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            foreach(byte hashedByte in hashedBytes) hash += hashedByte.ToString("x2");
            return hash;
        }
    }

    /*
    /// <summary>
    /// Class of cryptographic functions.
    /// </summary>
    static class Crypt {
        private const string salt = "Y(02.>'H}t\":E1"; //< Password salt.

        /// <summary>
        /// Hashes password with the random key provided by the server.
        /// </summary>
        /// <returns>The hashed password.</returns>
        /// <param name="password">The plaintext password.</param>
        /// <param name="rndk">The random key.</param>
        public static string HashPassword(string password, string rndk) {
            string key = SwapMD5(password, true).ToUpper();
            key += rndk;
            key += salt;
            key = SwapMD5(key, true);
            return key;
        }

        /// <summary>
        /// Hashes the text in MD5 and swaps the two halves.
        /// </summary>
        /// <returns>The hashed string.</returns>
        /// <param name="plain">The plaintext string.</param>
        /// <param name="blnMd5">If set to <c>true</c>, hash the plaintext into MD5.</param>
        public static string SwapMD5(string plain, bool blnMd5) {
            string hash = plain;
            if(blnMd5) hash = Md5(plain);
            hash = hash.Substring(16, 16) + hash.Substring(0, 16);
            return hash;
        }

        /// <summary>
        /// Hashes the specified plaintext in MD5.
        /// </summary>
        /// <param name="plain">The plaintext.</param>
        private static string Md5(string plain) {
            MD5 algo = MD5.Create();
            byte[] bytData = algo.ComputeHash(Utils.strToByte(plain));
            string hash = "";
            for(int i = 0; i < bytData.Length; i++) {
                hash += bytData[i].ToString("x2");
            }
            return hash;
        }
    }*/
}
