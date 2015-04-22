using System;
using System.Collections.Generic;

namespace Sharpenguin.Game.Room {
    public class Room {
        /// <summary>
        /// The list of players in the room.
        /// </summary>
        private List<Game.Player.Player> players = new List<Game.Player.Player>();
        /// <summary>
        /// Occurs when a player joins the room.
        /// </summary>
        public event JoinRoomEventHandler OnJoin;
        /// <summary>
        /// Occurs when a player leaves the room.
        /// </summary>
        public event LeaveRoomEventHandler OnLeave;
        /// <summary>
        /// The parent connection.
        /// </summary>
        private GameConnection connection;

        /// <summary>
        /// Gets the players in the room.
        /// </summary>
        /// <value>The players.</value>
        public IReadOnlyList<Game.Player.Player> Players {
            get {
                return players.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>The room identifier.</value>
        public int Id {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the room's external identifier.
        /// </summary>
        /// <value>The room's external identifier.</value>
        public int External {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the room's name.
        /// </summary>
        /// <value>The room's name.</value>
        public string Name {
            get;
            internal set;
        }

        /// <summary>
        /// Gets my player.
        /// </summary>
        /// <value>My player.</value>
        public Player.MyPlayer Me {
            get { return connection.Player; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Room.Room"/> class.
        /// </summary>
        /// <param name="connection">The parent connection.</param>
        public Room(GameConnection connection) {
            this.connection = connection;
        }

        /// <summary>
        /// Adds the specified player to the room.
        /// </summary>
        /// <param name="player">The player to add.</param>
        internal void Add(Game.Player.Player player) {
            players.Add(player);
            if(OnJoin != null)
                OnJoin(player);
        }

        /// <summary>
        /// Remove the specified player from the room.
        /// </summary>
        /// <param name="player">The player to remove.</param>
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
                        } catch(Configuration.Game.NonExistentRoomException ex) {
                            Configuration.Configuration.Logger.Error(ex.Message);
                        }
                    } else {
                        Configuration.Configuration.Logger.Error("Could not parse the external room ID as an integer. Given string was '" + packet.Arguments[0] + "'.");
                    }
                }
            }

        }
    }
}

