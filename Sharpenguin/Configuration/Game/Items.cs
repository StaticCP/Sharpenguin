using System;
using System.Collections.Generic;
using System.Linq;
using XLinq = System.Xml.Linq;

namespace Sharpenguin.Configuration.Game {
    public class Items {
        private List<Item> items;

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

        public Items(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

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

        public IEnumerable<Item> Where(Func<Item, bool> predictate) {
            return items.Where(predictate);
        }
    }
}

