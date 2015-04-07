using System;

namespace Sharpenguin.Login {
    public class LoginConnection : PenguinConnection {
        public event LoginEventHandler OnLogin; //< Event for handling login success.

        public LoginConnection(string username, string password) : base(username, password) {
            Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket>[] xml = HandlerLoader.GetHandlers<Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket>>();
            Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket>[] xt = HandlerLoader.GetHandlers<Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket>>();
            foreach(Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket> handler in xml) XmlHandlers.Add(handler);
            foreach(Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> handler in xt) XtHandlers.Add(handler);
        }

        public class RandomKeyHandler : Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket> {
            public string Handles {
                get { return "rndk"; }
            }

            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xml.XmlPacket packet) {
                LoginConnection login = connection as LoginConnection;
                login.rndk = packet.XmlData.ChildNodes[0].InnerText;
                string hash = Security.Crypt.HashPassword(login.password, login.rndk);
                login.Send(new Sharpenguin.Packets.Send.Xml.Login(login.Username, hash));
            }
        }

        /// <summary>
        /// Represents an authentication handler.
        /// </summary>
        public class AuthenticationHandler : Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "l"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="connection">The connection that received the packet.</param>
            /// <param name="packet">The packet.</param>
            /// <param name="connection">Connection.</param>
            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                LoginConnection login = connection as LoginConnection;
                if(login != null && packet.Arguments.Length >= 3) {
                    int id;
                    if(int.TryParse(packet.Arguments[2], out id)) {
                        Game.GameConnection game = new Game.GameConnection(id, connection.Username, packet.Arguments[2]);
                        login.OnLogin(game);
                        login.Disconnect();
                    }
                }
            }
        }

    }
}

