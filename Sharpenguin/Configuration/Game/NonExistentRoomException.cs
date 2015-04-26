using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents the exception thrown when the library or user tries to use a room that does not exist in the configuration.
    /// </summary>
    public class NonExistentRoomException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentRoomException"/> class.
        /// </summary>
        public NonExistentRoomException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentRoomException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NonExistentRoomException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentRoomException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public NonExistentRoomException(string message, Exception inner) : base(message, inner) { }

    }
}

