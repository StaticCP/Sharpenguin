namespace Sharpenguin {
    /// <summary>
    /// Represents a base exception within the library.
    /// </summary>
    public class PenguinException : System.Exception {
        public PenguinException() : base() { }
        public PenguinException(string message) : base(message) { }
        public PenguinException(string message, System.Exception inner) : base(message, inner) { }
    }
}

