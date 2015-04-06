namespace Sharpenguin.Packets.Receive {
    public abstract class Packet {
        /// <summary>
        /// The packet's data.
        /// </summary>
        protected string data = "";
        /// <summary>
        /// The packet's command.
        /// </summary>
        protected string command = "";

        /// <summary>
        /// Gets the packet's data.
        /// </summary>
        /// <value>
        /// The packet's data.
        /// </value>
        public string Data {
            get { return data; }
        }

        /// <summary>
        /// Gets the packet's command.
        /// </summary>
        /// <value>
        /// The packet's command.
        /// </value>
        public string Command {
            get { return command; }
        }

        /// <summary>
        /// Gets the packet's length.
        /// </summary>
        /// <value>
        /// The packet's length.
        /// </value>
        public int Length {
            get { return data.Length; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HPPS.API.Packets.Receive.Packet"/> class.
        /// </summary>
        /// <param name='packet'>
        /// The packet data.
        /// </param>
        public Packet(string packet) {
            data = packet;
        }
    }
}