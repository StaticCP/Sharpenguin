namespace Sharpenguin.Game.Packets.Send.Xt.Room {
    /// <summary>
    /// Represents a join player packet.
    /// </summary>
    public class JoinPlayer : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Room.JoinPlayer"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the player to join.</param>
        public JoinPlayer(PenguinConnection sender, int id) : base(sender, "j#jp", new string[] { id.ToString() }) {}
    }
}

