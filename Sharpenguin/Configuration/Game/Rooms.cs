using System;
using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents a list of rooms.
    /// </summary>
    public class Rooms {
        /// <summary>
        /// The list of rooms.
        /// </summary>
        private List<Room> rooms;

        /// <summary>
        /// Gets the <see cref="Sharpenguin.Configuration.Game.Rooms"/> with the specified identifier.
        /// </summary>
        /// <param name="i">The identifier.</param>
        public Room this[int i] {
            get {
                IEnumerable<Room> result = rooms.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentRoomException("The room with external ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.Rooms"/> class.
        /// </summary>
        /// <param name="file">Configuration file.</param>
        public Rooms(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        /// <summary>
        /// Load the specified document.
        /// </summary>
        /// <param name="document">The XML document.</param>
        private void Load(XLinq.XDocument document) {
            rooms = (
                from e in document.Root.Elements("room") select new Room {
                    Id = (int) e.Attribute("id"),
                    Name = (string) e.Attribute("name"),
                    IsMember = (((int) e.Attribute("member")) == 1)
                }
            ).ToList();
        }

        /// <summary>
        /// Gets rooms matching the specified predictate.
        /// </summary>
        /// <param name="predictate">Search predictate.</param>
        public IEnumerable<Room> Where(Func<Room, bool> predictate) {
            return rooms.Where(predictate);
        }

        /// <summary>
        /// Gets a random room.
        /// </summary>
        public Room Random() {
            Random rnd = new Random();
            return rooms[rnd.Next(rooms.Count)];
        }
    }
}

