namespace Sharpenguin {
    /// <summary>
    /// Represents an error given when the user tries to get the server the connection is connected to when it is not connected.
    /// </summary>
    public class NotConnectedException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.NotConnectedException"/> class.
        /// </summary>
        public NotConnectedException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.NotConnectedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public NotConnectedException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.NotConnectedException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public NotConnectedException(string message, System.Exception inner) : base(message, inner) { }
    }
}

