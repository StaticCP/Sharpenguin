using System;

namespace Sharpenguin.Packets.Send.Xml {
    /// <summary>
    /// Represents a random key packet.
    /// </summary>
    public class RandomKey : Packet {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Send.Xml.RandomKey"/> class.
        /// </summary>
        public RandomKey() : base("<msg t='sys'><body action='rndK' r='-1'></body></msg>") {
        }
    }
}

