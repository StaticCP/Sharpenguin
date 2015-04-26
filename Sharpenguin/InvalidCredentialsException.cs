namespace Sharpenguin {
    /// <summary>
    /// Represents an error given when the user gives invalid credentials.
    /// </summary>
    public class InvalidCredentialsException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.InvalidCredentialsException"/> class.
        /// </summary>
        public InvalidCredentialsException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.InvalidCredentialsException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidCredentialsException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.InvalidCredentialsException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidCredentialsException(string message, System.Exception inner) : base(message, inner) { }
    }
}

