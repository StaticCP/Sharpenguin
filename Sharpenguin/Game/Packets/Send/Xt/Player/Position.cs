namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents a change position packet.
    /// </summary>
    public class Position : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Position"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="x">The x coordinate to send.</param>
        /// <param name="y">The y coordinate to send.</param> 
        public Position(PenguinConnection sender, int x, int y) : base(sender, "u#sp", new string[] { x.ToString(), y.ToString() }) {}
    }
}

