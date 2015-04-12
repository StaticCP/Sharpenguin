using System;

namespace Sharpenguin {
    public class ApiOKHandler : Packets.Receive.IDefaultPacketHandler<Packets.Receive.Xml.XmlPacket> {
        public string Handles {
            get { return "apiOK"; }
        }

        public void Handle(PenguinConnection connection, Packets.Receive.Xml.XmlPacket packet) {
            connection.Send(new Packets.Send.Xml.RandomKey());
        }
    }
}

