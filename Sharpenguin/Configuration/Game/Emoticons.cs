using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.Game {
    public class Emoticons {
        private List<Emoticon> emotes;

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
    }
}

