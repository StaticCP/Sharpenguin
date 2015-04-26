using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents a safe message.
    /// </summary>
    public class Message {
        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public int Id {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <value>The message text.</value>
        public string Text {
            get;
            internal set;
        }
    }
}

