namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents an update head packet.
    /// </summary>
    public class UpdateHead : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateHead"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the head item to.</param>
        public UpdateHead(PenguinConnection sender, int id) : base(sender, 'h', id) {}
    }
}

