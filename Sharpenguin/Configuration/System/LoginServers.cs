using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.System {
    public class LoginServers {
        private List<LoginServer> servers;

        public LoginServers(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        private void Load(XLinq.XDocument document) {
            servers = (
                from e in document.Root.Element("login").Elements("server") select new LoginServer {
                    Id = (int) e.Attribute("id"),
                    Name = (string) e.Attribute("name"),
                    Host = (string) e.Attribute("host"),
                    Port = (int) e.Attribute("port"),
                }
            ).ToList();
        }
    }
}