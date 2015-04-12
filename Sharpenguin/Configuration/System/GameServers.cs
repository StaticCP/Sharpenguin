using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.System {
    public class GameServers {
        private List<GameServer> servers;

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

        public GameServers(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

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
    }
}