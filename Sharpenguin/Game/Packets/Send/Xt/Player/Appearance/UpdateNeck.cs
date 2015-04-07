namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    public class UpdateNeck : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.UpdateNeck"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID to update the neck item to.</param>
        public UpdateNeck(PenguinConnection sender, int id) : base(sender, 'n', id) {
        }
    }
}

