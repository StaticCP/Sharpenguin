namespace Sharpenguin.Packets.Send.Player {
    /// <summary>
    /// Represents a get player packet.
    /// </summary>
    public class GetPlayer : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Send.Player.GetPlayer"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the player.</param>
        public GetPlayer(PenguinConnection sender, int id) : base(sender, "u#gp", new string[] { id.ToString() })  {
        }
    }
}

