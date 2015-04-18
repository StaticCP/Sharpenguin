using System.Collections.Generic;
using System.Linq;

namespace Sharpenguin.Game.Player {

    /**
     * The player class. Information about players in the room are stored here.
     */
    public class Player {
        /// <summary>
        /// The player's items.
        /// </summary>
        private Appearance.Clothing items;
        /// <summary>
        /// The player's position.
        /// </summary>
        private Position position;
        /// <summary>
        /// The player's ID.
        /// </summary>
        private int id            = 0;
        /// <summary>
        /// The amount of days the player has been a member for.
        /// </summary>
        private int memberDays    = 0;
        /// <summary>
        /// The time zone offset.
        /// </summary>
        private int tzo           = 0;
        /// <summary>
        /// The player's username.
        /// </summary>
        private string username   = "";
        /// <summary>
        /// Whether the player is a member.
        /// </summary>
        private bool isMember     = false;

        /// <summary>
        /// Occurs when the player speaks.
        /// </summary>
        public event SpeakEventHandler OnSpeak;
        /// <summary>
        /// Occurs when the player sends an emoticon.
        /// </summary>
        public event EmoteEventHandler OnEmoticon;
        /// <summary>
        /// Occurs when the player does an action.
        /// </summary>
        public event ActionEventHandler OnAction;
        /// <summary>
        /// Occurs when the player throws a snowball.
        /// </summary>
        public event ThrowEventHandler OnThrow;

        /// <summary>
        /// Gets the player's ID.
        /// </summary>
        /// <value>The player's ID.</value>
        public int Id {
            get { return id; }
        }

        /// <summary>
        /// Gets the amount of days the player has been a member.
        /// </summary>
        /// <value>The amount of days the player has been a member.</value>
        public int MemberDays {
            get { return memberDays; }
        }
        /// <summary>
        /// Gets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        public int TimeZoneOffset {
            get { return tzo; }
        }

        /// <summary>
        /// Gets the player's username.
        /// </summary>
        /// <value>The player's username.</value>
        public string Username {
            get { return username; }
        }

        /// <summary>
        /// Gets a value indicating whether this player is a member.
        /// </summary>
        /// <value><c>true</c> if this player is a member; otherwise, <c>false</c>.</value>
        public bool IsMember {
            get { return isMember; }
        }

        /// <summary>
        /// Gets player's the items.
        /// </summary>
        /// <value>The player's items.</value>
        public Appearance.Clothing Clothing {
            get { return items; }
        }

        /// <summary>
        /// Gets the player's position.
        /// </summary>
        /// <value>The player's position.</value>
        public Position Position {
            get { return position; }
        }

        protected Player() {
            position = new Position(this);
        }

        public Player(string data) {
            position = new Position(this);
            LoadData(data);
        }

        protected void LoadData(string data) {
            string[] arr = data.Split("|".ToCharArray());
            id = int.Parse(arr[0]);
            username = arr[1];
            isMember = (int.Parse(arr[15]) != 0);
            memberDays = int.Parse(arr[16]);
            LoadItems(arr);
            LoadPosition(arr);
        }

        private void LoadItems(string[] arr) {
            int colour = int.Parse(arr[3]);
            int head = int.Parse(arr[4]);
            int face = int.Parse(arr[5]);
            int neck = int.Parse(arr[6]);
            int body = int.Parse(arr[7]);
            int hand = int.Parse(arr[8]);
            int feet = int.Parse(arr[9]);
            int flag = int.Parse(arr[10]);
            int background = int.Parse(arr[11]);
            items = new Appearance.Clothing(this, colour, head, face, neck, body, hand, feet, flag, background);
        }

        private void LoadPosition(string[] arr) {
            position.X = int.Parse(arr[12]);
            position.Y = int.Parse(arr[13]);
            position.frame = int.Parse(arr[14]);
        }

        protected void Spoke(Player player, string message) {
            if(player.OnSpeak != null) player.OnSpeak(player, message);
        }

        protected void Emotion(Player player, Emotes emote) {
            if(player.OnEmoticon != null) player.OnEmoticon(player, emote);
        }

        protected void Action(Player player, int action) {
            if(player.OnAction != null) player.OnAction(player, action);
        }

        protected void Snowball(Player player, int x, int y) {
            if(player.OnThrow != null) player.OnThrow(player, x, y);
        }

        /// <summary>
        /// Represents a message handler.
        /// </summary>
        class MessageHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "sm"; }
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
                GameConnection game = connection as GameConnection;
                if(game != null && packet.Arguments.Length >= 2) {
                    int id;
                    string message = packet.Arguments[1];
                    if(int.TryParse(packet.Arguments[0], out id) && id != game.Id) {
                        IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id); // Get every player with that id (there should only really be one..)
                        foreach(Player player in players) {
                            player.Spoke(player, message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Represents an emote handler.
        /// </summary>
        class EmoteHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "se"; }
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
                GameConnection game = connection as GameConnection;
                if(game != null && packet.Arguments.Length >= 2) {
                    try {
                        int id;
                        int emoteId;
                        if(int.TryParse(packet.Arguments[0], out id) && int.TryParse(packet.Arguments[1], out emoteId)) {
                            Emotes emote = (Emotes) emoteId;
                            IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id); // Get every player with that id (there should only really be one..)
                            foreach(Player player in players) {
                                player.Emotion(player, emote);
                            }
                        }
                    }catch(System.InvalidCastException) {
                        // Will be logged.
                    }
                }
            }
        }

        /// <summary>
        /// Represents an action handler.
        /// </summary>
        class ActionHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "sa"; }
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
                GameConnection game = connection as GameConnection;
                if(game != null && packet.Arguments.Length >= 2) {
                    int id;
                    int action;
                    if(int.TryParse(packet.Arguments[0], out id) && int.TryParse(packet.Arguments[1], out action)) {
                        IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id); // Get every player with that id (there should only really be one..)
                        foreach(Player player in players) {
                            player.Action(player, action);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Represents an snowball throw handler.
        /// </summary>
        class ThrowHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "sb"; }
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
                GameConnection game = connection as GameConnection;
                if(game != null && packet.Arguments.Length >= 3) {
                    int id;
                    int x;
                    int y;
                    if(int.TryParse(packet.Arguments[0], out id) && int.TryParse(packet.Arguments[1], out x) && int.TryParse(packet.Arguments[2], out y)) {
                        IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id); // Get every player with that id (there should only really be one..)
                        foreach(Player player in players) {
                            player.Snowball(player, x, y);
                        }
                    }
                }
            }
        }

    }
}
