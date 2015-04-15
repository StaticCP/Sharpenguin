using System;

namespace Sharpenguin.Configuration.Game {
    public class NonExistentRoomException : PenguinException {
        public NonExistentRoomException() : base() { }
        public NonExistentRoomException(string message) : base(message) { }
        public NonExistentRoomException(string message, Exception inner) : base(message, inner) { }

    }
}

