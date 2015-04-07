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

    /**
     * Crypt class for managing password and login key hashing.
     */
    static class Crypt {
        private const string salt = "Y(02.>'H}t\":E1"; //< Password salt.

        /**
         * Hashes password with the random key provided by the server.
         *
         * @param password
         *   Password to hash.
         * @param rndk
         *   Random key provided by the server.
         *
         * @return
         *   Hashed password.
         */
        public static string HashPassword(string password, string rndk) {
            string key = SwapMD5(password, true).ToUpper();
            key += rndk;
            key += salt;
            key = SwapMD5(key, true);
            return key;
        }

        /**
         * Swaps the two halves of an MD5 hash
         *
         * Gets the last 16 characters and first 16 characters of an MD5 hash and puts them together, effectively swapping the two halves. Optionally, you can also hash the input string.
         *
         * @param plain
         *   The input string.
         * @param blnMd5
         *   Whether to hash the input string or not.
         *
         * @return
         *   Output string.
         */
        public static string SwapMD5(string plain, bool blnMd5) {
            string hash = plain;
            if(blnMd5) hash = Md5(plain);
            hash = hash.Substring(16, 16) + hash.Substring(0, 16);
            return hash;
        }

        /**
         * Hashes a string in md5.
         *
         * @param plain
         *  The input string.
         *
         * @return
         *  The output string, which is the MD5 hash of the input string.
         */
        private static string Md5(string plain) {
            MD5 algo = MD5.Create();
            byte[] bytData = algo.ComputeHash(Utils.strToByte(plain));
            string hash = "";
            for(int i = 0; i < bytData.Length; i++) {
                hash += bytData[i].ToString("x2");
            }
            return hash;
        }
    }
}
