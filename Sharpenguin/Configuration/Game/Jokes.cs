using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.Game {
    public class Jokes {
        private List<Joke> jokes;

        public Jokes(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        private void Load(XLinq.XDocument document) {
            jokes = (
                from e in document.Root.Element("jokes").Elements("joke") select new Joke {
                    Id = (int) e.Attribute("id"),
                    Question = (string) e.Attribute("question"),
                    Answer = (string) e.Attribute("answer")
                }
            ).ToList();
        }
    }
}

