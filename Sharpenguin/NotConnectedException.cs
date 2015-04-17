namespace Sharpenguin {
    /// <summary>
    /// Represents an error given when the user tries to get the server the connection is connected to when it is not connected.
    /// </summary>
    public class NotConnectedException : PenguinException {
        public NotConnectedException() : base() { }
        public NotConnectedException(string message) : base(message) { }
        public NotConnectedException(string message, System.Exception inner) : base(message, inner) { }
    }
}

