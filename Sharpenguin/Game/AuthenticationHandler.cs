namespace Sharpenguin.Game {
    /// <summary>
    /// Represents an authentication handler.
    /// </summary>
    public class AuthenticationHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
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
        /// <param name="sender">The sender of the packet.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="connection">Connection.</param>
        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            connection.Send(new Packets.Send.Xt.JoinServer(connection));
        }
    }
}