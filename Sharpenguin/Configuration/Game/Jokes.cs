using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents a list of jokes.
    /// </summary>
    public class Jokes {
        /// <summary>
        /// The list of jokes.
        /// </summary>
        private List<Joke> jokes;

        /// <summary>
        /// Gets the <see cref="Sharpenguin.Configuration.Game.Jokes"/> with the specified identifier.
        /// </summary>
        /// <param name="i">The identifier.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.Game.Jokes"/> class.
        /// </summary>
        /// <param name="file">Configuration file.</param>
        public Jokes(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        /// <summary>
        /// Load the specified document.
        /// </summary>
        /// <param name="document">XML document.</param>
        private void Load(XLinq.XDocument document) {
            jokes = (
                from e in document.Root.Element("jokes").Elements("joke") select new Joke {
                    Id = (int) e.Attribute("id"),
                    Question = (string) e.Attribute("question"),
                    Answer = (string) e.Attribute("answer")
                }
            ).ToList();
        }

        /// <summary>
        /// Gets the joke matching the specified predicate.
        /// </summary>
        /// <param name="predicate">The search predicate.</param>
        public IEnumerable<Joke> Where(Func<Joke, bool> predicate) {
            return jokes.Where(predicate);
        }

        /// <summary>
        /// Gets a random joke.
        /// </summary>
        public Joke Random() {
            Random rnd = new Random();
            return jokes[rnd.Next(jokes.Count)];
        }
    }
}

