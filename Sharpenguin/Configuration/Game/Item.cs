namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents a clothing item.
    /// </summary>
    public class Item {
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        public int Id {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public int Price {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Sharpenguin.Configuration.Game.Item"/> is a member item.
        /// </summary>
        /// <value><c>true</c> if it is a member item; otherwise, <c>false</c>.</value>
        public bool Member {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the type of item.
        /// </summary>
        /// <value>The type of item.</value>
        public ItemType Type {
            get;
            internal set;
        }
    }
}

