using System;

namespace Sharpenguin.Packets.Send.Xml {
    public class Login : Packet {
        public Login(string username, string hash) : base("<msg t='sys'><body action='login' r='0'><login z='w1'><nick><![CDATA[" + username + "]]></nick><pword><![CDATA[" + hash + "]]></pword></login></body></msg>") {
        }
    }
}

