namespace Sharpenguin.Packets.Receive.Xt {
    /// <summary>
    /// Thrown when the XtParser fails to parse a string.
    /// </summary>
    public class InvalidXtException : PenguinException {
        public InvalidXtException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Receive.Xt.InvalidXtException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Exception message.</param>
        public InvalidXtException(string exceptionMessage) : base(exceptionMessage) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Receive.Xt.InvalidXtException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidXtException(string exceptionMessage, InvalidXtException innerException) : base(exceptionMessage, innerException) { }
    }
}
