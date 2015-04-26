namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents a send message packet.
    /// </summary>
    public class Message : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Message"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="message">The Message to send.</param>
        public Message(PenguinConnection sender, string message) : base(sender, "m#sm", new string[] { sender.Id.ToString(), message }) {}
    }
}

