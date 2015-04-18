using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System;

namespace Sharpenguin.Configuration.Game {
    public class Jokes {
        private List<Joke> jokes;

        public Joke this[int i] {
            get {
                IEnumerable<Joke> result = jokes.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentJokeException("The joke with ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

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

        public IEnumerable<Joke> Where(Func<Joke, bool> predictate) {
            return jokes.Where(predictate);
        }
    }
}

