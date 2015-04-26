using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents a list of safe chat messages.
    /// </summary>
    public class SafeChats {
        /// <summary>
        /// The a list of safe chat messages.
        /// </summary>
        private List<Message> messages;

        /// <summary>
        /// Gets the <see cref="Sharpenguin.Configuration.Game.Message"/> with the specified i.
        /// </summary>
        /// <param name="i">The index.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.SafeChats"/> class.
        /// </summary>
        /// <param name="file">Configuration file.</param>
        public SafeChats(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        /// <summary>
        /// Load the safe chat messages from the specified XML document.
        /// </summary>
        /// <param name="document">The XML document to load the messages from.</param>
        private void Load(XLinq.XDocument document) {
            messages = (
                from e in document.Root.Element("safe").Elements("message") select new Message {
                    Id = (int) e.Attribute("id"),
                    Text = (string) e.Attribute("text")
                }
            ).ToList();
        }

        /// <summary>
        /// Searches for all messages matching the given predictate.
        /// </summary>
        /// <param name="predictate">The search predictate.</param>
        public IEnumerable<Message> Where(Func<Message, bool> predictate) {
            return messages.Where(predictate);
        }

        /// <summary>
        /// Gets a random message.
        /// </summary>
        public Message Random() {
            Random rnd = new Random();
            return messages[rnd.Next(messages.Count)];
        }
    }
}

