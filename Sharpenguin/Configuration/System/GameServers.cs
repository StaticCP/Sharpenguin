using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System;

namespace Sharpenguin.Configuration.System {
    /// <summary>
    /// Represents a list of game servers.
    /// </summary>
    public class GameServers {
        /// <summary>
        /// The list of game servers.
        /// </summary>
        private List<GameServer> servers;

        /// <summary>
        /// Gets the <see cref="Sharpenguin.Configuration.System.GameServers"/> with the specified identifier.
        /// </summary>
        /// <param name="i">The identifier.</param>
        public GameServer this[int i] {
            get {
                IEnumerable<GameServer> result = servers.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentErrorException("The server with ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.System.GameServers"/> class.
        /// </summary>
        /// <param name="file">Configuration file.</param>
        public GameServers(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        /// <summary>
        /// Load the specified document.
        /// </summary>
        /// <param name="document">XML Document.</param>
        private void Load(XLinq.XDocument document) {
            servers = (
                from e in document.Root.Element("game").Elements("server") select new GameServer {
                    Id = (int) e.Attribute("id"),
                    Name = (string) e.Attribute("name"),
                    Host = (string) e.Attribute("host"),
                    Port = (int) e.Attribute("port"),
                    Safe = (((int) e.Attribute("safe")) == 1)
                }
            ).ToList();
        }

        /// <summary>
        /// Gets the servers matching the specified predicate.
        /// </summary>
        /// <param name="predicate">Search predicate.</param>
        public IEnumerable<GameServer> Where(Func<GameServer, bool> predicate) {
            return servers.Where(predicate);
        }

        /// <summary>
        /// Gets a random game server.
        /// </summary>
        public GameServer Random() {
            Random rnd = new Random();
            return servers[rnd.Next(servers.Count)];
        }
    }
}