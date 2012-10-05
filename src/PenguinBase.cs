/**
 * @file PenguinBase
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin {
    public delegate void LoginHandler();
    public delegate void JoinHandler();
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
        protected Data.PenguinRoom currentRoom; //< A room object of the room you are currently in.
        private Xt.HandlerTable penguinHandlers; //< Handler table.
        protected int extRoomID; //< External room id.
        protected int intPlayerID; //< Player id.
        protected int intRoomID         = -1; //< Internal room id.
        protected bool blnIsLogin       = false; //< If we are contacting the login servers, this is true.
        protected bool blnIsJoin        = false; //< If we are authenticating with the game servers, this is true.
        protected string strUsername    = ""; //< Your username.
        protected string strPassword    = ""; //< Your password.
        protected string strLoginKey    = ""; //< Login key from the login server.
        protected string strRndK        = ""; //< Random key from login or game.
        private Data.CPCrumbs penguinCrumbs; // Crumbs object.
        private const int intAPIVersion = 152; // Smart fox server API version.
        private Net.PenguinSocket psSock; //< Penguin socket wrapper.
        protected ErrorHandler penguinErrorEvent; //< Error event.
        private event LoginHandler loginSuccess; //< Event for handling login success.
        private event JoinHandler joinSuccess; //< Event for handling join success.
        private event PacketHandler packetReceived; //< Event for handling a packet being received.
        private event DisconnectHandler disconnectEvent; //< Event for handling socket disconnection.
        private event ConnectionFailHandler connectFail; //< Event for handling connection failure.

        // Properties
        public int ID {
            get { return intPlayerID; }
        }
        public int intRoom {
            get { return intRoomID; }
        }
        public int extRoom {
            get { return extRoomID; }
        }
        public string Username {
            get { return strUsername; }
        }
        public LoginHandler onLogin {
            set { loginSuccess = value; }
        }
        public JoinHandler onJoin {
            set { joinSuccess = value; }
        }
        public PacketHandler onReceive {
            set { packetReceived = value; }
        }
        public DisconnectHandler onDisconnect {
            set { disconnectEvent = value; }
        }
        public ConnectionFailHandler onConnectFail {
            set { connectFail = value; }
        }
        public Data.PenguinRoom Room {
            get { return currentRoom; }
        }
        public Data.CPCrumbs Crumbs {
            get { return penguinCrumbs; }
        }
        public Net.PenguinSocket Sock {
            get { return psSock; }
        }
        public Xt.HandlerTable Handler {
            get { return penguinHandlers; }
        }
        public ErrorHandler onError {
            set { penguinErrorEvent = value; }
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
         * Creates a socket and connects to a host.
         *
         * @param strIp
         *   The IP address of the remote host.
         * @param intPort
         *   The port to connect to.
         *
         * @return
         *   TRUE on success, FALSE on failure.
         */
        private bool InitialiseConnection(string strIp, int intPort) {
            psSock = new Net.PenguinSocket();
            return psSock.Connect(strIp, intPort);
        }

        /**
         * Connects to login server and begins authentication.
         *
         * @param strUsername
         *   The username to login as.
         * @param strPassword
         *   The password of the user.
         */
        public void Login(string strUsername, string strPassword) {
            if(CheckCredentials(strUsername, strPassword) == false) return;
            int intLoginServer = Crumbs.LoginServers.GetRandom();
            string strIp = Crumbs.LoginServers.GetAttributeById(intLoginServer, "ip");
            if(InitialiseConnection(strIp, LoginPort(strUsername, intLoginServer))) {
                blnIsLogin = true;
                StartAuth(strUsername, strPassword);
           }else{
                if(connectFail != null) connectFail(strIp, LoginPort(strUsername, intLoginServer));
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
            int intError = 0;
            if(string.IsNullOrEmpty(strUsername)) {
                intError = 140;
            }else if(strUsername.Length < 4) {
                intError = 141;
            }else if(strUsername.Length > 12) {
                intError = 142;
            }else if(string.IsNullOrEmpty(strPassword)) {
                intError = 130;
            }else if(strPassword.Length < 4) {
                intError = 131;
            }else if(strPassword.Length > 32) {
                intError = 132;
            }
            if(intError == 0) return true;
            if(penguinErrorEvent != null) penguinErrorEvent(intError);
            return false;
        }

        /**
         * Joins a game server.
         *
         * @param objServer
         *   The id or name of the server you wish to connect to.
         */
        public void Join(object objServer) {
            if(strLoginKey == "") throw new Exceptions.EarlyJoinException("You must login before you can join a game server!");
            blnIsJoin = true;
            string strIp = (objServer is int) ? Crumbs.Servers.GetById((int) objServer)["ip"] : Crumbs.Servers.GetByAttribute("name", objServer as string)["ip"];
            int intPort = System.Convert.ToInt32((objServer is int) ? Crumbs.Servers.GetById((int) objServer)["port"] : Crumbs.Servers.GetByAttribute("name", objServer as string)["port"]);
            if(InitialiseConnection(strIp, intPort)) {
                StartAuth(strUsername, strLoginKey);
            }else{
                if(connectFail != null) connectFail(strIp, intPort);
            }
        }

        /**
         * Starts authentication to the connected server.
         *
         * @param strUser
         *   The username to authenticate as.
         * @param strPass
         *   The password, or login key, to authenticate with.
         */
        private void StartAuth(string strUser, string strPass) {
            strUsername = strUser;
            strPassword = strPass;
            sendAPIVersion();
            psSock.BeginReceive(ReceiveCallback, DisconnectCallback);
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
        private int LoginPort(string strUsername, int intLoginServer) {
            return System.Convert.ToInt32(Crumbs.LoginServers.GetAttributeById(intLoginServer, (Utils.ord(strUsername.ToUpper()) % 2 == 1) ? "odd" : "even"));
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
                    strHash = (blnIsLogin) ? Security.Crypt.hashPassword(strPassword, strRndK) : Security.Crypt.subMd5(strLoginKey + strRndK, true) + strLoginKey;
                    SendLogin(strUsername, strHash);
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
            if(loginSuccess != null) loginSuccess();
        }

        /**
         * Calls the onJoin event.
         */
        protected void JoinFinished() {
            blnIsJoin = false;
            if(joinSuccess != null) joinSuccess();
        }

        /**
         * Sends data to the connected host.
         *
         * @param strData
         *   The data to send to the host.
         */
        public void SendData(string strData) {
            psSock.BeginWrite(strData);
        }

        /**
         * Receive callback for the asynchronous socket.
         *
         * @param receivedPacket
         *   The packet that was received.
         */
        protected void ReceiveCallback(Data.PenguinPacket receivedPacket) {
            if(packetReceived != null) packetReceived(receivedPacket);
            if(receivedPacket.Type == Data.PenguinPacket.PacketType.Xt) {
                HandleXt(receivedPacket);
            }else if(receivedPacket.Type == Data.PenguinPacket.PacketType.Xml) {
                XmlReceived(receivedPacket);
            }
        }

        /**
         * Disconnect callback for the asynchronous socket.
         *
         * @param intError
         *   The socket error number.
         */
        protected void DisconnectCallback() {
            if(disconnectEvent != null) disconnectEvent();
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
         */
        private void SendLogin(string strUsername, string strHash) {
            SendData("<msg t='sys'><body action='login' r='0'><login z='w1'><nick><![CDATA[" + strUsername + "]]></nick><pword><![CDATA[" + strHash + "]]></pword></login></body></msg>");
        }

        /**
         * Disconnects the socket.
         */
        public void Disconnect() {
            if(psSock.Connected) {
                psSock.Disconnect();
            }
        }

    }

}
