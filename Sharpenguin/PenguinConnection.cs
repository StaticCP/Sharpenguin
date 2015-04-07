/**
 * @file PenguinBase
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin {

    public delegate void ConnectionSuccessEventHandler(string host, int port);
    public delegate void ConnectionFailEventHandler(string host, int port);
    public delegate void ReceiveEventHandler(Packets.Receive.Packet receivedPacket);
    public delegate void DisconnectEventHandler();
    public delegate void ErrorEventHandler(int id);
    public delegate void IncorrectAPIHandler();

    /**
     * The base of the Sharpenguin library, which handles authentication to the servers, determines how packets are processed and handles events.
     */
    public abstract class PenguinConnection {
        // Fields
        public const int APIVersion  = 152; // Smart fox server API version.
        protected bool authenticated = false; //< If we are authenticated with a game server, this is true.
        protected string username    = ""; //< Your username.
        protected string password    = ""; //< Your password.
        protected string rndk        = ""; //< Random key from login or game.
        private string buffer        = "";
        private string serverName    = ""; //< The name of the server we are connecting, or have connected, to.
        private Configuration.Configuration configuration;
	    private NetClient.Connection connection; //< Penguin socket wrapper.
        private Room room;
        protected int id;
        public readonly Packets.Receive.HandlerTable<Packets.Receive.Xt.XtPacket> XtHandlers = new Packets.Receive.HandlerTable<Packets.Receive.Xt.XtPacket>();
        public readonly Packets.Receive.HandlerTable<Packets.Receive.Xml.XmlPacket> XmlHandlers = new Packets.Receive.HandlerTable<Packets.Receive.Xml.XmlPacket>();
        public event ErrorEventHandler OnError; //< Error event.
        public event ReceiveEventHandler OnReceive; //< Event for handling a packet being received.
        public event DisconnectEventHandler OnDisconnect; //< Event for handling socket disconnection.
        public event ConnectionFailEventHandler OnConnectFailure; //< Event for handling connection failure.
        public event ConnectionSuccessEventHandler OnConnect; //< Event for handling connection success;
        public event IncorrectAPIHandler OnIncorrectAPI;

        // Properties
        //! Gets your username.
        public int Id {
            get { return id; }
        }
        public string Username {
            get { return username; }
        }
        public string Password {
            get { return password; }
            set { password = value; }
        }

        public Room Room {
            get { return room; }
        }
	
	    /// <summary>
	    /// Gets the underlying connection to the server.
	    /// </summary>
	    /// <value>The connection.</value>
        public NetClient.Connection Connection {
            get { return connection; }
        }
        //! Gets whether we have authenticated or not.
        public bool IsAuthenticated {
            get { return authenticated; }
        }
        //! Gets the name of the server we are connecting, or have connected, to.
        public string ServerName {
            get { return serverName; }
        }

        public Sharpenguin.Configuration.Configuration Configuration {
            get { return configuration; }
        }

        /**
         * PenguinConnection constuctor. Creates the crumbs object and handler table.
         */
        public PenguinConnection(string username, string password) {
            if(username == null)
                throw new System.ArgumentNullException("username", "Argument cannot be null.");
            if(password == null)
                throw new System.ArgumentNullException("password", "Argument cannot be null.");
            this.username = username;
            this.password = password;
            OnReceive += HandlePacket;
            room = new Room {
                Id = -1,
                External = 0,
                Name = "System"
            };
        }

        /// <summary>
        /// Authenticate with the specified server.
        /// </summary>
        /// <param name="server">The server to authenticate with.</param>
        public void Authenticate(Configuration.System.Server server) {
            CheckCredentials(username, password);
            Connect(server.Host, server.Port);
        }
        

        /**
         * Starts a new connection.
         *
         * @param connectionHost
         *   The address of the host we are connecting to.
         * @param connectionPort
         *   The port we are connecting to.
         */
        private void Connect(string host, int port) {
            if(host == null)
                throw new System.ArgumentNullException("host", "Argument cannot be null.");
            if(connection != null && connection.Connected == true)
                throw new AlreadyConnectedException("This instance is already connected to a server!");
            connection = new NetClient.Connection(new NetClient.ConnectionInfo(host, port, NetClient.ConnectionType.Tcp));
            connection.OnConnect += HandleConnect;
            connection.OnReceive += HandleReceive;
            connection.OnDisconnect += HandleDisconnect;
            connection.Open();
        }

        
        /**
         * Connection attempt callback.
         *
         * @param hostAddress
         *  The address of the host we attempted to connect to.
         * @param hostPort
         *  The port we attempted to connect to.
         * @param connectionSuccess
         *  Whether the connection was successful or not.
         */
        private void HandleConnect(NetClient.Connection connection) {
            if(OnConnect != null)
                OnConnect(connection.Information.Host.ToString(), connection.Information.Port);
            Send(new Packets.Send.Xml.VersionCheck(APIVersion));
        }

        public void HandleReceive(NetClient.Connection connection, string data) {
            buffer += data;
            if(buffer.Contains("\0")) {
                string[] packets = buffer.Split('\0');
                foreach(string received in packets) {
                    Packets.Receive.Packet packet = null;
                    if(received.IndexOf("%") == 0)
                        packet = new Packets.Receive.Xt.XtPacket(received);
                    else if(received.IndexOf("<") == 0)
                        packet = new Packets.Receive.Xml.XmlPacket(received);
                    else
                        return;
                    if(OnReceive != null)
                        OnReceive(packet);
                }
            }
        }

        private void HandlePacket(Packets.Receive.Packet packet) {
            if(packet is Packets.Receive.Xt.XtPacket) {
                XtHandlers.Handle(this, (Packets.Receive.Xt.XtPacket) packet);
            } else if(packet is Packets.Receive.Xml.XmlPacket) {
                XmlHandlers.Handle(this, (Packets.Receive.Xml.XmlPacket) packet);
            }
        }


        private void SocketErrorHandler(NetClient.Connection connection, System.Net.Sockets.SocketError error) {
            if(error == System.Net.Sockets.SocketError.HostUnreachable
               || error == System.Net.Sockets.SocketError.ConnectionRefused
               || error == System.Net.Sockets.SocketError.HostDown) {
                OnConnectFailure(connection.Information.Host.ToString(), connection.Information.Port);
            }
        }

        /**
         * Checks username and password are correct lengths before login.
         *
         * @param username
         *   The username to check.
         * @param password
         *   The password to check.
         */
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
            }else if(password.Length > 32) {
                throw new InvalidCredentialsException("The given password is too long.");
            }
        }

        /**
         * Sends data to the connected host.
         *
         * @param dataText
         *   The data to send to the host.
         */
        public void Send(string data) {
            connection.Send(data);
        }

        public void Send(Packets.Send.Packet packet) {
            Send(packet.Data);
        }

        /**
         * Disconnect callback for the asynchronous socket.
         */
        private void HandleDisconnect(NetClient.Connection connection) {
            if(OnDisconnect != null) OnDisconnect();
        }

        /**
         * Disconnects the socket.
         */
        public void Disconnect() {
            connection.Close();
        }

        /// <summary>
        /// Represents an error handler.
        /// </summary>
        public class ErrorHandler : Packets.Receive.IPacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
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
                if(packet.Arguments.Length >= 1) {
                    int id;
                    if(int.TryParse(packet.Arguments[0], out id)) {
                        connection.OnError(id);
                    }
                }
            }
        }

        public class ApiKOHandler : Packets.Receive.IPacketHandler<Packets.Receive.Xml.XmlPacket> {
            public string Handles {
                get { return "apiKO"; }
            }

            public void Handle(PenguinConnection connection, Packets.Receive.Xml.XmlPacket packet) {
                connection.OnIncorrectAPI();
                connection.Disconnect();
            }
        }

    }

}
