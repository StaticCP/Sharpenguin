namespace Sharpenguin.Game.Packets.Send.Xt.Room {
    /// <summary>
    /// Represents a get igloo details packet.
    /// </summary>
    public class GetIglooDetails : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Room.GetIglooDetails"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The igloo owner's ID.</param>
        public GetIglooDetails(PenguinConnection sender, int id) : base(sender, "g#gm", new string[] { id.ToString() }) {}
    }
}

