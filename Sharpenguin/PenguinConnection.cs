/**
 * @file PenguinBase
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */
#define DEBUG

namespace Sharpenguin {

    public delegate void ConnectionSuccessEventHandler(string host, int port);
    public delegate void ConnectionFailEventHandler(string host, int port);
    public delegate void ReceiveEventHandler(Packets.Receive.Packet receivedPacket);
    public delegate void DisconnectEventHandler(PenguinConnection connection);
    public delegate void ErrorEventHandler(PenguinConnection connection, int id);
    public delegate void IncorrectAPIHandler(PenguinConnection connection);

    /**
     * The base of the Sharpenguin library, which handles authentication to the servers, determines how packets are processed and handles events.
     */
    public abstract class PenguinConnection {
        // Fields
        /// <summary>
        /// The smart fox server API version.
        /// </summary>
        public const int APIVersion  = 152;
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
        private string serverName    = ""; //< The name of the server we are connecting, or have connected, to.
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
        public virtual int InternalRoom {
            get { return -1; } // The system room, -1.
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
        /// Gets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName {
            get { return serverName; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.PenguinConnection"/> class.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public PenguinConnection(string username, string password) {
            if(username == null) throw new System.ArgumentNullException("username");
            if(password == null) throw new System.ArgumentNullException("password");
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
            CheckCredentials(username, password);
            Connect(server.Host, server.Port);
        }
        
        /// <summary>
        /// Connect the specified host and port.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <param name="port">Port.</param>
        private void Connect(string host, int port) {
            if(host == null) throw new System.ArgumentNullException("host");
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
            if(connection == null) throw new System.ArgumentNullException("connection");
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
            if(connection == null) throw new System.ArgumentNullException("connection");
            if(data == null) throw new System.ArgumentNullException("data");
            buffer += data;
            if(buffer.Contains("\0")) {
                string[] packets = buffer.Split('\0');
                buffer = packets[packets.Length - 1];
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
                        // TODO
                    }
                }
            }
        }

        /// <summary>
        /// Handles a received packet.
        /// </summary>
        /// <param name="packet">The received packet.</param>
        private void HandlePacket(Packets.Receive.Packet packet) {
            if(packet == null) throw new System.ArgumentNullException("packet");
            if(packet is Packets.Receive.Xt.XtPacket) {
                XtHandlers.Handle(this, (Packets.Receive.Xt.XtPacket) packet);
            } else if(packet is Packets.Receive.Xml.XmlPacket) {
                XmlHandlers.Handle(this, (Packets.Receive.Xml.XmlPacket) packet);
            }
        }

        /// <summary>
        /// Handles a socket error.
        /// </summary>
        /// <param name="connection">The connection which the error happened on.</param>
        /// <param name="error">The socket error.</param>
        private void SocketErrorHandler(NetClient.Connection connection, System.Net.Sockets.SocketError error) {
            if(connection == null) throw new System.ArgumentNullException("connection");
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
            if(data == null) throw new System.ArgumentNullException("data");
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
            if(packet == null) throw new System.ArgumentNullException("packet");
            Send(packet.Data + "\0");
        }

        /// <summary>
        /// Handles a disconnect from the host.
        /// </summary>
        /// <param name="connection">The connection that disconnected.</param>
        private void HandleDisconnect(NetClient.Connection connection) {
            if(connection == null) throw new System.ArgumentNullException("connection");
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
            /// <param name="sender">The sender of the packet.</param>
            /// <param name="packet">The packet.</param>
            /// <param name="connection">Connection.</param>
            public void Handle(PenguinConnection connection, Packets.Receive.Xt.XtPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection");
                if(packet == null) throw new System.ArgumentNullException("packet");
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
            /// <param name="receiver">The connection that received the packet.</param>
            /// <param name="packet">The packet.</param>
            /// <param name="connection">Connection.</param>
            public void Handle(PenguinConnection connection, Packets.Receive.Xml.XmlPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection");
                if(packet == null) throw new System.ArgumentNullException("packet");
                connection.OnIncorrectAPI(connection);
                connection.Disconnect();
            }
        }

    }

}
