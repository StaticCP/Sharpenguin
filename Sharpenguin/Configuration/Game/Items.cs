using System;
using System.Collections.Generic;
using System.Linq;
using XLinq = System.Xml.Linq;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents a list of items.
    /// </summary>
    public class Items {
        /// <summary>
        /// The list of items.
        /// </summary>
        private List<Item> items;

        /// <summary>
        /// Gets the <see cref="Sharpenguin.Configuration.Game.Item"/> with the specified i.
        /// </summary>
        /// <param name="i">The index.</param>
        public Item this[int i] {
            get {
                IEnumerable<Item> result = items.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentItemException("The item with ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.Items"/> class.
        /// </summary>
        /// <param name="file">File.</param>
        public Items(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        /// <summary>
        /// Load the items configuration from the specified XML document.
        /// </summary>
        /// <param name="document">The XML document.</param>
        private void Load(XLinq.XDocument document) {
            items = (
                from e in document.Root.Elements("item") select new Item {
                    Id = (int) e.Attribute("id"),
                    Type = (ItemType) Enum.Parse(typeof(ItemType), (string) e.Attribute("type"), true),
                    Price = (int) e.Attribute("price"),
                    Member = (((int) e.Attribute("member")) == 1)
                }
            ).ToList();
        }

        /// <summary>
        /// Gets all items matching the given predictate.
        /// </summary>
        /// <param name="predictate">Predictate of the search.</param>
        public IEnumerable<Item> Where(Func<Item, bool> predictate) {
            return items.Where(predictate);
        }

        /// <summary>
        /// Gets a random item.
        /// </summary>
        public Item Random() {
            Random rnd = new Random();
            return items[rnd.Next(items.Count)];
        }
    }
}

