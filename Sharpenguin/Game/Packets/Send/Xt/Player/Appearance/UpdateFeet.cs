namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represnts an update feet packet.
    /// </summary>
    public class UpdateFeet : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateFeet"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the feet to.</param>
        public UpdateFeet(PenguinConnection sender, int id) : base(sender, 'e', id) {
        }
    }
}

