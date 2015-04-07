namespace Sharpenguin {
    /// <summary>
    /// Represents an error given when the user tries to connect to a server when we are already connected.
    /// </summary>
    public class AlreadyConnectedException : PenguinException {
        public AlreadyConnectedException() : base() { }
        public AlreadyConnectedException(string message) : base(message) { }
        public AlreadyConnectedException(string message, System.Exception inner) : base(message, inner) { }
    }
}

