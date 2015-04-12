namespace Sharpenguin.Configuration.Game {
    public class Item {
        public int Id {
            get;
            internal set;
        }

        public int Price {
            get;
            internal set;
        }

        public string Description {
            get;
            internal set;
        }

        public bool Member {
            get;
            internal set;
        }

        public ItemType Type {
            get;
            internal set;
        }
    }
}

