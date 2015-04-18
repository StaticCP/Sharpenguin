namespace Sharpenguin.Game.Packets.Send.Xt.Player {
    /// <summary>
    /// Represents an emoticon packet.
    /// </summary>
    public class Emoticon : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Emoticon"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="emote">The ID of the emote to send.</param>
        public Emoticon(PenguinConnection sender, int emote) : base(sender, "u#se", new string[] { emote.ToString() }) {}
    }
}

