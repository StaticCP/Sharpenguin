using System;

namespace Sharpenguin.Packets.Receive {
    /// <summary>
    /// Represents a packet handler for both login and game packets.
    /// </summary>
    public interface IDefaultPacketHandler<T> : IPacketHandler<T> where T : Packets.Receive.Packet {
    }
}

