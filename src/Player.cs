/**
 * @file Player
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Data {
    using Convert = System.Convert;

    /**
     * The player class. Information about players in the room are stored here.
     */
    public class Player {
        private PlayerItem playerItems; //< PlayerItem object to store the player's items.
        private PlayerPosition playerPosition; //< Player position object to store the player's position and frame.
        private int intId              = 0; //< The player's id.
        private int intMemberDays      = 0; //< How many days the player has been a member for.
        private int intTimeZoneOffset  = 0; //< The time offset of the player.
        private string strName         = ""; //< The name of the player.
        private bool blnIsMember       = false; //< Whether the player is a member or not.

        public int Id {
            get { return intId; }
        }
        public int MemberDays {
            get { return intMemberDays; }
        }
        public int TimeZoneOffset {
            get { return intTimeZoneOffset; }
        }
        public string Username {
            get { return strName; }
        }
        public bool IsMember {
            get { return blnIsMember; }
        }
        public PlayerItem Item {
            get { return playerItems; }
        }

        public PlayerPosition Position {
            get { return playerPosition; }
        }

        /**
         * Constructor for the player class, loads the player data from the player string.
         */
        public void LoadData(string strData) {
            string[] arrData = strData.Split("|".ToCharArray());
            intId = Convert.ToInt32(arrData[0]);
            strName = arrData[1];
            blnIsMember = (Convert.ToInt32(arrData[15]) != 0);
            intMemberDays = Convert.ToInt32(arrData[16]);
            LoadItems(arrData);
            LoadPosition(arrData);
        }

        private void LoadItems(string[] arrData) {
            playerItems = new PlayerItem();
            playerItems.SetColour(Convert.ToInt32(arrData[3]));
            playerItems.SetHead(Convert.ToInt32(arrData[4]));
            playerItems.SetFace(Convert.ToInt32(arrData[5]));
            playerItems.SetNeck(Convert.ToInt32(arrData[6]));
            playerItems.SetBody(Convert.ToInt32(arrData[7]));
            playerItems.SetHand(Convert.ToInt32(arrData[8]));
            playerItems.SetFeet(Convert.ToInt32(arrData[9]));
            playerItems.SetFlag(Convert.ToInt32(arrData[10]));
            playerItems.SetPhoto(Convert.ToInt32(arrData[11]));
        }

        private void LoadPosition(string[] arrData) {
            playerPosition = new PlayerPosition();
            playerPosition.SetX(Convert.ToInt32(arrData[12]));
            playerPosition.SetY(Convert.ToInt32(arrData[13]));
            playerPosition.SetFrame(Convert.ToInt32(arrData[14]));
        }
    }

    /**
     * Player item class, items are stored here.
     */
    public class PlayerItem {
        private System.Collections.Generic.Dictionary<string, int> dicItems = new System.Collections.Generic.Dictionary<string, int>();
        private string[] arrCodes = new string[] {"upc", "uph", "upf", "upn", "upb", "upa", "upe", "upl", "upp"};
        public int Colour {
            get { return dicItems["upc"]; }
        }
        public int Head {
            get { return dicItems["uph"]; }
        }
        public int Face {
            get { return dicItems["upf"]; }
        }
        public int Neck {
            get { return dicItems["upn"]; }
        }
        public int Body {
            get { return dicItems["upb"]; }
        }
        public int Hand {
            get { return dicItems["upa"]; }
        }
        public int Feet {
            get { return dicItems["upe"]; }
        }
        public int Flag {
            get { return dicItems["upl"]; }
        }
        public int Photo {
            get { return dicItems["upp"]; }
        }

        public PlayerItem() {
            foreach(string strCode in arrCodes) {
                dicItems[strCode] = 0;
            }
        }

        public void SetColour(int intId) {
            dicItems["upc"] = intId;
        }

        public void SetHead(int intId) {
            dicItems["uph"] = intId;
        }

        public void SetFace(int intId) {
            dicItems["upf"] = intId;
        }

        public void SetNeck(int intId) {
            dicItems["upn"] = intId;
        }

        public void SetBody(int intId) {
            dicItems["upb"] = intId;
        }

        public void SetHand(int intId) {
            dicItems["upa"] = intId;
        }

        public void SetFeet(int intId) {
            dicItems["upe"] = intId;
        }

        public void SetFlag(int intId) {
            dicItems["upl"] = intId;
        }

        public void SetPhoto(int intId) {
            dicItems["upp"] = intId;
        }

        public bool SetByCode(string strCode, int intId) {
            if(dicItems.ContainsKey(strCode)) {
                dicItems[strCode] = intId;
                return true;
            }else{
                return false;
            }
        }
    }

    public class PlayerPosition {
        private int intX     = 0; //< X Position
        private int intY     = 0; //< Y Position
        private int intFrame = 0; //< Frame number.
        public int X {
            get { return intX; }
        }
        public int Y {
            get { return intY; }
        }
        public int Frame {
            get { return intFrame; }
        }

        public void SetX(int newX) {
            intX = newX;
        }

        public void SetY(int newY) {
            intY = newY;
        }

        public void SetFrame(int newFrame) {
            intFrame = newFrame;
        }
    }
}
