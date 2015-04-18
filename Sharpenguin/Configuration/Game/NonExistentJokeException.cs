using System;

namespace Sharpenguin.Configuration.Game {
    public class NonExistentJokeException : PenguinException {
        public NonExistentJokeException() : base() { }
        public NonExistentJokeException(string message) : base(message) { }
        public NonExistentJokeException(string message, Exception inner) : base(message, inner) { }

    }
}

