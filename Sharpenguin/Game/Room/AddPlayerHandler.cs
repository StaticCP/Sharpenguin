using System;

namespace Sharpenguin.Game.Room {
    public class AddPlayerHandler : Game.Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
        public string Handles {
            get { return "ap"; }
        }

        public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            Game.GameConnection game = connection as Game.GameConnection;
            if(game != null) {
                Game.Player.Player player = new Game.Player.Player();
                player.LoadData(packet.Arguments[0]);
                if(player.Id != game.Player.Id) game.Room.Add(player);
            }
        }
    }
}

