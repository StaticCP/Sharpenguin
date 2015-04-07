namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update body packet.
    /// </summary>
    public class UpdateBody : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateBody"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the body item to.</param>
        public UpdateBody(PenguinConnection sender, int id) : base(sender, 'b', id) {
        }
    }
}

