using System;

namespace Sharpenguin.Packets.Receive {
    /// <summary>
    /// Represents the exception thrown when a packet is received and has no handler set up for it (for debugging).
    /// </summary>
    public class UnhandledPacketException : PenguinException {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Receive.UnhandledPacketException"/> class.
        /// </summary>
        public UnhandledPacketException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Receive.UnhandledPacketException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public UnhandledPacketException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Receive.UnhandledPacketException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public UnhandledPacketException(string message, System.Exception inner) : base(message, inner) { }
    }
}

