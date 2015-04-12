﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Sharpenguin.Room {
    public class RemovePlayerHandler : Game.Packets.Receive.IGamePacketHandler<Packets.Receive.Xt.XtPacket> {
        public string Handles {
            get { return "rp"; }
        }

        public void Handle(PenguinConnection connection, Packets.Receive.Xt.XtPacket packet) {
            Game.GameConnection game = connection as Game.GameConnection;
            if(game != null) {
                int id;
                if(packet.Arguments.Length >= 1 && int.TryParse(packet.Arguments[0], out id)) {
                    IEnumerable<Game.Player.Player> returns = game.Room.Players.Where(p => p.Id == id);
                    foreach(Game.Player.Player player in returns) {
                        connection.Room.Remove(player);
                    }
                }
            }
        }
    }
}

