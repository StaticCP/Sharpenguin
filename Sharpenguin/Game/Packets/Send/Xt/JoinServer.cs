namespace Sharpenguin.Game.Packets.Send.Xt {
    /// <summary>
    /// Represents a join server packet.
    /// </summary>
    public class JoinServer : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.JoinServer"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        public JoinServer(PenguinConnection sender) : base(sender, "j#js", new string[] { sender.Id.ToString(), sender.Password, "en" }) { }
    }
}

