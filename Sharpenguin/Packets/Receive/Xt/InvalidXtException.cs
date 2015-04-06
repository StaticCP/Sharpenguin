namespace Sharpenguin.Packets.Receive.Xt {
    /// <summary>
    /// Thrown when the XtParser fails to parse a string.
    /// </summary>
    public class InvalidXtException : PenguinException {
        public InvalidXtException() : base() { }
        public InvalidXtException(string exceptionMessage) : base(exceptionMessage) { }
        public InvalidXtException(string exceptionMessage, InvalidXtException innerException) : base(exceptionMessage, innerException) { }
    }
}
