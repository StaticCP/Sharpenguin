using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;

namespace Sharpenguin.Configuration.System {
    public class Errors {
        private List<Error> errors;

        public Error this[int i] {
            get {
                IEnumerable<Error> result = errors.Where(p => p.Id == i);
                if(result.Count() != 0) {
                    return result.First();
                } else {
                    throw new NonExistentErrorException("The error with ID '" + i + "' does not exist in the configuration!");
                }
            }
        }

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