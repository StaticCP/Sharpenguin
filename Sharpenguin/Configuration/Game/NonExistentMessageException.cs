using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents the exception thrown when the library or user tries to use a message which does not exist in the configuration.
    /// </summary>
    public class NonExistentMessageException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentMessageException"/> class.
        /// </summary>
        public NonExistentMessageException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentMessageException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NonExistentMessageException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentMessageException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public NonExistentMessageException(string message, Exception inner) : base(message, inner) { }

    }
}

