using System;
using System.Collections.Generic;

namespace Sharpenguin.Game.Room {
    public class Room {
        private List<Game.Player.Player> players = new List<Game.Player.Player>();
        public event JoinRoomEventHandler OnJoin;
        public event LeaveRoomEventHandler OnLeave;
        private GameConnection connection;

        public IReadOnlyList<Game.Player.Player> Players {
            get {
                return players.AsReadOnly();
            }
        }

        public int Id {
            get;
            internal set;
        }

        public int External {
            get;
            internal set;
        }

        public string Name {
            get;
            internal set;
        }

        public Player.MyPlayer Me {
            get { return connection.Player; }
        }

        public Room(GameConnection connection) {
            this.connection = connection;
        }

        internal void Add(Game.Player.Player player) {
            players.Add(player);
            if(OnJoin != null)
                OnJoin(player);
        }

        internal void Remove(Game.Player.Player player) {
            players.Remove(player);
            if(OnLeave != null)
                OnLeave(player);
        }

        /// <summary>
        /// Represents an join room handler.
        /// </summary>
        class JoinRoomHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "jr"; }
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
                if(game != null && packet.Arguments.Length >= 1) {
                    int extId;
                    if(int.TryParse(packet.Arguments[0], out extId)) {
                        try {
                            if(extId <= 1000) {
                                Configuration.Game.Room room = Configuration.Configuration.Rooms[extId];
                                game.Room.Id = packet.Room;
                                game.Room.External = room.Id;
                                game.Room.Name = room.Name;
                            }else{
                                game.Room.Id = extId;
                                game.Room.External = extId;
                                game.Room.Name = "an igloo";
                            }
                            game.Room.players.Clear();
                            for(int i = 1; i < packet.Arguments.Length; i++) {
                                Player.Player load = new Player.Player(packet.Arguments[i]);
                                if(load.Id != game.Id) game.Room.players.Add(load);
                            }
                            game.Room.OnJoin(game.Player);
                        } catch(Configuration.Game.NonExistentRoomException) {
                            // Will be logged
                        }
                    } else {
                        // Will be logged
                    }
                }
            }

        }
    }
}

