using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.System {
    public class LoginServers {
        private List<LoginServer> servers;

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

        public LoginServers(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        private void Load(XLinq.XDocument document) {
            servers = (
                from e in document.Root.Element("login").Elements("server") select new LoginServer {
                    Id = (int) e.Attribute("id"),
                    Host = (string) e.Attribute("host"),
                    Port = (int) e.Attribute("port"),
                }
            ).ToList();
        }
    }
}