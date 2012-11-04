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

        //! Gets the player's ID.
        public int Id {
            get { return intId; }
        }
        //! Gets the amount of days that this player has been a member.
        public int MemberDays {
            get { return intMemberDays; }
        }
        //! Gets the player's time zone offset.
        public int TimeZoneOffset {
            get { return intTimeZoneOffset; }
        }
        //! Get's the player's username.
        public string Username {
            get { return strName; }
        }
        //! Gets whether the player is a member or not.
        public bool IsMember {
            get { return blnIsMember; }
        }
        //! Gets the player's items.
        public PlayerItem Item {
            get { return playerItems; }
        }
        //! Get's the player's position object.
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
        //! Gets the player's colour.
        public int Colour {
            get { return dicItems["upc"]; }
        }
        //! Gets the player's head item.
        public int Head {
            get { return dicItems["uph"]; }
        }
        //! Gets the player's face item.
        public int Face {
            get { return dicItems["upf"]; }
        }
        //! Gets the player's neck item.
        public int Neck {
            get { return dicItems["upn"]; }
        }
        //! Gets the player's body item.
        public int Body {
            get { return dicItems["upb"]; }
        }
        //! Gets the player's hand item.
        public int Hand {
            get { return dicItems["upa"]; }
        }
        //! Gets the player's feet item.
        public int Feet {
            get { return dicItems["upe"]; }
        }
        //! Gets the player's flag (pin).
        public int Flag {
            get { return dicItems["upl"]; }
        }
        //! Gets the player's photo (background).
        public int Photo {
            get { return dicItems["upp"]; }
        }

        /**
         * PlayerItem constructor.
         */
        public PlayerItem() {
            foreach(string strCode in arrCodes) {
                dicItems[strCode] = 0;
            }
        }

        /**
         * Sets the players colour.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetColour(int intId) {
            dicItems["upc"] = intId;
        }

        /**
         * Sets the players head.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetHead(int intId) {
            dicItems["uph"] = intId;
        }

        /**
         * Sets the players face.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetFace(int intId) {
            dicItems["upf"] = intId;
        }

        /**
         * Sets the players neck.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetNeck(int intId) {
            dicItems["upn"] = intId;
        }

        /**
         * Sets the players body.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetBody(int intId) {
            dicItems["upb"] = intId;
        }

        /**
         * Sets the players hand.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetHand(int intId) {
            dicItems["upa"] = intId;
        }

        /**
         * Sets the players feet.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetFeet(int intId) {
            dicItems["upe"] = intId;
        }

        /**
         * Sets the players flag (pin).
         *
         * @param intId
         *  ID of the item.
         */
        public void SetFlag(int intId) {
            dicItems["upl"] = intId;
        }

        /**
         * Sets the players photo.
         *
         * @param intId
         *  ID of the item.
         */
        public void SetPhoto(int intId) {
            dicItems["upp"] = intId;
        }

        /**
         * Sets a player item by the code of the item type.
         *
         * @param strCode
         *  Item type code.
         *
         * @param intId
         *  The ID of the item.
         */
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
        
        //! Gets the player's X position.
        public int X {
            get { return intX; }
        }
        //! Gets the pleyer's Y position.
        public int Y {
            get { return intY; }
        }
        //! Gets the player's frame number.
        public int Frame {
            get { return intFrame; }
        }

        /**
         * Sets the X position of the player.
         *
         * @param newX
         *   The new X position of the player.
         */
        public void SetX(int newX) {
            intX = newX;
        }

        /**
         * Sets the Y position of the player.
         *
         * @param newY
         *  The new Y position of the player.
         */
        public void SetY(int newY) {
            intY = newY;
        }

        /**
         * Sets the frame number the player is currently in.
         *
         * @param newFrame
         *   The number of the new frame.
         */
        public void SetFrame(int newFrame) {
            intFrame = newFrame;
        }
    }
}
