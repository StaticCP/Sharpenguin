namespace Sharpenguin.Game.Packets.Receive {
    /// <summary>
    /// Represents a game packet handler.
    /// </summary>
    public interface IGamePacketHandler<T> : Sharpenguin.Packets.Receive.IPacketHandler<T> where T : Sharpenguin.Packets.Receive.Packet { }
}

