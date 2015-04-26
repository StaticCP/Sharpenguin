namespace Sharpenguin.Game.Player {
    using GCollections = System.Collections.Generic;

    /// <summary>
    /// Represents your own player.
    /// </summary>
    public class MyPlayer : Player {
        private int age             = 0; //< The age of the player.
        private int memberRemaining = 0; //< How many membership days the player has remaining.
        private int minutes         = 0; //< The minutes the player has spent playing the game.
        private Money.Wallet wallet = new Money.Wallet();
        private Inventory.Inventory inventory;
        private Relations.Buddies.Buddies buddies;
        private GameConnection connection;

        /// <summary>
        /// Gets the player's age.
        /// </summary>
        /// <value>The player's age.</value>
        public int Age {
            get { return age; }
        }

        /// <summary>
        /// Gets the player's wallet.
        /// </summary>
        /// <value>The player's wallet.</value>
        public Money.Wallet Wallet {
            get { return wallet; }
        }

        /// <summary>
        /// Gets the player's buddies.
        /// </summary>
        /// <value>The player's buddies.</value>
        public Relations.Buddies.Buddies Buddies {
            get { return buddies; }
        }

        /// <summary>
        /// Gets or sets the player's remaining membership days.
        /// </summary>
        /// <value>The player's remaining membership days.</value>
        public int MemberRemaining {
            get { return memberRemaining; }
            internal set { memberRemaining = value; }
        }

        /// <summary>
        /// Gets or sets the minutes played.
        /// </summary>
        /// <value>The minutes played.</value>
        public int MinutesPlayed {
            get { return minutes; }
            internal set { minutes = value; }
        }
        /// <summary>
        /// Gets the room the player is currently in.
        /// </summary>
        /// <value>The room the player is currently in.</value>
        public Room.Room Room {
            get { return connection.Room; }
        }

        /// <summary>
        /// Gets the player's inventory.
        /// </summary>
        /// <value>The inventory.</value>
        public Inventory.Inventory Inventory {
            get { return inventory; }
        }

        /// <summary>
        /// Gets the player's connection.
        /// </summary>
        /// <value>The connection.</value>
        public GameConnection Connection {
            get { return connection; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.MyPlayer"/> class.
        /// </summary>
        /// <param name="connection">Connection.</param>
        /// <param name="packet">Packet.</param>
        public MyPlayer(GameConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            this.connection = connection;
            inventory = new Inventory.Inventory(this);
            buddies = new Relations.Buddies.Buddies(this);
            LoadData(packet.Arguments[0]);
            int coins, age, minutes;
            if(int.TryParse(packet.Arguments[1], out coins) && int.TryParse(packet.Arguments[5], out age) && int.TryParse(packet.Arguments[7], out minutes)) {
                Wallet.Amount = coins;
                this.age = age;
                this.minutes = minutes;
            }
        }

        /// <summary>
        /// Make the player say the message.
        /// </summary>
        /// <param name="message">The message to say.</param>
        public void Say(string message) {
            if(message == null) throw new System.ArgumentNullException("message", "Argument cannot be null.");
            connection.Send(new Packets.Send.Xt.Player.Message(connection, message));
            Spoke(this, message);
        }

        /// <summary>
        /// Make the player say the emote.
        /// </summary>
        /// <param name="emote">The emote to say.</param>
        public void Say(Emotes emote) {
            connection.Send(new Packets.Send.Xt.Player.Emoticon(connection, (int) emote));
            Emotion(this, emote);
        }

        /// <summary>
        /// Make the player do the specified action.
        /// </summary>
        /// <param name="action">The identifier of the action to do.</param>
        public void Do(Actions action) {
            connection.Send(new Packets.Send.Xt.Player.Action(connection, (int) action));
            Action(this, action);
        }

        /// <summary>
        /// Make the player throw a snowball to the specified x and y coordinates.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void Throw(int x, int y) {
            connection.Send(new Packets.Send.Xt.Player.Snowball(connection, x, y));
            Snowball(this, x, y);
        }

        /// <summary>
        /// Make the player dance.
        /// </summary>
        public void Dance() {
            Position.Frame = 26;
        }

        /// <summary>
        /// Make the player sit the specified direction.
        /// </summary>
        /// <param name="direction">Sit direction.</param>
        public void Sit(Sit direction) {
            Position.Frame = (int) direction;
        }

        /// <summary>
        /// Makes the player join the specified room and enter at the specified x and y coordinates.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void Enter(Configuration.Game.Room room, int x = 0, int y = 0) {
            if(room == null) throw new System.ArgumentNullException("room", "Argument cannot be null.");
            Enter(room.Id);
        }

        /// <summary>
        /// Makes the player join the specified room and enter at the specified x and y coordinates.
        /// </summary>
        /// <param name="id">Room identifier.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void Enter(int id, int x = 0, int y = 0) {
            if(id <= 1000) {
                connection.Send(new Packets.Send.Xt.Room.JoinRoom(connection, id, x, y));
            } else {
                connection.Send(new Packets.Send.Xt.Room.GetIglooDetails(connection, id));
                connection.Send(new Packets.Send.Xt.Player.GetPuffle(connection, id));
                connection.Send(new Packets.Send.Xt.Room.JoinPlayer(connection, id));
            }
        }

    }

}
