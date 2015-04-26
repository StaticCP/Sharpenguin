namespace Sharpenguin.Packets.Receive {
    /// <summary>
    /// Represents a packet handler.
    /// </summary>
    public interface IPacketHandler<T> where T : Packet {
        /// <summary>
        /// Gets the command that this packet handler handles.
        /// </summary>
        /// <value>
        /// The command that this packet handler handles.
        /// </value>
        string Handles {
            get;
        }
  
        /// <summary>
        /// Handle the given packet.
        /// </summary>
        /// <param name="receiver">
        /// The connection that received the packet.
        /// </param>
        /// <param name="packet">
        /// The packet.
        /// </param>
        void Handle(PenguinConnection receiver, T packet);
    }
}
