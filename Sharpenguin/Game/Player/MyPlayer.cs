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
        private GCollections.List<int> inventoryList = new GCollections.List<int>(); //< The list of inventory items.

        //! Gets the player's age.
        public int Age {
            get { return age; }
        }
        public Money.Wallet Wallet {
            get { return wallet; }
        }
        //! Gets the amount membership days remaining for the player.
        public int MemberRemaining {
            get { return memberRemaining; }
        }
        //! Gets the amount of minutes played by the player.
        public int MinutesPlayed {
            get { return minutes; }
        }
        //! Gets an array of the player's inventory.
        public int[] Inventory {
            get { return inventoryList.ToArray(); }
        }

        /**
         * Sets the age of the player.
         *
         * @param totalDays
         *   How many days old the player is.
         */
        public void SetAge(int totalDays) {
            age = totalDays;
        }

        /**
         * Sets the remaining member days that the player has.
         *
         * @param totalDays
         *   The amount of days remaining.
         */
        public void SetMemberRemaining(int totalDays) {
            memberRemaining = totalDays;
        }

        /**
         * Sets the amount of minutes that the player has played the game.
         *
         * @param totalMinutes
         *   The total amount of minutes that the player has played.
         */
        public void SetMinutesPlayed(int totalMinutes) {
            minutes = totalMinutes;
        }

        /**
         * Adds an item to the inventory list.
         *
         * @param itemId
         *   The id of the item to add.
         */
        public void AddInventoryItem(int itemId) {
            inventoryList.Add(itemId);
        }

    }

}
