using System;

namespace Sharpenguin {
    class ApiOKHandler : Packets.Receive.IDefaultPacketHandler<Packets.Receive.Xml.XmlPacket> {
        public string Handles {
            get { return "apiOK"; }
        }

        public void Handle(PenguinConnection connection, Packets.Receive.Xml.XmlPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection");
            if(packet == null) throw new System.ArgumentNullException("packet");
            connection.Send(new Packets.Send.Xml.RandomKey());
        }
    }
}

