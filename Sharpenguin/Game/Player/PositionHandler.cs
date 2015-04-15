using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpenguin.Game.Player {
    class PositionHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        public string Handles {
            get { return "sp"; }
        }

        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection");
            if(packet == null) throw new System.ArgumentNullException("packet");
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

