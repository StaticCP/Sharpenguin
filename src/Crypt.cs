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
        private const string strSalt = "Y(02.>'H}t\":E1";

        /**
         * Hashes password with the random key provided by the server.
         *
         * @param strPassword
         *   Password to hash.
         * @param strRndK
         *   Random key provided by the server.
         *
         * @return
         *   Hashed password.
         */
        public static string hashPassword(string strPassword, string strRndK) {
            string strKey = subMd5(strPassword, true).ToUpper();
            strKey += strRndK;
            strKey += strSalt;
            strKey = subMd5(strKey, true);
            return strKey;
        }

        /**
         * Swaps the two halves of an MD5 hash
         *
         * Gets the last 16 characters and first 16 characters of an MD5 hash and puts them together, effectively swapping the two halves. Optionally, you can also hash the input string.
         *
         * @param strIn
         *   The input string.
         * @param blnMd5
         *   Whether to hash the input string or not.
         *
         * @return
         *   Output string.
         */
        public static string subMd5(string strIn, bool blnMd5) {
            if(blnMd5) {
                strIn = md5(strIn);
            }
            string strOut = strIn.Substring(16, 16) + strIn.Substring(0, 16);
            return strOut;
        }

        /**
         * Hashes a string in md5.
         *
         * @param strIn
         *  The input string.
         *
         * @return
         *  The output string, which is the MD5 hash of the input string.
         */
        private static string md5(string strIn) {
            MD5 objHash = MD5.Create();
            byte[] bytData = objHash.ComputeHash(Utils.strToByte(strIn));
            string strHash = "";
            for(int intLoops = 0; intLoops < bytData.Length; intLoops++) {
                strHash += bytData[intLoops].ToString("x2");
            }
            return strHash;
        }
    }
}
