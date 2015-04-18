using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System; 

namespace Sharpenguin.Configuration.Game {
    public class Emoticons {
        private List<Emoticon> emotes;

        public Emoticon this[int i] {
            get {
                IEnumerable<Emoticon> result = emotes.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentItemException("The emote with ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

        public Emoticons(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        private void Load(XLinq.XDocument document) {
            emotes = (
                from e in document.Root.Element("emoticons").Elements("emoticon") select new Emoticon {
                    Id = (int) e.Attribute("id"),
                    Emote = (string) e.Attribute("emote")
                }
            ).ToList();
        }

        public IEnumerable<Emoticon> Where(Func<Emoticon, bool> predictate) {
            return emotes.Where(predictate);
        }
    }
}

