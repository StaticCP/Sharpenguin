using System.Runtime.CompilerServices;

namespace Sharpenguin.Configuration.System {
    /// <summary>
    /// Represents a Club Penguin error.
    /// </summary>
    public class Error {
        /// <summary>
        /// Gets or sets the error's identifier.
        /// </summary>
        /// <value>The error's identifier.</value>
        public int Id {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string Message {
            get;
            internal set;
        }
    }
}

