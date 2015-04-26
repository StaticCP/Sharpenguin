using System;

namespace Sharpenguin.Game.Packets.Send.Xt {
    /// <summary>
    /// Represents a heart beat packet.
    /// </summary>
    public class HeartBeat : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.HeartBeat"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        public HeartBeat(PenguinConnection sender) : base(sender, "u#h") { }
    }
}

