namespace Sharpenguin.Game.Player.Money {
    /// <summary>
    /// Represents a base exception within the library.
    /// </summary>
    public class NotEnoughCoinsException : PenguinException {
        public NotEnoughCoinsException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.Money.NotEnoughCoinsException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NotEnoughCoinsException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.Money.NotEnoughCoinsException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="inner">Inner exception.</param>
        public NotEnoughCoinsException(string message, NotEnoughCoinsException inner) : base(message, inner) { }
    }
}

