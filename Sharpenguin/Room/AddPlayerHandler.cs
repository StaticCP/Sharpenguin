using System;

namespace Sharpenguin {
    public class AddPlayerHandler : Game.Packets.Receive.IGamePacketHandler<Packets.Receive.Xt.XtPacket> {
        public string Handles {
            get { return "ap"; }
        }

        public void Handle(PenguinConnection connection, Packets.Receive.Xt.XtPacket packet) {
            Game.GameConnection game = connection as Game.GameConnection;
            if(game != null) {
                Game.Player.Player player = new Game.Player.Player();
                player.LoadData(packet.Arguments[0]);
                game.Room.Add(player);
            }
        }
    }
}

