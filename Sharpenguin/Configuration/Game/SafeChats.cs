using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System;

namespace Sharpenguin.Configuration.Game {
    public class SafeChats {
        private List<Message> messages;

        public Message this[int i] {
            get {
                IEnumerable<Message> result = messages.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentMessageException("The Message with ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

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

        public IEnumerable<Message> Where(Func<Message, bool> predictate) {
            return messages.Where(predictate);
        }
    }
}

