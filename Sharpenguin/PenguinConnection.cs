namespace Sharpenguin {

    /// <summary>
    /// Connection success event handler.
    /// </summary>
    public delegate void ConnectionSuccessEventHandler(string host, int port);
    /// <summary>
    /// Connection fail event handler.
    /// </summary>
    public delegate void ConnectionFailEventHandler(string host, int port);
    /// <summary>
    /// Receive event handler.
    /// </summary>
    public delegate void ReceiveEventHandler(Packets.Receive.Packet receivedPacket);
    /// <summary>
    /// Disconnect event handler.
    /// </summary>
    public delegate void DisconnectEventHandler(PenguinConnection connection);
    /// <summary>
    /// Error event handler.
    /// </summary>
    public delegate void ErrorEventHandler(PenguinConnection connection, int id);
    /// <summary>
    /// Incorrect API handler.
    /// </summary>
    public delegate void IncorrectAPIHandler(PenguinConnection connection);

    /**
     * The base of the Sharpenguin library, which handles authentication to the servers, determines how packets are processed and handles events.
     */
    public abstract class PenguinConnection {
        // Fields
        /// <summary>
        /// The smart fox server API version.
        /// </summary>
        #if AS2
        public const int APIVersion  = 152;
        #elif AS3
        public const int APIVersion  = 153;
        #endif
        /// <summary>
        /// Whether this instance is authenticated.
        /// </summary>
        protected bool authenticated = false;
        /// <summary>
        /// The username.
        /// </summary>
        protected string username    = "";
        /// <summary>
        /// The password.
        /// </summary>
        protected string password    = "";
        /// <summary>
        /// The random key.
        /// </summary>
        protected string rndk        = "";
        /// <summary>
        /// The temporary buffer where data from currently incomplete packets are stored.
        /// </summary>
        private string buffer        = "";
        /// <summary>
        /// The connection.
        /// </summary>
	    private NetClient.Connection connection;
        /// <summary>
        /// The player's identifier.
        /// </summary>
        protected int id;
        /// <summary>
        /// The xt handler table.
        /// </summary>
        public readonly Packets.Receive.HandlerTable<Packets.Receive.Xt.XtPacket> XtHandlers = new Packets.Receive.HandlerTable<Packets.Receive.Xt.XtPacket>();
        /// <summary>
        /// The xml handler table.
        /// </summary>
        public readonly Packets.Receive.HandlerTable<Packets.Receive.Xml.XmlPacket> XmlHandlers = new Packets.Receive.HandlerTable<Packets.Receive.Xml.XmlPacket>();
        private Configuration.System.Server server;
        /// <summary>
        /// Raised when an error occurs.
        /// </summary>
        public event ErrorEventHandler OnError;
        /// <summary>
        /// Occurs when a packet is received.
        /// </summary>
        public event ReceiveEventHandler OnReceive;
        /// <summary>
        /// Occurs when the client or server disconnects.
        /// </summary>
        public event DisconnectEventHandler OnDisconnect;
        /// <summary>
        /// Occurs when the connection fails.
        /// </summary>
        public event ConnectionFailEventHandler OnConnectFailure;
        /// <summary>
        /// Occurs when a connection has been established.
        /// </summary>
        public event ConnectionSuccessEventHandler OnConnect;
        /// <summary>
        /// Occurs when the smart fox server API version is incompatible with the server.
        /// </summary>
        public event IncorrectAPIHandler OnIncorrectAPI;

        // Properties
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id {
            get { return id; }
        }
        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username {
            get { return username; }
        }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password {
            get { return password; }
            set { password = value; }
        }
        /// <summary>
        /// Gets or sets the internal room id.
        /// </summary>
        /// <value>The internal room.</value>
        public abstract int InternalRoom {
            get;
        }
	
	    /// <summary>
	    /// Gets the underlying connection to the server.
	    /// </summary>
	    /// <value>The connection.</value>
        public NetClient.Connection Connection {
            get { return connection; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value><c>true</c> if this instance is authenticated; otherwise, <c>false</c>.</value>
        public bool IsAuthenticated {
            get { return authenticated; }
        }

        /// <summary>
        /// Gets the configuration of the server the connection is connected to.
        /// </summary>
        /// <value>The server.</value>
        public Configuration.System.Server Server {
            get {
                if(server == null)
                    throw new NotConnectedException("You are not connected to a server yet.");
                else
                    return server;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.PenguinConnection"/> class.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public PenguinConnection(string username, string password) {
            if(username == null) throw new System.ArgumentNullException("username", "Argument cannot be null.");
            if(password == null) throw new System.ArgumentNullException("password", "Argument cannot be null.");
            this.username = username;
            this.password = password;
            Packets.Receive.IDefaultPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket>[] xml = HandlerLoader.GetHandlers<Packets.Receive.IDefaultPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket>>();
            Packets.Receive.IDefaultPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket>[] xt = HandlerLoader.GetHandlers<Packets.Receive.IDefaultPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket>>();
            foreach(Packets.Receive.IDefaultPacketHandler<Sharpenguin.Packets.Receive.Xml.XmlPacket> handler in xml) XmlHandlers.Add(handler);
            foreach(Packets.Receive.IDefaultPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> handler in xt) XtHandlers.Add(handler);
            OnReceive += HandlePacket;
        }

        /// <summary>
        /// Authenticate with the specified server.
        /// </summary>
        /// <param name="server">The server to authenticate with.</param>
        public void Authenticate(Configuration.System.Server server) {
            if(server == null) throw new System.ArgumentNullException("server", "Argument cannot be null.");
            this.server = server;
            CheckCredentials(username, password);
            Connect(server.Host, server.Port);
        }
        
        /// <summary>
        /// Connect the specified host and port.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <param name="port">Port.</param>
        private void Connect(string host, int port) {
            if(host == null) throw new System.ArgumentNullException("host", "Argument cannot be null.");
            if(connection != null && connection.Connected == true) throw new AlreadyConnectedException("This instance is already connected to a server!");
            connection = new NetClient.Connection(new NetClient.ConnectionInfo(host, port, NetClient.ConnectionType.Tcp));
            connection.OnConnect += HandleConnect;
            connection.OnReceive += HandleReceive;
            connection.OnDisconnect += HandleDisconnect;
            connection.Open();
        }

        
        /// <summary>
        /// Handles the connect event.
        /// </summary>
        /// <param name="connection">The established connection.</param>
        private void HandleConnect(NetClient.Connection connection) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(OnConnect != null)
                OnConnect(connection.Information.Host.ToString(), connection.Information.Port);
            Send(new Packets.Send.Xml.VersionCheck(APIVersion));
        }

        /// <summary>
        /// Handles the received data.
        /// </summary>
        /// <param name="connection">The connection data was recieved from.</param>
        /// <param name="data">The received data.</param>
        private void HandleReceive(NetClient.Connection connection, string data) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(data == null) throw new System.ArgumentNullException("data", "Argument cannot be null.");
            buffer += data;
            if(buffer.Contains("\0")) {
                string[] packets = buffer.Split('\0');
                for(int i = 0; i <= packets.Length - 2; i++) {
                    string received = packets[i];
                    #if DEBUG
                    System.Console.WriteLine("RECEIVED: " + received);
                    #endif
                    try {
                        Packets.Receive.Packet packet = null;
                        if(received.IndexOf("%") == 0)
                            packet = new Packets.Receive.Xt.XtPacket(received);
                        else if(received.IndexOf("<") == 0)
                            packet = new Packets.Receive.Xml.XmlPacket(received);
                        else
                            return;
                        if(OnReceive != null)
                            OnReceive(packet);
                    }catch(Packets.Receive.UnhandledPacketException) {
                        // This only really matters in a debugging situation.
                        #if DEBUG
                        Configuration.Configuration.Logger.Debug("Could not handle packet: " + received + ".");
                        #endif
                    }
                }
                buffer = packets[packets.Length - 1];
            }
        }

        /// <summary>
        /// Handles a received packet.
        /// </summary>
        /// <param name="packet">The received packet.</param>
        private void HandlePacket(Packets.Receive.Packet packet) {
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            if(packet is Packets.Receive.Xt.XtPacket) {
                XtHandlers.Handle(this, (Packets.Receive.Xt.XtPacket) packet);
            }else if(packet is Packets.Receive.Xml.XmlPacket) {
                XmlHandlers.Handle(this, (Packets.Receive.Xml.XmlPacket) packet);
            }
        }

        /// <summary>
        /// Handles a socket error.
        /// </summary>
        /// <param name="connection">The connection which the error happened on.</param>
        /// <param name="error">The socket error.</param>
        private void SocketErrorHandler(NetClient.Connection connection, System.Net.Sockets.SocketError error) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(error == System.Net.Sockets.SocketError.HostUnreachable
               || error == System.Net.Sockets.SocketError.ConnectionRefused
               || error == System.Net.Sockets.SocketError.HostDown) {
                OnConnectFailure(connection.Information.Host.ToString(), connection.Information.Port);
            }
        }

        /// <summary>
        /// Checks the credentials before login.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        protected void CheckCredentials(string username, string password) {
            if(string.IsNullOrEmpty(username)) {
                throw new InvalidCredentialsException("No username was given.");
            }else if(username.Length < 4) {
                throw new InvalidCredentialsException("The given username is too short.");
            }else if(username.Length > 12) {
                throw new InvalidCredentialsException("The given username is too long.");
            }else if(string.IsNullOrEmpty(password)) {
                throw new InvalidCredentialsException("No password was given.");
            }else if(password.Length < 4) {
                throw new InvalidCredentialsException("The given password is too short.");
            }else if(password.Length > 100) {
                throw new InvalidCredentialsException("The given password is too long.");
            }
        }

        /// <summary>
        /// Send data to the connected host.
        /// </summary>
        /// <param name="data">The data to send.</param>
        public void Send(string data) {
            if(data == null) throw new System.ArgumentNullException("data", "Argument cannot be null.");
            #if DEBUG
            System.Console.WriteLine("SENT: " + data);
            #endif
            connection.Send(data);
        }

        /// <summary>
        /// Sends a packet to the host.
        /// </summary>
        /// <param name="packet">The packet to send.</param>
        public void Send(Packets.Send.Packet packet) {
            if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
            Send(packet.Data + "\0");
        }

        /// <summary>
        /// Handles a disconnect from the host.
        /// </summary>
        /// <param name="connection">The connection that disconnected.</param>
        private void HandleDisconnect(NetClient.Connection connection) {
            if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
            if(OnDisconnect != null) OnDisconnect(this);
        }

        /// <summary>
        /// Disconnect this instance.
        /// </summary>
        public void Disconnect() {
            connection.Disconnect();
        }

        /// <summary>
        /// Represents an error handler.
        /// </summary>
        public class ErrorHandler : Packets.Receive.IDefaultPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "e"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="connection">The connection the packet is for.</param>
            /// <param name="packet">The packet.</param>
            public void Handle(PenguinConnection connection, Packets.Receive.Xt.XtPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                if(packet.Arguments.Length >= 1) {
                    int id;
                    if(int.TryParse(packet.Arguments[0], out id)) {
                        if(connection.OnError != null) connection.OnError(connection, id);
                    }
                }
            }
        }

        /// <summary>
        /// Represents an incorrect API version handler.
        /// </summary>
        public class ApiKOHandler : Packets.Receive.IDefaultPacketHandler<Packets.Receive.Xml.XmlPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "apiKO"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="connection">The connection the packet is for.</param>
            /// <param name="packet">The packet.</param>
            public void Handle(PenguinConnection connection, Packets.Receive.Xml.XmlPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                connection.OnIncorrectAPI(connection);
                connection.Disconnect();
            }
        }

    }

}
