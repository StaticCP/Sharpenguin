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
        private GCollections.List<int> inventoryList = new GCollections.List<int>(); //< The list of inventory items.
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
        //! Gets an array of the player's inventory.
        public int[] Inventory {
            get { return inventoryList.ToArray(); }
        }
        public GameConnection Connection {
            get { return connection; }
        }

        public MyPlayer(GameConnection connection) {
            this.connection = connection;
            inventory = new Inventory.Inventory(this);
        }

    }

}
