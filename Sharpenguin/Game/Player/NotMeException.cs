namespace Sharpenguin.Game.Player {
    /// <summary>
    /// Represents an error given when the user tries to change the details of another player.
    /// </summary>
    public class NotMeException : PenguinException {
        public NotMeException() : base() { }
        public NotMeException(string message) : base(message) { }
        public NotMeException(string message, System.Exception inner) : base(message, inner) { }
    }
}

