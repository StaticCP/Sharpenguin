using System;

namespace Sharpenguin.Packets.Receive {
    public interface IDefaultPacketHandler<T> : IPacketHandler<T> where T : Packets.Receive.Packet {
    }
}

