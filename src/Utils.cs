/**
 * @file Utils
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin {

    /**
     * The utility class, a set of useful functions which do not lay in a particular category.
     */
    static class Utils {
        /**
         * Get the ASCII value of the first letter of a string.
         *
         * @param strInput
         *   The input string.
         *
         * @return
         *   The ASCII value of the first letter of the input string.
         */

        public static int ord(string strInput) {
            if(strInput.Length > 0) {
                char[] arrChars = strInput.ToCharArray();
                return (int) arrChars[0];
            }else{
                return 0;
            }
        }

        /**
         * Converts a string to a byte array.
         *
         * @param strInput
         *   The input string to convert.
         *
         * @return
         *   The byte array.
         */
        public static byte[] strToByte(string strInput) {
            return System.Text.Encoding.ASCII.GetBytes(strInput);
        }

        /**
         * Converts a byte array to a string.
         *
         * @param bytInput
         *   The input byte array to convert.
         *
         * @return
         *   The string.
         */
        public static string byteToStr(byte[] bytInput) {
            return System.Text.Encoding.ASCII.GetString(bytInput);
        }

        /**
         * Same as String.IndexOf, except one can specify which incidence to return, if there are multiple.
         *
         * @param strInput
         *   The string to look inside.
         * @param strFind
         *   The string to find in the input string.
         * @param intN
         *   The incidence to return the position of.
         *
         * @return
         *   If found, the position of the string will be returned. If not, -1 will be returned.
         */
        public static int getNth(string strInput, string strFind, int intN) {
            System.Text.RegularExpressions.Match matMatch = System.Text.RegularExpressions.Regex.Match(strInput, "((" + strFind + ").*?){" + intN + "}");
            if (matMatch.Success) {
                return matMatch.Groups[2].Captures[intN - 1].Index;
            }else{
                return -1;
            }
        }

        /**
         * Make the current thread sleep for a specified amount of seconds.
         *
         * @param intSeconds
         *   The amount of seconds to sleep for.
         */
        public static void sleep(int intSeconds) {
            System.Threading.Thread.Sleep(intSeconds * 1000);
        }

        /**
         * Download a string from a webserver.
         *
         * @param strUrl
         *   The URL to download from.
         *
         * @return
         *  The string that was downloaded from the server.
         */
        public static string DownloadString(string strUrl) {
            System.Net.WebClient webDownloader = new System.Net.WebClient();
            return webDownloader.DownloadString(strUrl);
        }

    }
}
