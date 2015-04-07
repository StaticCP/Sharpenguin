namespace Sharpenguin.Game.Player {
    /// <summary>
    /// Represents the items a player is wearing.
    /// </summary>
    public class Items {
        private int colour;
        private int head;
        private int face;
        private int neck;
        private int body;
        private int hand;
        private int feet;
        private int flag;
        private int background;

         /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        /// <value>The colour.</value>
        public int Colour {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// Gets or sets the head.
        /// </summary>
        /// <value>The head.</value>
        public int Head {
            get { return head; }
            set { head = value; }
        }

        /// <summary>
        /// Gets or sets the face.
        /// </summary>
        /// <value>The face.</value>
        public int Face {
            get { return face; }
            set { face = value; }
        }

        /// <summary>
        /// Gets or sets the neck.
        /// </summary>
        /// <value>The neck.</value>
        public int Neck {
            get { return neck; }
            set { neck = value; }
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public int Body {
            get { return body; }
            set { body = value; }
        }

        /// <summary>
        /// Gets or sets the hand.
        /// </summary>
        /// <value>The hand.</value>
        public int Hand {
            get { return hand; }
            set { hand = value; }
        }

        /// <summary>
        /// Gets or sets the feet.
        /// </summary>
        /// <value>The feet.</value>
        public int Feet {
            get { return feet; }
            set { feet = value; }
        }

        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        /// <value>The flag.</value>
        public int Flag {
            get { return flag; }
            set { flag = value; }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public int Background {
            get { return background; }
            set { background = value; }
        }

    }
}

