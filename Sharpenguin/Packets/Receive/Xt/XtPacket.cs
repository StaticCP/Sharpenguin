namespace Sharpenguin.Packets.Receive.Xt {
    public sealed class XtPacket : Packet {
        /// <summary>
        /// The Xt parser.
        /// </summary>
        private XtParser parser;

        /// <summary>
        /// Gets the room that this packet was sent to.
        /// </summary>
        /// <value>
        /// The room that this packet was sent to.
        /// </value>
        public int Room {
            get { return parser.Room; }
        }

        /// <summary>
        /// Gets the arguments of this packet.
        /// </summary>
        /// <value>
        /// The arguments of this packet.
        /// </value>
        public string[] Arguments {
            get { return parser.Arguments; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HPPS.API.Packets.Receive.Xt.XtPacket"/> class.
        /// </summary>
        /// <param name='data'>
        /// The packet's data.
        /// </param>
        public XtPacket(string data) : base(data) {
            parser = new XtParser(data);
            command = parser.Command;
        }
    }
}