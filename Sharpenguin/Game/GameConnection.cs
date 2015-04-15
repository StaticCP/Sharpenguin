using System;
using XtPacket = Sharpenguin.Packets.Receive.Xt.XtPacket;
using XmlPacket = Sharpenguin.Packets.Receive.Xml.XmlPacket;
using Timers = System.Timers;

namespace Sharpenguin.Game {
    public class GameConnection : PenguinConnection {
        public event JoinEventHandler OnJoin; //< Event for handling join success.
        private Player.MyPlayer player;
        private Timers.Timer beat = new Timers.Timer(); //< Heartbeat timer.
        private Room.Room room;

        public override int InternalRoom {
            get { return room.Id; }
        }

        public Room.Room Room {
            get { return room; }
            internal set { room = value; }
        }

        public Player.MyPlayer Player {
            get { return player; }
        }

        public GameConnection(int id, string username, string loginkey) : base(username, loginkey) {
            this.id = id;
            player = new Player.MyPlayer(this);
            Packets.Receive.IGamePacketHandler<XmlPacket>[] xml = HandlerLoader.GetHandlers<Packets.Receive.IGamePacketHandler<XmlPacket>>();
            Packets.Receive.IGamePacketHandler<XtPacket>[] xt = HandlerLoader.GetHandlers<Packets.Receive.IGamePacketHandler<XtPacket>>();
            foreach(Packets.Receive.IGamePacketHandler<XmlPacket> handler in xml) XmlHandlers.Add(handler);
            foreach(Packets.Receive.IGamePacketHandler<XtPacket> handler in xt) XtHandlers.Add(handler);
            room = new Room.Room {
                Id = -1,
                External = 0,
                Name = "System"
            };
            beat.Interval = 60000;
            beat.Elapsed += HeartBeat;
            OnDisconnect += DisconnectHandler;
            beat.Start();
        }

        /// <summary>
        /// Sends a heartbeat packet to the server on each interval of the beat timer.
        /// </summary>
        /// <param name="sender">The timer.</param>
        /// <param name="e">The event arguments.</param>
        private void HeartBeat(object sender, Timers.ElapsedEventArgs e) {
            Send(new Packets.Send.Xt.HeartBeat(this)); // Sends the heart beat
        }

        /// <summary>
        /// Handles a disconnect.
        /// </summary>
        /// <param name="connection">Connection.</param>
        private void DisconnectHandler(PenguinConnection connection) {
            beat.Stop(); 
        }

        public class RandomKeyHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket> {
            public string Handles {
                get { return "rndK"; }
            }

            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xml.XmlPacket packet) {
                GameConnection game = connection as GameConnection;
                game.rndk = packet.XmlData.ChildNodes[0].InnerText;
                string hash = Security.Crypt.HashPassword(game.username, game.password, game.rndk);
                game.Send(new Sharpenguin.Packets.Send.Xml.Login(game.Username, hash + game.password));
            }
        }

        /// <summary>
        /// Represents an join server handler.
        /// </summary>
        public class JoinServerHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "js"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="sender">The sender of the packet.</param>
            /// <param name="packet">The packet.</param>
            /// <param name="connection">Connection.</param>
            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                GameConnection game = connection as GameConnection;
                if(game != null && game.OnJoin != null) game.OnJoin(game);
            }
        }
    }
}

