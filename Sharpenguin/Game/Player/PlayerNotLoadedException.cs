namespace Sharpenguin.Game.Player {
    /// <summary>
    /// Represents an error given when the user tries to change the details of another player.
    /// </summary>
    public class PlayerNotLoadedException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.PlayerNotLoadedException"/> class.
        /// </summary>
        public PlayerNotLoadedException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.PlayerNotLoadedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public PlayerNotLoadedException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.PlayerNotLoadedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public PlayerNotLoadedException(string message, System.Exception inner) : base(message, inner) { }
    }
}

