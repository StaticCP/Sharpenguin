using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpenguin.Game.Player {
    class PositionHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        /// <summary>
        /// Gets the command that this packet handler handles.
        /// </summary>
        /// <value>The command that this packet handler handles.</value>
        public string Handles {
            get { return "sp"; }
        }

        /// <summary>
        /// Handle the given packet.
        /// </summary>
        /// <param name="receiver">The connection that received the packet.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="connection">Connection.</param>
        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            GameConnection game = connection as GameConnection;
            if(packet.Arguments.Length >= 3 && game != null) {
                int id;
                int x;
                int y;
                if(int.TryParse(packet.Arguments[0], out id) && int.TryParse(packet.Arguments[1], out x) && int.TryParse(packet.Arguments[2], out y)) {
                    IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id);
                    foreach(Player player in players) {
                        player.Position.X = x;
                        player.Position.Y = y;
                    }
                }
            }
        }
    }
}

