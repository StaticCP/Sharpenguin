using System;
using System.Linq;
using System.Collections.Generic;

namespace Sharpenguin.Game.Room {
    class RemovePlayerHandler : Game.Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        public string Handles {
            get { return "rp"; }
        }

        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            Game.GameConnection game = connection as Game.GameConnection;
            if(game != null) {
                int id;
                if(packet.Arguments.Length >= 1 && int.TryParse(packet.Arguments[0], out id)) {
                    IEnumerable<Game.Player.Player> returns = game.Room.Players.Where(p => p.Id == id);
                    foreach(Game.Player.Player player in returns) {
                        game.Room.Remove(player);
                    }
                }
            }
        }
    }
}

