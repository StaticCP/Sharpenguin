namespace Sharpenguin {
    /// <summary>
    /// Represents a base exception within the library.
    /// </summary>
    public class PenguinException : System.Exception {
        public PenguinException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.PenguinException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public PenguinException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.PenguinException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="inner">Inner.</param>
        public PenguinException(string message, System.Exception inner) : base(message, inner) { }
    }
}

