namespace Sharpenguin.Packets.Send.Xt {
    /// <summary>
    /// Represents an xt packet to be sent.
    /// </summary>
    public abstract class XtPacket : Send.Packet {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Send.Xt.XtPacket"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="handler">The packet's handler string (command).</param>
        public XtPacket(PenguinConnection sender, string handler) : base("%xt%s%" + handler + "%" + sender.InternalRoom.ToString() + "%") {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Send.Xt.XtPacket"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="handler">The packet's handler string (command).</param>
        /// <param name="args">The packet's arguments.</param>
        public XtPacket(PenguinConnection sender, string handler, string[] args) : base("%xt%s%" + handler + "%" + sender.InternalRoom.ToString() + "%" + string.Join("%", args) + "%") {}
    }
}
