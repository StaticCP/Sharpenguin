namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update hands packet.
    /// </summary>
    public class UpdateHands : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateHands"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the hands to.</param>
        public UpdateHands(PenguinConnection sender, int id) : base(sender, 'a', id) {
        }
    }
}

