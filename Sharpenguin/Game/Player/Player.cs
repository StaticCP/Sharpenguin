/**
 * @file Player
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Game.Player {

    /**
     * The player class. Information about players in the room are stored here.
     */
    public class Player {
        /// <summary>
        /// The player's items.
        /// </summary>
        private Items items       = new Items();
        /// <summary>
        /// The player's position.
        /// </summary>
        private Position position = new Position();
        /// <summary>
        /// The player's ID.
        /// </summary>
        private int id            = 0;
        /// <summary>
        /// The amount of days the player has been a member for.
        /// </summary>
        private int memberDays    = 0;
        /// <summary>
        /// The time zone offset.
        /// </summary>
        private int tzo           = 0;
        /// <summary>
        /// The player's username.
        /// </summary>
        private string username   = "";
        /// <summary>
        /// Whether the player is a member.
        /// </summary>
        private bool isMember     = false;

        /// <summary>
        /// Gets the player's ID.
        /// </summary>
        /// <value>The player's ID.</value>
        public int Id {
            get { return id; }
        }

        /// <summary>
        /// Gets the amount of days the player has been a member.
        /// </summary>
        /// <value>The amount of days the player has been a member.</value>
        public int MemberDays {
            get { return memberDays; }
        }
        /// <summary>
        /// Gets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        public int TimeZoneOffset {
            get { return tzo; }
        }

        /// <summary>
        /// Gets the player's username.
        /// </summary>
        /// <value>The player's username.</value>
        public string Username {
            get { return username; }
        }

        /// <summary>
        /// Gets a value indicating whether this player is a member.
        /// </summary>
        /// <value><c>true</c> if this player is a member; otherwise, <c>false</c>.</value>
        public bool IsMember {
            get { return isMember; }
        }

        /// <summary>
        /// Gets player's the items.
        /// </summary>
        /// <value>The player's items.</value>
        public Items Items {
            get { return items; }
        }

        /// <summary>
        /// Gets the player's position.
        /// </summary>
        /// <value>The player's position.</value>
        public Position Position {
            get { return position; }
        }
            
        public void LoadData(string data) {
            string[] arr = data.Split("|".ToCharArray());
            id = int.Parse(arr[0]);
            username = arr[1];
            isMember = (int.Parse(arr[15]) != 0);
            memberDays = int.Parse(arr[16]);
            LoadItems(arr);
            LoadPosition(arr);
        }

        private void LoadItems(string[] arr) {
            items.Colour = int.Parse(arr[3]);
            items.Head = int.Parse(arr[4]);
            items.Face = int.Parse(arr[5]);
            items.Neck = int.Parse(arr[6]);
            items.Body = int.Parse(arr[7]);
            items.Hand = int.Parse(arr[8]);
            items.Feet = int.Parse(arr[9]);
            items.Flag = int.Parse(arr[10]);
            items.Background = int.Parse(arr[11]);
        }

        private void LoadPosition(string[] arr) {
            position.X = int.Parse(arr[12]);
            position.Y = int.Parse(arr[13]);
            position.Frame = int.Parse(arr[14]);
        }
    }
}
