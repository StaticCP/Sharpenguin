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
        private int frame;

        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>The x.</value>
        public int X {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>The y.</value>
        public int Y {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// Gets or sets the frame ID.
        /// </summary>
        /// <value>The frame.</value>
        public int Frame {
            get { return frame; }
            set { frame = value; }
        }
    }
}

