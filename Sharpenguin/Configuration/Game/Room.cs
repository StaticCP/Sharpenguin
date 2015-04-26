using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents the details of a room.
    /// </summary>
    public class Room {
        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>The room identifier.</value>
        public int Id {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the room name.
        /// </summary>
        /// <value>The room name.</value>
        public string Name {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a member room.
        /// </summary>
        /// <value><c>true</c> if this instance is a member room; otherwise, <c>false</c>.</value>
        public bool IsMember {
            get;
            internal set;
        }
    }
}

