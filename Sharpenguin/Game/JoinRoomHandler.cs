namespace Sharpenguin.Game {
    /// <summary>
    /// Represents an join room handler.
    /// </summary>
    class JoinRoomHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        /// <summary>
        /// Gets the command that this packet handler handles.
        /// </summary>
        /// <value>The command that this packet handler handles.</value>
        public string Handles {
            get { return "jr"; }
        }

        /// <summary>
        /// Handle the given packet.
        /// </summary>
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="connection">Connection.</param>
        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection");
            if(packet == null) throw new System.ArgumentNullException("packet");
            // TODO
        }
    }
}