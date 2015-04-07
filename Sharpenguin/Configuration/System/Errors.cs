using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.System {
    public class Errors {
        private List<Error> errors;

        public Errors(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        private void Load(XLinq.XDocument document) {
            errors = (
                from e in document.Root.Elements("error") select new Error {
                    Id = (int) e.Attribute("id"),
                    Message = (string) e.Attribute("message")
                }
            ).ToList();
        }
    }
}