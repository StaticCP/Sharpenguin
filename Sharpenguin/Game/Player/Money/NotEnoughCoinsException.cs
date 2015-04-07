namespace Sharpenguin.Game.Player.Money {
    /// <summary>
    /// Represents a base exception within the library.
    /// </summary>
    public class NotEnoughCoinsException : System.Exception {
        public NotEnoughCoinsException() : base() { }
        public NotEnoughCoinsException(string message) : base(message) { }
        public NotEnoughCoinsException(string message, NotEnoughCoinsException inner) : base(message, inner) { }
    }
}

