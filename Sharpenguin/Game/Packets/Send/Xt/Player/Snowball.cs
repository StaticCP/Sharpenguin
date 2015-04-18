namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents a throw snowball packet.
    /// </summary>
    public class Snowball : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Snowball"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="x">The x coordinate of the throw.</param>
        /// <param name="y">The y coordinate of the throw.</param>
        public Snowball(PenguinConnection sender, int x, int y) : base(sender, "u#sb", new string[] { x.ToString(), y.ToString() }) {}
    }
}

