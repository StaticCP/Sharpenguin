using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.Game {
    public class Rooms {
        private List<Room> rooms;

        public Rooms(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        private void Load(XLinq.XDocument document) {
            rooms = (
                from e in document.Root.Elements("room") select new Room {
                    Id = (int) e.Attribute("id"),
                    Name = (string) e.Attribute("name"),
                    IsMember = (((int) e.Attribute("member")) == 1)
                }
            ).ToList();
        }
    }
}

