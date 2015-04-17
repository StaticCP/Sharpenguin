using System;
using XtPacket = Sharpenguin.Packets.Receive.Xt.XtPacket;
using XmlPacket = Sharpenguin.Packets.Receive.Xml.XmlPacket;
using Timers = System.Timers;

namespace Sharpenguin.Game {
    public class GameConnection : PenguinConnection {
        /// <summary>
        /// Occurs when the game server has been joined.
        /// </summary>
        public event JoinEventHandler OnJoin;
        /// <summary>
        /// Occurs when the connection's player has been loaded.
        /// </summary>
        public event LoadEventHandler OnLoad;
        /// <summary>
        /// The connection's player.
        /// </summary>
        private Player.MyPlayer player = null;
        /// <summary>
        /// Heartbeat timer.
        /// </summary>
        private Timers.Timer beat = new Timers.Timer();
        /// <summary>
        /// The room the player is currently in.
        /// </summary>
        private Room.Room room;

        public override int InternalRoom {
            get { return room.Id; }
        }

        public Room.Room Room {
            get { return room; }
            internal set { room = value; }
        }

        public Player.MyPlayer Player {
            get { 
                if(player != null)
                    return player;
                else
                    throw new Player.PlayerNotLoadedException("Your player has not yet been loaded!");
            }
            private set { player = value; }
        }

        public GameConnection(int id, string username, string loginkey) : base(username, loginkey) {
            this.id = id;
            Packets.Receive.IGamePacketHandler<XmlPacket>[] xml = HandlerLoader.GetHandlers<Packets.Receive.IGamePacketHandler<XmlPacket>>();
            Packets.Receive.IGamePacketHandler<XtPacket>[] xt = HandlerLoader.GetHandlers<Packets.Receive.IGamePacketHandler<XtPacket>>();
            foreach(Packets.Receive.IGamePacketHandler<XmlPacket> handler in xml) XmlHandlers.Add(handler);
            foreach(Packets.Receive.IGamePacketHandler<XtPacket> handler in xt) XtHandlers.Add(handler);
            room = new Room.Room(this) {
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

        class RandomKeyHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket> {
            public string Handles {
                get { return "rndK"; }
            }

            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xml.XmlPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                GameConnection game = connection as GameConnection;
                game.rndk = packet.XmlData.ChildNodes[0].InnerText;
                string hash = Security.Crypt.HashPassword(game.username, game.password, game.rndk);
                game.Send(new Sharpenguin.Packets.Send.Xml.Login(game.Username, hash + game.password));
            }
        }

        /// <summary>
        /// Represents a join server handler.
        /// </summary>
        class JoinServerHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
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
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                GameConnection game = connection as GameConnection;
                if(game != null) {
                    if(game.OnJoin != null) game.OnJoin(game);
                    game.Send(new Packets.Send.Xt.Player.Inventory.GetInventory(game));
                }
            }
        }

        /// <summary>
        /// Represents a load player handler.
        /// </summary>
        class LoadPlayerHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "lp"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="sender">The sender of the packet.</param>
            /// <param name="packet">The packet.</param>
            /// <param name="connection">Connection.</param>
            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                GameConnection game = connection as GameConnection;
                if(game != null) {
                    game.Player = new Player.MyPlayer(game, packet);
                    if(game.OnLoad != null) game.OnLoad(game.Player);
                }
            }
        }
    }
}

