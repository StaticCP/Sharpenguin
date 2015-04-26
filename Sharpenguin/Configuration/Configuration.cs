namespace Sharpenguin.Configuration {
    /// <summary>
    /// Represents the Sharpenguin configuration.
    /// </summary>
    public static class Configuration {
        private static readonly Game.Jokes jokes = new Game.Jokes("Configuration/Chat.xml");
        private static readonly Game.Items items = new Game.Items("Configuration/Items.xml");
        private static readonly Game.SafeChats safe = new Game.SafeChats("Configuration/Chat.xml");
        private static readonly Game.Rooms rooms = new Game.Rooms("Configuration/Rooms.xml");
        private static readonly System.Errors errors = new System.Errors("Configuration/Errors.xml");
        private static readonly System.GameServers games = new System.GameServers("Configuration/Servers.xml");
        private static readonly System.LoginServers logins = new System.LoginServers("Configuration/Servers.xml");
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Sharpenguin");

        /// <summary>
        /// Gets the joke list.
        /// </summary>
        /// <value>The joke list.</value>
        public static Game.Jokes Jokes {
            get { return jokes; }
        }

        /// <summary>
        /// Gets the item list.
        /// </summary>
        /// <value>The item list.</value>
        public static Game.Items Items {
            get { return items; }
        }

        /// <summary>
        /// Gets the list of safe messages.
        /// </summary>
        /// <value>The list of safe messages.</value>
        public static Game.SafeChats SafeMessages {
            get { return safe; }
        }

        /// <summary>
        /// Gets the list of rooms.
        /// </summary>
        /// <value>The list of rooms.</value>
        public static Game.Rooms Rooms {
            get { return rooms; }
        }

        /// <summary>
        /// Gets the list of errors.
        /// </summary>
        /// <value>The list of errors.</value>
        public static System.Errors Errors {
            get { return errors; }
        }

        /// <summary>
        /// Gets the list of login servers.
        /// </summary>
        /// <value>The list of login servers.</value>
        public static System.LoginServers LoginServers {
            get { return logins; }
        }

        /// <summary>
        /// Gets the list of game servers.
        /// </summary>
        /// <value>The list of game servers.</value>
        public static System.GameServers GameServers {
            get { return games; }
        }

        /// <summary>
        /// Gets the Sharpenguin logger.
        /// </summary>
        /// <value>The sharpenguin logger.</value>
        internal static log4net.ILog Logger {
            get { return log; }
        }
    }
}

