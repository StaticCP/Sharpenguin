namespace Sharpenguin.Game.Packets.Send.Xt.Room {
    /// <summary>
    /// Represents a join room packet.
    /// </summary>
    public class JoinRoom : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.JoinRoom"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the room to join.</param>
        public JoinRoom(PenguinConnection sender, int room, int x, int y) : base(sender, "j#jr", new string[] { room.ToString(), x.ToString(), y.ToString() }) {}
    }
}

