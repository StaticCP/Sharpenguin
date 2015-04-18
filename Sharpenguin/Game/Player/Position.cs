using System.Collections.Generic;
using System.Linq;

namespace Sharpenguin.Game.Player {
    /// <summary>
    /// Represents a player's position.
    /// </summary>
    public class Position {
        /// <summary>
        /// The x position.
        /// </summary>
        private int x;
        /// <summary>
        /// The y position.
        /// </summary>
        private int y;
        /// <summary>
        /// The frame ID.
        /// </summary>
        internal int frame;
        /// <summary>
        /// The parent player.
        /// </summary>
        private Player player;

        /// <summary>
        /// Occurs when the player changes position.
        /// </summary>
        public event PositionChangeEventHandler OnMove;
        /// <summary>
        /// Occurs when the player changes frame.
        /// </summary>
        public event FrameChangeEventHandler OnChangeFrame;

        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>The x.</value>
        public int X {
            get { return x; }
            internal set { x = value; }
        }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>The y.</value>
        public int Y {
            get { return y; }
            internal set { y = value; }
        }

        /// <summary>
        /// Gets or sets the frame ID.
        /// </summary>
        /// <value>The frame.</value>
        public int Frame {
            get { return frame; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer)player;
                    me.Connection.Send(new Packets.Send.Xt.Player.Frame(me.Connection, value));
                    frame = value;
                } else {
                    throw new NotMeException("The frame of other players cannot be set!");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.Position"/> class.
        /// </summary>
        /// <param name="player">The parent player.</param>
        public Position(Player player) {
            if(player == null) throw new System.ArgumentNullException("player", "Argument cannot be null.");
            this.player = player;
        }

        /// <summary>
        /// Set the x and y of the player.
        /// </summary>
        /// <remarks>
        /// This only works for your own player, not others!
        /// </remarks>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void Set(int x, int y) {
            if(player is MyPlayer) {
                MyPlayer me = (MyPlayer)player;
                me.Connection.Send(new Packets.Send.Xt.Player.Position(me.Connection, x, y));
                X = x;
                Y = y;
                if(me.Position.OnMove != null) me.Position.OnMove(me, me.Position);
            } else {
                throw new NotMeException("The position of other players cannot be set!");
            }
        }

        class PositionHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "sp"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="receiver">The connection that received the packet.</param>
            /// <param name="packet">The packet.</param>
            /// <param name="connection">Connection.</param>
            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                GameConnection game = connection as GameConnection; // We need a game connection for the room.
                if(packet.Arguments.Length >= 3 && game != null) {
                    int id;
                    int x;
                    int y;
                    // "id != game.Id" is a design choice, everything is handled when the player sets their own position..
                    if(int.TryParse(packet.Arguments[0], out id) && id != game.Id &&  int.TryParse(packet.Arguments[1], out x) && int.TryParse(packet.Arguments[2], out y)) {
                        IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id); // Get every player with that id (there should only really be one..)
                        foreach(Player player in players) {
                            player.Position.X = x; // Set their X coordinate
                            player.Position.Y = y; // Set their Y coordinate
                            if(player.Position.OnMove != null) player.Position.OnMove(player, player.Position); // Raise the event
                        }
                    }
                }
            }
        }

        class FrameHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            public string Handles {
                get { return "sf"; }
            }

            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                GameConnection game = connection as GameConnection;
                if(packet.Arguments.Length >= 2 && game != null) {
                    int id;
                    int frame;
                    if(int.TryParse(packet.Arguments[0], out id) && id != game.Id && int.TryParse(packet.Arguments[1], out frame)) {
                        IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id); // Get every player with that id (there should only really be one..)
                        foreach(Player player in players) {
                            player.Position.frame = frame; // Set their frame
                            if(player.Position.OnChangeFrame != null) player.Position.OnChangeFrame(player, player.Position); // Raise the event
                        }
                    }
                }
            }
        }

    }
}

