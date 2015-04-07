namespace Sharpenguin.Login.Packets.Receive {
    public interface ILoginPacketHandler<T> : Sharpenguin.Packets.Receive.IPacketHandler<T> where T : Sharpenguin.Packets.Receive.Packet { }
}