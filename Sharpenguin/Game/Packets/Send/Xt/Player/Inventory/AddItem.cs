namespace Sharpenguin.Game.Packets.Send.Xt.Player.Inventory {
    /// <summary>
    /// Represents an add item packet.
    /// </summary>
    public class AddItem : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Inventory.AddItem"/> class.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="item">The id of the item to add.</param>
        public AddItem(PenguinConnection sender, int item) : base(sender, "i#ai", new string[] { item.ToString() }) { }
    }
}

