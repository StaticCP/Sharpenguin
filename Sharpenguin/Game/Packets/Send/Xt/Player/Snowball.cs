namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents a throw snowball packet.
    /// </summary>
    public class Snowball : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Snowball"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the Snowball to send.</param>
        public Snowball(PenguinConnection sender, int x, int y) : base(sender, "u#sb", new string[] { x.ToString(), y.ToString() }) {}
    }
}

