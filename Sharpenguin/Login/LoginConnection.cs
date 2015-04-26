using System;

namespace Sharpenguin.Login {
    /// <summary>
    /// Represents a connection to the login server.
    /// </summary>
    public class LoginConnection : PenguinConnection {
        /// <summary>
        /// Occurs when a login is successfully completed.
        /// </summary>
        public event LoginEventHandler OnLogin;

        /// <summary>
        /// Gets or sets the internal room id.
        /// </summary>
        /// <value>The internal room.</value>
        public override int InternalRoom {
            get { return -1; } // The system room, -1.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Login.LoginConnection"/> class.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public LoginConnection(string username, string password) : base(username, password) {
            Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket>[] xml = HandlerLoader.GetHandlers<Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket>>();
            Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket>[] xt = HandlerLoader.GetHandlers<Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket>>();
            foreach(Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket> handler in xml) XmlHandlers.Add(handler);
            foreach(Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> handler in xt) XtHandlers.Add(handler);
        }

        /// <summary>
        /// Represents a handler for a random key packet.
        /// </summary>
        public class RandomKeyHandler : Packets.Receive.ILoginPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "rndK"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="connection">The connection the packet is for.</param>
            /// <param name="packet">The packet.</param>
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
            /// <param name="connection">The connection the packet is for.</param>
            /// <param name="packet">The packet.</param>
            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                LoginConnection login = connection as LoginConnection;
                if(login != null && packet.Arguments.Length >= 3) {
                    int id;
                    #if AS2
                    if(int.TryParse(packet.Arguments[0], out id)) {
                    #elif AS3
                    string[] userdata = packet.Arguments[0].Split('|');
                    if(int.TryParse(userdata[0], out id)) {
                    #endif
                        if(login.OnLogin != null) {
                            #if AS2
                            login.OnLogin(new Game.GameConnection(id, connection.Username, packet.Arguments[1]));
                            #elif AS3
                            System.Guid swid = new System.Guid(userdata[1]);
                            string username = userdata[2];
                            string loginkey = userdata[3];
                            //bool languageApproved = (userdata[5] == "true"); if ever it's needed..
                            //bool languageRejected = (userdata[6] == "true");
                            string friendsLoginKey = packet.Arguments[2];
                            string confirmationHash = packet.Arguments[1];
                            string email = packet.Arguments[4];
                            login.OnLogin(
                                // A lot of arguments, I'll lay it out like this.
                                new Game.GameConnection(
                                    id,
                                    swid,
                                    username,
                                    loginkey,
                                    friendsLoginKey,
                                    confirmationHash,
                                    email,
                                    packet.Arguments[0]
                                )
                            );
                            #endif
                        }
                        login.Disconnect();
                    }
                }
            }
        }

        /*
         * AS3 login data referenced from handleOnLogin in airtower.swf:
         *   var v5 = serverResponse[1].split('|');
         *   this.loginObject = {};
         *   this.loginObject.loginDataRaw = serverResponse[1];
         *   this.loginObject.playerID = v5[0];
         *   this.loginObject.swid = v5[1];
         *   this.loginObject.username = v5[2];
         *   this.loginObject.loginKey = v5[3];
         *   this.loginObject.languageApproved = v5[5];
         *   this.loginObject.languageRejected = v5[6];
         *   this.loginObject.friendsLoginKey = serverResponse[3];
         *   this.loginObject.confirmationHash = serverResponse[2];
         *   this.loginObject.emailAddress = serverResponse[5];
         */


    }
}

