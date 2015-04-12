using System;

namespace Sharpenguin.Packets.Receive {
    public class UnhandledPacketException : PenguinException {
        public UnhandledPacketException() : base() { }
        public UnhandledPacketException(string message) : base(message) { }
        public UnhandledPacketException(string message, System.Exception inner) : base(message, inner) { }
    }
}

