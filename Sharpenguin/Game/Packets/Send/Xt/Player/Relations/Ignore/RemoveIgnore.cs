namespace Sharpenguin.Game.Packets.Send.Xt.Player.Relations.Ignore {
    /// <summary>
    /// Represents a remove ignore packet.
    /// </summary>
    public class RemoveIgnore : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Relations.Ignore.RemoveIgnore"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the player to remove the ignore from.</param>
        public RemoveIgnore(PenguinConnection sender, int id) : base(sender, "n#rn", new string[] { id.ToString() }) { }
    }
}