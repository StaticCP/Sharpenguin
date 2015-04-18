using System;

namespace Sharpenguin.Configuration.Game {
    public class NonExistentMessageException : PenguinException {
        public NonExistentMessageException() : base() { }
        public NonExistentMessageException(string message) : base(message) { }
        public NonExistentMessageException(string message, Exception inner) : base(message, inner) { }

    }
}

