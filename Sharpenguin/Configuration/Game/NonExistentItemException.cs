using System;

namespace Sharpenguin.Configuration.Game {
    public class NonExistentItemException : PenguinException {
            public NonExistentItemException() : base() { }
            public NonExistentItemException(string message) : base(message) { }
            public NonExistentItemException(string message, Exception inner) : base(message, inner) { }

    }
}

