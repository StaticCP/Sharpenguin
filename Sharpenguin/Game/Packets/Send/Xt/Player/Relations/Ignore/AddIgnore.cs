namespace Sharpenguin.Game.Packets.Send.Xt.Player.Relations.Ignore {
    /// <summary>
    /// Represents an add ignore packet.
    /// </summary>
    public class AddIgnore : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Relations.Ignore.AddIgnore"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the player to ignore.</param>
        public AddIgnore(PenguinConnection sender, int id) : base(sender, "n#an", new string[] { id.ToString() }) { }
    }
}

