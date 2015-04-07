namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update background packet.
    /// </summary>
    public class UpdateBackground : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateBackground"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the background to.</param>
        public UpdateBackground(PenguinConnection sender, int id) : base(sender, 'p', id) {
        }
    }
}

