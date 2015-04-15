using System;
using System.Collections.Generic;

namespace Sharpenguin.Game.Room {
    public class Room {
        private List<Game.Player.Player> players = new List<Game.Player.Player>();
        public event JoinRoomEventHandler OnJoin;
        public event LeaveRoomEventHandler OnLeave;

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

        public void Join(Configuration.Game.Room room) {
            Join(room.Id);
        }

        public void Join(int id) {
            if(id <= 1000) {

            } else {
                //SendData("%xt%s%g#gm%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
                //SendData("%xt%s%p#pg%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
                //SendData("%xt%s%j#jp%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
            }
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
                if(connection == null) throw new System.ArgumentNullException("connection");
                if(packet == null) throw new System.ArgumentNullException("packet");
                GameConnection game = connection as GameConnection;
                if(game != null && packet.Arguments.Length >= 1) {
                    int extId;
                    if(int.TryParse(packet.Arguments[0], out extId)) {
                        try {
                            Configuration.Game.Room room = Configuration.Configuration.Rooms[extId];
                            game.Room.players.Clear();
                            game.Room.Id = packet.Room;
                            game.Room.External = room.Id;
                            game.Room.Name = room.Name;
                            for(int i = 1; i < packet.Arguments.Length; i++) {
                                Player.Player load = new Player.Player(packet.Arguments[i]);
                                if(load.Id != game.Id) game.Room.players.Add(load);
                            }
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

