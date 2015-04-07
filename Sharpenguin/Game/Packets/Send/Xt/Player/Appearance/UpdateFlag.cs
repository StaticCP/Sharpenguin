namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update flag packet.
    /// </summary>
    public class UpdateFlag : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateFlag"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the flag to.</param>
        public UpdateFlag(PenguinConnection sender, int id) : base(sender, 'l', id) {
        }
    }
}

