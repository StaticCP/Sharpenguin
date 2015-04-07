using System;

namespace Sharpenguin.Packets.Send.Xml {
    public class VersionCheck : Packets.Send.Packet {
        public VersionCheck(int version) : base("<msg t='sys'><body action='verChk' r='0'><ver v='" + version.ToString() + "' /></body></msg>") {
        }
    }
}

