namespace Sharpenguin.Configuration.System {
    /// <summary>
    /// Represents server details.
    /// </summary>
    public class Server {
        /// <summary>
        /// Gets or sets the server identifier.
        /// </summary>
        /// <value>The server identifier.</value>
        public int Id {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port {
            get;
            internal set;
        }
    }
}

