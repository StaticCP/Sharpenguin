namespace Sharpenguin.Game.Player.Money {
    /// <summary>
    /// Represents the coins a player has.
    /// </summary>
    public sealed class Wallet {
        /// <summary>
        /// The amount of coins the player has .
        /// </summary>
        private int amount = 0;

        /// <summary>
        /// Gets or sets the amount of coins.
        /// </summary>
        /// <value>The amount of coins.</value>
        public int Amount {
            get { return amount; }
            set { amount = value; }
        }
    }
}