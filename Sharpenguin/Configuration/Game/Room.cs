using System;

namespace Sharpenguin.Configuration.Game {
    public class Room {
        public int Id {
            get;
            internal set;
        }

        public string Name {
            get;
            internal set;
        }

        public bool IsMember {
            get;
            internal set;
        }
    }
}

