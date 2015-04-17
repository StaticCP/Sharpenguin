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
        private Appearance.Clothing items;
        /// <summary>
        /// The player's position.
        /// </summary>
        private Position position;
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
        public Appearance.Clothing Clothing {
            get { return items; }
        }

        /// <summary>
        /// Gets the player's position.
        /// </summary>
        /// <value>The player's position.</value>
        public Position Position {
            get { return position; }
        }

        protected Player() {
            position = new Position(this);
        }

        public Player(string data) {
            position = new Position(this);
            LoadData(data);
        }

        protected void LoadData(string data) {
            string[] arr = data.Split("|".ToCharArray());
            id = int.Parse(arr[0]);
            username = arr[1];
            isMember = (int.Parse(arr[15]) != 0);
            memberDays = int.Parse(arr[16]);
            LoadItems(arr);
            LoadPosition(arr);
        }

        private void LoadItems(string[] arr) {
            items = new Appearance.Clothing(this);
            items.colour = int.Parse(arr[3]);
            items.head = int.Parse(arr[4]);
            items.face = int.Parse(arr[5]);
            items.neck = int.Parse(arr[6]);
            items.body = int.Parse(arr[7]);
            items.hand = int.Parse(arr[8]);
            items.feet = int.Parse(arr[9]);
            items.flag = int.Parse(arr[10]);
            items.background = int.Parse(arr[11]);
        }

        private void LoadPosition(string[] arr) {
            position.X = int.Parse(arr[12]);
            position.Y = int.Parse(arr[13]);
            position.frame = int.Parse(arr[14]);
        }
    }
}
