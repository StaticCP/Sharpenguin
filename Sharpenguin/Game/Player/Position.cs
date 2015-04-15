namespace Sharpenguin.Game.Player {
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
            if(player == null) throw new System.ArgumentNullException("player");
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
            } else {
                throw new NotMeException("The position of other players cannot be set!");
            }
        }
    }
}

