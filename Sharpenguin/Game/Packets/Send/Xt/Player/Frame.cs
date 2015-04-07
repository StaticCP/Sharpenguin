namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents an update frame packet.
    /// </summary>
    public class Frame : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Frame"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID to update the frame to.</param>
        public Frame(PenguinConnection sender, int id) : base(sender, "u#sf", new string[] { id.ToString() }) {}
    }
}

