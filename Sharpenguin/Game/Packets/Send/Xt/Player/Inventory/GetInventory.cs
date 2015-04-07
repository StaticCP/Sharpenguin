namespace Sharpenguin.Game.Packets.Send.Xt.Player.Inventory {
    /// <summary>
    /// Represents a get inventory packet.
    /// </summary>
    public class GetInventory : Sharpenguin.Packets.Send.Xt.XtPacket {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Sharpenguin.Game.Packets.Send.Xt.Player.Inventory.GetInventory"/> class.
        /// </summary>
        /// <param name="sender">Sender.</param>
        public GetInventory(PenguinConnection sender) : base(sender, "i#gi") { }
    }
}
