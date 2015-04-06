namespace Sharpenguin.Packets.Receive {
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
        /// <param name='sender'>
        /// The sender of the packet.
        /// </param>
        /// <param name='packet'>
        /// The packet.
        /// </param>
        void Handle(PenguinConnection sender, T packet);
    }
}
