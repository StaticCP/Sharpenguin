using System;

namespace Sharpenguin.Packets.Send.Xml {
    /// <summary>
    /// Represents a login authentication packet.
    /// </summary>
    public class Login : Packet {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Send.Xml.Login"/> class.
        /// </summary>
        /// <param name="username">Penguin username.</param>
        /// <param name="hash">Password hash.</param>
        public Login(string username, string hash) : base("<msg t='sys'><body action='login' r='0'><login z='w1'><nick><![CDATA[" + username + "]]></nick><pword><![CDATA[" + hash + "]]></pword></login></body></msg>") {
        }
    }
}

