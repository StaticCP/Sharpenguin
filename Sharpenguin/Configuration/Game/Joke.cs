namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Represents a joke.
    /// </summary>
    public class Joke {

        /// <summary>
        /// Gets or sets the joke identifier.
        /// </summary>
        /// <value>The joke identifier.</value>
        public int Id {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the joke question.
        /// </summary>
        /// <value>The joke question.</value>
        public string Question {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the joke answer (punchline).
        /// </summary>
        /// <value>The joke answer.</value>
        public string Answer {
            get;
            internal set;
        }
    }
}