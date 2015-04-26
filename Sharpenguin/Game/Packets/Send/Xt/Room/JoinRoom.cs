namespace Sharpenguin.Game.Packets.Send.Xt.Room {
    /// <summary>
    /// Represents a join room packet.
    /// </summary>
    public class JoinRoom : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Room.JoinRoom"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="room">The room id to join</param>
        /// <param name="x">The x coordinate to join at.</param>
        /// <param name="y">The y coordinate to join at.</param> 
        public JoinRoom(PenguinConnection sender, int room, int x, int y) : base(sender, "j#jr", new string[] { room.ToString(), x.ToString(), y.ToString() }) {}
    }
}

