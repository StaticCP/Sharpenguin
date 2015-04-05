/**
 * @file PenguinBase
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin {
    using Threads = System.Threading;

    public delegate void LoginHandler();
    public delegate void JoinHandler();
    public delegate void ConnectionSuccessHandler(string strIp, int intPort);
    public delegate void ConnectionFailHandler(string strIp, int intPort);
    public delegate void XtHandler(Xt.XtParser xpParser);
    public delegate void PacketHandler(Data.PenguinPacket receivedPacket);
    public delegate void DisconnectHandler();
    public delegate void ErrorHandler(int intId);

    /**
     * The base of the Sharpenguin library, which handles authentication to the servers, determines how packets are processed and handles events.
     */
    public abstract class PenguinBase {
        // Feilds
        protected int playerID; //< Player id.
        private const int intAPIVersion = 152; // Smart fox server API version.
        protected bool blnIsLogin       = false; //< If we are contacting the login servers, this is true.
        protected bool blnIsJoin        = false; //< If we are authenticating with the game servers, this is true.
        protected bool blnAuthenticated = false; //< If we are authenticated with a game server, this is true.
        protected string penguinName    = ""; //< Your username.
        protected string penguinPass    = ""; //< Your password.
        protected string loginKey    = ""; //< Login key from the login server.
        protected string strRndK        = ""; //< Random key from login or game.
        private string serverName       = ""; //< The name of the server we are connecting, or have connected, to.
        private Data.CPCrumbs penguinCrumbs; // Crumbs object.
        protected Data.PenguinRoom currentRoom; //< A room object of the room you are currently in.
	private NetClient.Connection connection; //< Penguin socket wrapper.
        private Xt.HandlerTable penguinHandlers; //< Handler table.
        public event ErrorHandler OnError; //< Error event.
        public event LoginHandler OnLogin; //< Event for handling login success.
        public event JoinHandler OnJoin; //< Event for handling join success.
        public event PacketHandler OnReceive; //< Event for handling a packet being received.
        public event DisconnectHandler OnDisconnect; //< Event for handling socket disconnection.
        public event ConnectionFailHandler OnConnectFailure; //< Event for handling connection failure.
        public event ConnectionSuccessHandler OnConnect; //< Event for handling connection success;

        // Properties
        //! Get's your ID.
        public int ID {
            get { return playerID; }
        }
        //! Gets your username.
        public string Username {
            get { return penguinName; }
        }
        //! Gets the current room object.
        public Data.PenguinRoom Room {
            get { return currentRoom; }
        }
        //! Gets the crumbs loaded from the crumb XML files.
        public Data.CPCrumbs Crumbs {
            get { return penguinCrumbs; }
        }
	
	/// <summary>
	/// Gets the underlying connection to the server.
	/// </summary>
	/// <value>The connection.</value>
        public Net.PenguinSocket Connection {
            get { return connection; }
        }
        //! Gets the handler table.
        public Xt.HandlerTable Handler {
            get { return penguinHandlers; }
        }
        //! Gets whether we have authenticated or not.
        public bool IsAuthenticated {
            get { return blnAuthenticated; }
        }
        //! Gets the name of the server we are connecting, or have connected, to.
        public string ServerName {
            get { return serverName; }
        }

        /**
         * PenguinBase constuctor. Creates the crumbs object and handler table.
         */
        public PenguinBase() {
            penguinCrumbs = new Data.CPCrumbs();
            penguinHandlers = new Xt.HandlerTable();
            currentRoom = new Data.PenguinRoom();
        }
        

        /**
         * Starts a new connection.
         *
         * @param connectionHost
         *   The address of the host we are connecting to.
         * @param connectionPort
         *   The port we are connecting to.
         */
        private void InitialiseConnection(string host, int port) {
            connection = new NetClient.Connection(new NetClient.ConnectionInfo(host, port, NetClient.ConnectionType.Tcp));
            connection.OnConnect += HandleConnect;
            connection.OnReceive += HandleReceive;
            connection.OnDisconnect += HandleDiconnect;
            connection.Open();
        }

        /**
         * Connects to login server and begins authentication.
         *
         * @param penguinUsername
         *   The username to login as.
         * @param penguinPassword
         *   The password of the user.
         */
        public void Login(string penguinUsername, string penguinPassword) {
            if(CheckCredentials(penguinUsername, penguinPassword) == false) return;
            penguinName = penguinUsername;
            penguinPass = penguinPassword;
            blnAuthenticated = false;
            int loginServer = Crumbs.LoginServers.GetRandom();
            string strIp = Crumbs.LoginServers.GetAttributeById(loginServer, "ip");
            blnIsLogin = true;
            InitialiseConnection(strIp, LoginPort(penguinUsername, loginServer));
        }
        
        /**
         * Joins a game server via id.
         *
         * @param serverId
         *   The ID of the server to connect to.
         */
        public void Join(int serverId) {
            if(loginKey == "") throw new Exceptions.EarlyJoinException("You must login before you can join a game server!");
            System.Collections.Generic.Dictionary<string, string> serverCrumbs = Crumbs.Servers.GetById(serverId);
            serverName = serverCrumbs["name"];
            blnIsJoin = true;
            InitialiseConnection(serverCrumbs["ip"], int.Parse(serverCrumbs["port"]));
        }
        
        /**
         * Joins a game server via name.
         *
         * @param serverName
         *   The name of the server to connect to.
         */
        public void Join(string serverName) {
            Join(Crumbs.Servers.GetIdByAttribute("name", serverName));
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
            if(OnConnectionSuccess != null)
                OnConnectionSuccess(connection.Information.Host, connection.Information.Port, true);
        }

        public void HandleReceive (NetClient.Connection connection, string packet) {
            if(OnReceive != null) OnReceive(packet);
            if(receivedPacket.Type == Data.PenguinPacket.PacketType.Xt) {
                HandleXt(receivedPacket);
            }else if(receivedPacket.Type == Data.PenguinPacket.PacketType.Xml) {
                XmlReceived(receivedPacket);
            }
        }


        private void SocketErrorHandler(NetClient.Connection connection, System.Net.Sockets.SocketError error) {
            if(error == System.Net.Sockets.SocketError.HostUnreachable
               || error == System.Net.Sockets.SocketError.ConnectionRefused
               || error == System.Net.Sockets.SocketError.HostDown) {

            }
        }

        /**
         * Checks username and password are correct lengths before login.
         *
         * @param strUsername
         *   The username to check.
         * @param strPassword
         *   The password to check.
         */
        private bool CheckCredentials(string strUsername, string strPassword) {
            int error = 0;
            if(string.IsNullOrEmpty(strUsername)) {
                error = 140;
            }else if(strUsername.Length < 4) {
                error = 141;
            }else if(strUsername.Length > 12) {
                error = 142;
            }else if(string.IsNullOrEmpty(strPassword)) {
                error = 130;
            }else if(strPassword.Length < 4) {
                error = 131;
            }else if(strPassword.Length > 32) {
                error = 132;
            }
            if(error == 0) return true;
            if(OnError != null) OnError(error);
            return false;
        }

        /**
         * Starts authentication to the connected server.
         *
         * @param penguinUsername
         *   The username to authenticate as.
         * @param penguinPassword
         *   The password, or login key, to authenticate with.
         */
        private void StartAuth(string penguinUsername, string penguinPassword) {
            penguinName = penguinUsername;
            penguinPass = penguinPassword;
            sendAPIVersion();
        }

        /**
         * Determines what login port to use by the first letter of the username.
         *
         * @param strUsername
         *   The seed username.
         *
         * @return
         *   The port to login with.
         */
        private int LoginPort(string penguinUsername, int loginServer) {
            return int.Parse(Crumbs.LoginServers.GetAttributeById(loginServer, (Utils.ord(penguinUsername.ToUpper()) % 2 == 1) ? "odd" : "even"));
        }

        /**
         * Handles login XML.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleLoginXml(Data.PenguinPacket receivedPacket) {
            switch(receivedPacket.Xml.Name) {
                case "msg":
                    HandleXmlMsg(receivedPacket);
                break;
            }
        }

        /**
         * Handles Xml with the tag name "msg".
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleXmlMsg(Data.PenguinPacket receivedPacket) {
            switch(receivedPacket.Xml.ChildNodes[0].Attributes["action"].Value) {
                case "apiOK":
                    SendRndKRequest();
                break;
                case "apiKO":
                    throw new Exceptions.InvalidAPIException("Invalid API version."); // Only happens if the SFS API version is wrong. I've never known this to happen.
                case "rndK":
                    string strHash;
                    strRndK = receivedPacket.Xml.ChildNodes[0].InnerText;
                    strHash = (blnIsLogin) ? Security.Crypt.HashPassword(penguinPass, strRndK) : Security.Crypt.RevMd5(loginKey + strRndK, true) + loginKey;
                    SendLogin(penguinName, strHash);
                break;
            }
        }

        /**
         * Handle xt packets.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleXt(Data.PenguinPacket receivedPacket) {
            penguinHandlers.Execute(receivedPacket.Xt.Command, receivedPacket);
        }

        /**
         * Disconnects from the login server and calls the onLogin event.
         */
        protected void LoginFinished() {
            blnIsLogin = false;
            Disconnect();
            if(OnLogin != null) OnLogin();
        }

        /**
         * Calls the onJoin event.
         */
        protected void JoinFinished() {
            blnIsJoin = false;
            blnAuthenticated = true;
            if(OnJoin != null) OnJoin();
        }

        /**
         * Sends data to the connected host.
         *
         * @param dataText
         *   The data to send to the host.
         */
        public void Send(string data) {
            connection.Send(data)
        }

        /**
         * Disconnect callback for the asynchronous socket.
         */
        protected void HandleDisconnect() {
            if(OnDisconnect != null) OnDisconnect();
        }

        /**
         * Handles Xml packets.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void XmlReceived(Data.PenguinPacket receivedPacket) {
            if(blnIsLogin || blnIsJoin) { // If it's not from the login, say "wtf."
                HandleLoginXml(receivedPacket);
            }
        }

        /**
         * Sends SFS API version to the server.
         */
        private void sendAPIVersion() {
            SendData("<msg t='sys'><body action='verChk' r='0'><ver v='" + intAPIVersion.ToString() + "' /></body></msg>");
        }

        /**
         * Sends a random key request to the server.
         */
        private void SendRndKRequest() {
            SendData("<msg t='sys'><body action='rndK' r='-1'></body></msg>");
        }

        /**
         * Sends a login request to the server.
         *
         * @param penguinUsername
         *   The username of the penguin to login.
         *
         * @param authHash
         *   The hash to authenticate to the server with.
         */
        private void SendLogin(string penguinUsername, string authHash) {
            SendData("<msg t='sys'><body action='login' r='0'><login z='w1'><nick><![CDATA[" + penguinUsername + "]]></nick><pword><![CDATA[" + authHash + "]]></pword></login></body></msg>");
        }

        /**
         * Disconnects the socket.
         */
        public void Disconnect() {
            psSock.Disconnect();
        }

    }

}
