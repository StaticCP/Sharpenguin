using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System;

namespace Sharpenguin.Configuration.System {
    /// <summary>
    /// Represents a list of login servers.
    /// </summary>
    public class LoginServers {
        /// <summary>
        /// The list of login servers.
        /// </summary>
        private List<LoginServer> servers;

        /// <summary>
        /// Gets the <see cref="Sharpenguin.Configuration.System.LoginServers"/> with the specified identifier.
        /// </summary>
        /// <param name="i">The identifier.</param>
        public LoginServer this[int i] {
            get {
                IEnumerable<LoginServer> result = servers.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentErrorException("The error with ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.System.LoginServers"/> class.
        /// </summary>
        /// <param name="file">Configuration file.</param>
        public LoginServers(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        /// <summary>
        /// Load the specified document.
        /// </summary>
        /// <param name="document">XML document.</param>
        private void Load(XLinq.XDocument document) {
            servers = (
                from e in document.Root.Element("login").Elements("server") select new LoginServer {
                    Id = (int) e.Attribute("id"),
                    Host = (string) e.Attribute("host"),
                    Port = (int) e.Attribute("port"),
                }
            ).ToList();
        }

        /// <summary>
        /// Get the login servers matching the specified predictate.
        /// </summary>
        /// <param name="predictate">The search predictate.</param>
        public IEnumerable<LoginServer> Where(Func<LoginServer, bool> predictate) {
            return servers.Where(predictate);
        }

        /// <summary>
        /// Gets a random login server.
        /// </summary>
        public LoginServer Random() {
            Random rnd = new Random();
            return servers[rnd.Next(servers.Count)];
        }
    }
}