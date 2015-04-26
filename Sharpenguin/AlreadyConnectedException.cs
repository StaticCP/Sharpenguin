namespace Sharpenguin {
    /// <summary>
    /// Represents an error given when the user tries to connect to a server when we are already connected.
    /// </summary>
    public class AlreadyConnectedException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.AlreadyConnectedException"/> class.
        /// </summary>
        public AlreadyConnectedException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.AlreadyConnectedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public AlreadyConnectedException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.AlreadyConnectedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public AlreadyConnectedException(string message, System.Exception inner) : base(message, inner) { }
    }
}

