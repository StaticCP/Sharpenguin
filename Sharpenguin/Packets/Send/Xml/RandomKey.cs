using System;

namespace Sharpenguin.Packets.Send.Xml {
    public class RandomKey : Packet {
        public RandomKey() : base("<msg t='sys'><body action='rndK' r='-1'></body></msg>") {
        }
    }
}

