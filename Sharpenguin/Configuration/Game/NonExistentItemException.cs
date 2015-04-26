using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents the exception thrown when the library or user tries to use a item that does not exist.
    /// </summary>
    public class NonExistentItemException : PenguinException {
            /// <summary>
            /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentItemException"/> class.
            /// </summary>
            public NonExistentItemException() : base() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentItemException"/> class.
            /// </summary>
            /// <param name="message">Exception message.</param>
            public NonExistentItemException(string message) : base(message) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.NonExistentItemException"/> class.
            /// </summary>
            /// <param name="message">Exception message.</param>
            /// <param name="inner">Inner exception.</param>
            public NonExistentItemException(string message, Exception inner) : base(message, inner) { }

    }
}

