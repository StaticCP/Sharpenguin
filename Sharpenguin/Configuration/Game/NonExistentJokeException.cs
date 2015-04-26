using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents the exception thrown when the library or user tries to use a joke which is not in the configuration.
    /// </summary>
    public class NonExistentJokeException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentJokeException"/> class.
        /// </summary>
        public NonExistentJokeException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentJokeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NonExistentJokeException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentJokeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public NonExistentJokeException(string message, Exception inner) : base(message, inner) { }

    }
}

