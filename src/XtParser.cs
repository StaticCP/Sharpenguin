/**
 * @file XtParser
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * Contains classes which handle Xt data within Sharpenguin.
 */
namespace Sharpenguin.Xt {
    /*
     * Everyone told me making this class was pointless...
     * ... I proved them wrong. :')
     */

    /**
     * Parses xt packets for easier handling.
     */
    public class XtParser {
        private int intRoom; //< Room that the packet came from.
        private string strCommand; //< Packet's command, which is used to determine the handler.
        private string[] arrArguments; //< Packet's arguments.

        //! Gets the ID of the room this Xt string came from (Internal Room).
        public int Room {
            get { return intRoom; }
        }
        //! Gets the command of the Xt string.
        public string Command {
            get { return strCommand; }
        }
        //! Gets the arguments of the Xt string.
        public string[] Arguments {
            get { return arrArguments; }
        }


        /**
         * Loads an xt string into the object.
         *
         * @param strData
         *   The xt string.
         */
        public void LoadXt(string strData) {
            if(strData == null) throw new System.ArgumentException("Parameter cannot be null.", "strData");
            if(isXt(strData)) {
                strCommand = getCommand(strData);
                intRoom = getRoom(strData);
                arrArguments = getArguments(strData);
            }else{
                throw new Exceptions.InvalidXtException("The supplied data is not a valid Xt packet!");
            }
        }


        /**
         * Determines wether the string is an xt string or not.
         *
         * @param strData
         *   The xt string.
         *
         * @return TRUE if the string is an xt string, FALSE if not.
         */
        private bool isXt(string strData) {
            if(strData.IndexOf("%") != -1) {
                string[] arrData = strData.Split("%".ToCharArray());
                if(arrData.Length >= 2 && arrData[1] == "xt") {
                    return true;
                }else{
                    return false;
                }
            }
            return false;
        }

        /**
         * Gets the command from the xt string.
         *
         * @param strData
         *   The xt string to get the command from.
         *
         * @return
         *   The xt command.
         */
        private string getCommand(string strData) {
            if(strData.IndexOf("%") != -1) {
                string[] arrData = strData.Split("%".ToCharArray());
                if(arrData.Length >= 3) {
                    return arrData[2];
                }else{
                    throw new Exceptions.InvalidXtException("Could not load Xt Command.");
                }
            }
            throw new Exceptions.InvalidXtException("Could not load Xt Command.");
        }

        /**
         * Gets external room id from the xt string
         *
         * @param strData
         *   The xt string to get the room from.
         *
         * @return
         *   External room id.
         */
        private int getRoom(string strData) {
            if(strData.IndexOf("%") != -1) {
                string[] arrData = strData.Split("%".ToCharArray());
                if(arrData.Length >= 4) {
                    return System.Convert.ToInt32(arrData[3]);
                }else{
                    throw new Exceptions.InvalidXtException("Could not load Xt Room.");
                }
            }
            throw new Exceptions.InvalidXtException("Could not load Xt Room.");
        }

        /**
         * Takes the arguments from the xt string and puts them into a string array.
         *
         * @param strData
         *   The xt string to get the arguments from.
         *
         * @return
         *   Array of arguments.
         */
        private string[] getArguments(string strData) {
            try {
                return strData.Substring(Utils.getNth(strData, "%", 4) + 1).Split("%".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
                
            }catch{
                throw new Exceptions.InvalidXtException("Could not load Xt Arguments.");
            }
        }


    }


}
