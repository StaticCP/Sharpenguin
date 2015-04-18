/**
 * @file MyPlayer
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Game.Player {
    using GCollections = System.Collections.Generic;

    /**
     * An extension class of player for your own player.
     */
    public class MyPlayer : Player {
        private int age             = 0; //< The age of the player.
        private int memberRemaining = 0; //< How many membership days the player has remaining.
        private int minutes         = 0; //< The minutes the player has spent playing the game.
        private Money.Wallet wallet = new Money.Wallet();
        private Inventory.Inventory inventory;
        private GameConnection connection;

        //! Gets the player's age.
        public int Age {
            get { return age; }
        }
        public Money.Wallet Wallet {
            get { return wallet; }
        }
        //! Gets the amount membership days remaining for the player.
        public int MemberRemaining {
            get { return memberRemaining; }
            internal set { memberRemaining = value; }
        }
        //! Gets the amount of minutes played by the player.
        public int MinutesPlayed {
            get { return minutes; }
            internal set { minutes = value; }
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
        /// <param name="connection">They player's parent connection.</param>
        public MyPlayer(GameConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            this.connection = connection;
            inventory = new Inventory.Inventory(this);
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
        public void Do(int action) {
            connection.Send(new Packets.Send.Xt.Player.Action(connection, action));
            Action(this, action);
        }

        public void Throw(int x, int y) {
            connection.Send(new Packets.Send.Xt.Player.Snowball(connection, x, y));
            Snowball(this, x, y);
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
