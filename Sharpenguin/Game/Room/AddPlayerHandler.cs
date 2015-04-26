using System;

namespace Sharpenguin.Game.Room {
    /// <summary>
    /// Represents an add player packet handler.
    /// </summary>
    class AddPlayerHandler : Game.Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        /// <summary>
        /// Gets the command that this packet handler handles.
        /// </summary>
        /// <value>The command that this packet handler handles.</value>
        public string Handles {
            get { return "ap"; }
        }

        /// <summary>
        /// Handle the given packet.
        /// </summary>
        /// <param name="connection">The connection that received the packet.</param>
        /// <param name="packet">The packet.</param>
        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            Game.GameConnection game = connection as Game.GameConnection;
            if(game != null) {
                Game.Player.Player player = new Game.Player.Player(packet.Arguments[0]);
                if(player.Id != game.Player.Id) game.Room.Add(player);
            }
        }
    }
}

