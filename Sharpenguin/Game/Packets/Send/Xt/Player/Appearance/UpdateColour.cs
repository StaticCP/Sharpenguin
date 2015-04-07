namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update colour packet.
    /// </summary>
    public class UpdateColour : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateColour"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the colour to.</param>
        public UpdateColour(PenguinConnection sender, int id) : base(sender, 'c', id) {
        }
    }
}

