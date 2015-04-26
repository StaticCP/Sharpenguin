namespace Sharpenguin.Configuration.System {
    /// <summary>
    /// Represents game server details.
    /// </summary>
    public class GameServer : Server {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Sharpenguin.Configuration.System.GameServer"/> is a safe chat server.
        /// </summary>
        /// <value><c>true</c> if it is a safe chat server; otherwise, <c>false</c>.</value>
        public bool Safe {
            get;
            internal set;
        }
    }
}

