/**
 * @file MyPlayer
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Game.Player {
    using GCollections = System.Collections.Generic;

    /**
     * An extension class of player for your own player.
     */
    public class MyPlayer : Player {
        private int age             = 0; //< The age of the player.
        private int memberRemaining = 0; //< How many membership days the player has remaining.
        private int minutes         = 0; //< The minutes the player has spent playing the game.
        private Money.Wallet wallet = new Money.Wallet();
        private Inventory.Inventory inventory;
        private GameConnection connection;

        //! Gets the player's age.
        public int Age {
            get { return age; }
            internal set { age = value; }
        }
        public Money.Wallet Wallet {
            get { return wallet; }
        }
        //! Gets the amount membership days remaining for the player.
        public int MemberRemaining {
            get { return memberRemaining; }
            internal set { memberRemaining = value; }
        }
        //! Gets the amount of minutes played by the player.
        public int MinutesPlayed {
            get { return minutes; }
            internal set { minutes = value; }
        }

        /// <summary>
        /// Gets the player's inventory.
        /// </summary>
        /// <value>The inventory.</value>
        public Inventory.Inventory Inventory {
            get { return inventory; }
        }

        /// <summary>
        /// Gets the player's connection.
        /// </summary>
        /// <value>The connection.</value>
        public GameConnection Connection {
            get { return connection; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.MyPlayer"/> class.
        /// </summary>
        /// <param name="connection">They player's parent connection.</param>
        public MyPlayer(GameConnection connection) {
            this.connection = connection;
            inventory = new Inventory.Inventory(this);
        }

    }

}
