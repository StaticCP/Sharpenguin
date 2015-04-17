namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents a get puffle packet.
    /// </summary>
    public class GetPuffle : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Room.JoinPlayer"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the player to get puffle information from.</param>
        public GetPuffle(PenguinConnection sender, int id) : base(sender, "p#gp", new string[] { id.ToString() }) {}
    }
}

