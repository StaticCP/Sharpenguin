namespace Sharpenguin.Configuration {
    public static class Configuration {
        private static Game.Jokes jokes = new Game.Jokes("Configuration/Chat.xml");
        private static Game.Items items = new Game.Items("Configuration/Items.xml");
        private static Game.SafeChats safe = new Game.SafeChats("Configuration/Chat.xml");
        private static Game.Rooms rooms = new Game.Rooms("Configuration/Rooms.xml");
        private static System.Errors errors = new System.Errors("Configuration/Errors.xml");
        private static System.GameServers games = new System.GameServers("Configuration/Servers.xml");
        private static System.LoginServers logins = new System.LoginServers("Configuration/Servers.xml");

        public static Game.Jokes Jokes {
            get { return jokes; }
        }

        public static Game.Items Items {
            get { return items; }
        }

        public static Game.SafeChats SafeMessages {
            get { return safe; }
        }

        public static Game.Rooms Rooms {
            get { return rooms; }
        }

        public static System.Errors Errors {
            get { return errors; }
        }

        public static System.LoginServers LoginServers {
            get { return logins; }
        }

        public static System.GameServers GameServers {
            get { return games; }
        }
    }
}

