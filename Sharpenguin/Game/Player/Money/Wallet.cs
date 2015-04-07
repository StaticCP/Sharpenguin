namespace Sharpenguin.Game.Player.Money {
    public sealed class Wallet {
        private int amount = 0;

        public int Amount {
            get { return amount; }
            set { amount = value; }
        }
    }
}