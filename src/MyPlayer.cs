/**
 * @file MyPlayer
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Data {
    using GCollections = System.Collections.Generic;

    /**
     * An extension class of player for your own player.
     */
    public class MyPlayer : Player {
        private int intAge             = 0; //< The age of the player.
        private int totalCoins         = 0; //< How many coins the player has.
        private int intMemberRemaining = 0; //< How many membership days the player has remaining.
        private int intMinutesPlayed   = 0; //< The minutes the player has spent playing the game.
        private GCollections.List<int> inventoryList = new GCollections.List<int>(); //< The list of inventory items.

        public int Age {
            get { return intAge; }
        }
        public int Coins {
            get { return totalCoins; }
        }
        public int MemberRemaining {
            get { return intMemberRemaining; }
        }
        public int MinutesPlayed {
            get { return intMinutesPlayed; }
        }
        public int[] Inventory {
            get { return inventoryList.ToArray(); }
        }

        /**
         * Sets how many coins the player has.
         *
         * @param coinAmount
         *   The amount of coins that the player has.
         */
        public void SetCoins(int coinAmount) {
            totalCoins = coinAmount;
        }

        /**
         * Adds coins to the total coin amount.
         *
         * @param addTotal
         *   The amount of coins to add.
         */
        public void AddCoins(int addTotal) {
            totalCoins += addTotal;
        }

        /**
         * Subtracts coins from the total coin amount.
         * @param subTotal
         *   The amount of coins to subtract.
         */
        public void SubCoins(int subTotal) {
            totalCoins -= subTotal;
        }

        /**
         * Sets the age of the player.
         *
         * @param totalDays
         *   How many days old the player is.
         */
        public void SetAge(int totalDays) {
            intAge = totalDays;
        }

        /**
         * Sets the remaining member days that the player has.
         *
         * @param totalDays
         *   The amount of days remaining.
         */
        public void SetMemberRemaining(int totalDays) {
            intMemberRemaining = totalDays;
        }

        /**
         * Sets the amount of minutes that the player has played the game.
         *
         * @param totalMinutes
         *   The total amount of minutes that the player has played.
         */
        public void SetMinutesPlayed(int totalMinutes) {
            intMinutesPlayed = totalMinutes;
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
