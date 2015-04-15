namespace Sharpenguin.Game.Player {
    /// <summary>
    /// Represents an error given when the user tries to change the details of another player.
    /// </summary>
    public class PlayerNotLoadedException : PenguinException {
        public PlayerNotLoadedException() : base() { }
        public PlayerNotLoadedException(string message) : base(message) { }
        public PlayerNotLoadedException(string message, System.Exception inner) : base(message, inner) { }
    }
}

