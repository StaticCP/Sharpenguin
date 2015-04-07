namespace Sharpenguin.Game.Packets.Receive {
    public interface IGamePacketHandler<T> : Sharpenguin.Packets.Receive.IPacketHandler<T> where T : Sharpenguin.Packets.Receive.Packet { }
}

