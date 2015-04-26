namespace Sharpenguin.Login.Packets.Receive {
    /// <summary>
    /// Represents a login packet handler.
    /// </summary>
    public interface ILoginPacketHandler<T> : Sharpenguin.Packets.Receive.IPacketHandler<T> where T : Sharpenguin.Packets.Receive.Packet { }
}