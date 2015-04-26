using System;

namespace Sharpenguin.Configuration.System {
    /// <summary>
    /// Represents the exception thrown when the library or user tries to use an error that does not exist.
    /// </summary>
    public class NonExistentErrorException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.System.NonExistentErrorException"/> class.
        /// </summary>
        public NonExistentErrorException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.System.NonExistentErrorException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NonExistentErrorException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.System.NonExistentErrorException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public NonExistentErrorException(string message, Exception inner) : base(message, inner) { }

    }
}

