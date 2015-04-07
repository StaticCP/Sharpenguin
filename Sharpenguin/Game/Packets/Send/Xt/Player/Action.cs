namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents a send action packet.
    /// </summary>
    public class Action : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Action"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID of the action to send.</param>
        public Action(PenguinConnection sender, int id) : base(sender, "u#sa", new string[] { id.ToString() }) {}
    }
}

