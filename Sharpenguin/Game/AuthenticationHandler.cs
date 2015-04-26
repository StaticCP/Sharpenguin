namespace Sharpenguin.Game {
    /// <summary>
    /// Represents an authentication handler.
    /// </summary>
    class AuthenticationHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        /// <summary>
        /// Gets the command that this packet handler handles.
        /// </summary>
        /// <value>The command that this packet handler handles.</value>
        public string Handles {
            get { return "l"; }
        }

        /// <summary>
        /// Handle the given packet.
        /// </summary>
        /// <param name="connection">The connection that received the packet.</param>
        /// <param name="packet">The packet.</param>
        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            connection.Send(new Packets.Send.Xt.JoinServer(connection));
        }
    }
}