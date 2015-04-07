using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.Game {
    public class SafeChats {
        private List<Message> messages;

        public SafeChats(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        private void Load(XLinq.XDocument document) {
            messages = (
                from e in document.Root.Element("safe").Elements("message") select new Message {
                    Id = (int) e.Attribute("id"),
                    Text = (string) e.Attribute("text")
                }
            ).ToList();
        }
    }
}

