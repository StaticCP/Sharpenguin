namespace Sharpenguin.Game.Packets.Send.Xt.Player.Relations.Buddies {

    /// <summary>
    /// Represents a find buddy packet.
    /// </summary>
    public class FindBuddy : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Relations.Buddies.FindBuddy"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The id of the buddy to find.</param>
        public FindBuddy(PenguinConnection sender, int id) : base(sender, "u#bf", new string[] { id.ToString() }) { }
    }
}

