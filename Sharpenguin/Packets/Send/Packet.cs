namespace Sharpenguin.Packets.Send {
    /// <summary>
    /// Represents a packet to be sent.
    /// </summary>
    public abstract class Packet {
        private string data;
        private int length;

        /// <summary>
        /// Gets the packet's data.
        /// </summary>
        /// <value>The data.</value>
        public string Data {
            get { return data; }
        }

        /// <summary>
        /// Gets the packet's length.
        /// </summary>
        /// <value>The length.</value>
        public int Length {
            get { return length; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Send.Packet"/> class.
        /// </summary>
        /// <param name="packet">Raw packet data.</param>
        public Packet(string packet) {
            data = packet;
            length = data.Length;
        }
    }
}
