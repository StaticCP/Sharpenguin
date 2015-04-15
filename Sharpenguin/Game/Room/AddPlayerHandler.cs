using System;

namespace Sharpenguin.Game.Room {
    class AddPlayerHandler : Game.Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        public string Handles {
            get { return "ap"; }
        }

        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection");
            if(packet == null) throw new System.ArgumentNullException("packet");
            Game.GameConnection game = connection as Game.GameConnection;
            if(game != null) {
                Game.Player.Player player = new Game.Player.Player(packet.Arguments[0]);
                if(player.Id != game.Player.Id) game.Room.Add(player);
            }
        }
    }
}

