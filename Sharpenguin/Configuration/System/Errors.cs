using System.Collections.Generic;
using XLinq = System.Xml.Linq;
using System.Linq;
using System;

namespace Sharpenguin.Configuration.System {
    /// <summary>
    /// Represents the error list.
    /// </summary>
    public class Errors {
        /// <summary>
        /// The error list.
        /// </summary>
        private List<Error> errors;

        /// <summary>
        /// Gets the <see cref="Sharpenguin.Configuration.System.Errors"/> with the specified identifier.
        /// </summary>
        /// <param name="i">The identifier.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Configuration.System.Errors"/> class.
        /// </summary>
        /// <param name="file">Configuration file.</param>
        public Errors(string file) {
            XLinq.XDocument document = XLinq.XDocument.Load(file);
            Load(document);
        }

        /// <summary>
        /// Load the specified XML document.
        /// </summary>
        /// <param name="document">XML Document.</param>
        private void Load(XLinq.XDocument document) {
            errors = (
                from e in document.Root.Elements("error") select new Error {
                    Id = (int) e.Attribute("id"),
                    Message = (string) e.Attribute("message")
                }
            ).ToList();
        }

        /// <summary>
        /// Gets the error matching the given predicate
        /// </summary>
        /// <param name="predicate">Search predicate.</param>
        public IEnumerable<Error> Where(Func<Error, bool> predicate) {
            return errors.Where(predicate);
        }
    }
}