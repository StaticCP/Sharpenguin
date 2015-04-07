namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update face packet.
    /// </summary>
    public class UpdateFace : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateFace"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The ID to update the face item to.</param>
        public UpdateFace(PenguinConnection sender, int id) : base(sender, 'f', id) {
        }
    }
}

