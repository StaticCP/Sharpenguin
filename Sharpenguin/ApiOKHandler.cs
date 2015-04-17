using System;

namespace Sharpenguin {
    class ApiOKHandler : Packets.Receive.IDefaultPacketHandler<Packets.Receive.Xml.XmlPacket> {
        public string Handles {
            get { return "apiOK"; }
        }

        public void Handle(PenguinConnection connection, Packets.Receive.Xml.XmlPacket packet) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            connection.Send(new Packets.Send.Xml.RandomKey());
        }
    }
}

