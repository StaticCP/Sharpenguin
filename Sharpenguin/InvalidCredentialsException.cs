namespace Sharpenguin {
    /// <summary>
    /// Represents an error given when the user gives invalid credentials.
    /// </summary>
    public class InvalidCredentialsException : PenguinException {
        public InvalidCredentialsException() : base() { }
        public InvalidCredentialsException(string message) : base(message) { }
        public InvalidCredentialsException(string message, System.Exception inner) : base(message, inner) { }
    }
}

