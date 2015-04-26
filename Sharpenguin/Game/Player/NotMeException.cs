namespace Sharpenguin.Game.Player {
    /// <summary>
    /// Represents an error given when the user tries to change the details of another player.
    /// </summary>
    public class NotMeException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.NotMeException"/> class.
        /// </summary>
        public NotMeException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.NotMeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NotMeException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.NotMeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public NotMeException(string message, System.Exception inner) : base(message, inner) { }
    }
}

