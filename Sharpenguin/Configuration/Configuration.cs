namespace Sharpenguin.Configuration {
    public class Configuration {
        private readonly Game.Emoticons emoticons = new Game.Emoticons("Configuration/Chat.xml");
        private readonly Game.Jokes jokes = new Game.Jokes("Configuration/Chat.xml");
        private readonly Game.Items items = new Game.Items("Configuration/Items.xml");
        private readonly Game.SafeChats safe = new Game.SafeChats("Configuration/Chat.xml");
        private readonly System.Errors errors = new System.Errors("Configuration/Errors.xml");
        private readonly System.GameServers games = new System.GameServers("Configuration/Servers.xml");
        private readonly System.LoginServers logins = new System.LoginServers("Configuration/Servers.xml");

        public Game.Emoticons Emoticons {
            get { return emoticons; }
        }

        public Game.Jokes Jokes {
            get { return jokes; }
        }

        public Game.Items Items {
            get { return items; }
        }

        public Game.SafeChats SafeMessages {
            get { return safe; }
        }

        public System.Errors Errors {
            get { return errors; }
        }

        public System.LoginServers LoginServers {
            get { return logins; }
        }

        public System.GameServers GameServers {
            get { return games; }
        }
    }
}

