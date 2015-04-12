using System;

namespace Sharpenguin.Configuration.System {
    public class NonExistentErrorException : PenguinException {
        public NonExistentErrorException() : base() { }
        public NonExistentErrorException(string message) : base(message) { }
        public NonExistentErrorException(string message, Exception inner) : base(message, inner) { }

    }
}

