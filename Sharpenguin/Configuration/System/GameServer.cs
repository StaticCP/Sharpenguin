namespace Sharpenguin.Configuration.System {
    public class GameServer : Server {
        public string Name {
            get;
            internal set;
        }

        public bool Safe {
            get;
            internal set;
        }
    }
}

