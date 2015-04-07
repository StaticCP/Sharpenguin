namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update player art packet.
    /// </summary>
    public class UpdateArt : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateArt"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="code">The item type code.</param>
        /// <param name="item">The item id to update the art to.</param>
        public UpdateArt(PenguinConnection sender, char code, int item) : base(sender, "s#up" + code, new string[] { item.ToString() }) { }
    }
}

