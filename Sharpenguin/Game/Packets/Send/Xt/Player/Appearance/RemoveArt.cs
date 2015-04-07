using System;

namespace Sharpenguin.Game.Packets.Send.Xt.Player.Appearance {
    /// <summary>
    /// Represents a remove art packet.
    /// </summary>
    public class RemoveArt : UpdateArt {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Appearance.RemoveArt"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="id">The item ID of the item to remove.</param>
        public RemoveArt(PenguinConnection sender, int id) : base(sender, 'r', id) {
        }
    }
}

