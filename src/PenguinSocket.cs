/**
 * @file PenguinSocket
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * Sharpenguin Net connections.
 */
namespace Sharpenguin.Net {
    using Sockets = System.Net.Sockets;

    public delegate void PenguinConnectCallback(string connectionAddress, int connectionPort, bool connectionSuccessful);
    public delegate void PenguinReceiveCallback(Data.PenguinPacket objPacket);
    public delegate void PenguinDisconnectCallback();

    /**
     * Socket wrapper for penguin. Incorporates asynchronous reading until \0.
     */
    public class PenguinSocket {
        private Sockets.Socket penguinSocks; //< Socket connection.
        private PenguinReceiveCallback receiveCall; //< Event called when the socket receives data.
        private PenguinDisconnectCallback disconnectCall; //< Event called when the socket disconnects.

        //! Gets whether or not the socket is connected.
        public bool Connected {
            get { return penguinSocks.Connected; }
        }


        /**
         * Creates a new socket and connects to the specified host and port.
         *
         * @param strHost
         *   Host to connect to.
         * @param intPort
         *   Port to connect to.
         */
        public void BeginConnect(string strHost, int intPort, PenguinConnectCallback connectCallback) {
            penguinSocks = new Sockets.Socket(Sockets.AddressFamily.InterNetwork, Sockets.SocketType.Stream, Sockets.ProtocolType.Tcp);
            ConnectState connectionState = new ConnectState(strHost, intPort, connectCallback);
            penguinSocks.BeginConnect(System.Net.IPAddress.Parse(strHost), intPort, ConnectionCallback, connectionState);
        }
        
        /**
         * Handles connection success or failure.
         *
         * @param asyncResult
         *  The IAsyncResult of the connection attempt.
         */
        public void ConnectionCallback(System.IAsyncResult asyncResult) {
            bool connectionSuccess = true;
            ConnectState connectionState = (ConnectState) asyncResult.AsyncState;
            try {
                penguinSocks.EndConnect(asyncResult);
            }catch{
                connectionSuccess = false;
            }
            connectionState.ConnectCallback(connectionState.Host, connectionState.Port, connectionSuccess);
        }

        /**
         * Closes the socket connection.
         */
        public void Disconnect() {
            if(Connected) {
                penguinSocks.Shutdown(Sockets.SocketShutdown.Both);
                penguinSocks.Disconnect(true);
            }
        }

        /**
         * Begins asynchronous receiving.
         *
         * @param receiveDel
         *   The callback to trigger when a packet is received.
         * @param disconnectDel
         *   The callback to trigger when an error occurs.
         */
        public void BeginReceive(PenguinReceiveCallback receiveCallback, PenguinDisconnectCallback disconnectCallback) {
            if(receiveCallback == null) throw new System.ArgumentNullException("receiveCallback");
            if(disconnectCallback == null) throw new System.ArgumentNullException("disconnectCallback");
            receiveCall = receiveCallback;
            disconnectCall = disconnectCallback;
            BufferState receiveState = new BufferState();
            penguinSocks.BeginReceive(receiveState.Buffer, 0, receiveState.BufferSize, 0, ReceiveCallback, receiveState);
        }

        /**
         * Ends asynchronous receiving.
         *
         * @param asyncResult
         *   The IAsyncResult returned from the asynchronous reading.
         */
        private int EndReceive(System.IAsyncResult asyncResult) {
            try {
                return penguinSocks.EndReceive(asyncResult);
            }catch(System.Exception readEx) {
                Out.Logger.WriteOutput("Could not end receive: " + readEx.Message);
                return 0;
            }
        }

        /**
         * Reads data that is available on the socket.
         *
         * @param asyncResult
         *   The IAsyncResult returned from the asynchronous reading.
         */
        private void ReceiveCallback(System.IAsyncResult asyncResult) {
            BufferState receiveState = (BufferState) asyncResult.AsyncState;
            int bytesRead = 0;
            if(penguinSocks.Connected) bytesRead = EndReceive(asyncResult);
            if(bytesRead > 0 && penguinSocks.Connected) {
                receiveState.BufferString += System.Text.Encoding.ASCII.GetString(receiveState.Buffer).Substring(0, bytesRead);
                receiveState.ClearBuffer();
                HandleData(receiveState);
                try {
                    penguinSocks.BeginReceive(receiveState.Buffer, 0, receiveState.BufferSize, 0, ReceiveCallback, receiveState);
                }catch(System.Exception readEx) {
                    Out.Logger.WriteOutput("Could not start read from socket: " + readEx.Message, Out.Logger.LogLevel.Error);
                    disconnectCall();
                }
            }else{
                disconnectCall();
            }
        }

        /**
         * Handles data received from the socket recursively.
         *
         * @param receiveState
         *  The BufferState object containing the received data.
         */
        private void HandleData(BufferState receiveState) {
            int splitPos = receiveState.BufferString.IndexOf("\0");
            if(splitPos != -1) {
                    string strPacket = receiveState.BufferString.Substring(0, splitPos);
                    receiveCall(new Data.PenguinPacket(strPacket));
                    receiveState.BufferString = receiveState.BufferString.Substring(splitPos + 1);
                    HandleData(receiveState);
            }
        }

        /**
         * Begins asynchronous writing.
         *
         * @param strData
         *   The data to send.
         */
        public void BeginWrite(string strData) {
            byte[] arrData = System.Text.Encoding.ASCII.GetBytes(strData + "\0");
            try {
                penguinSocks.BeginSend(arrData, 0, arrData.Length, Sockets.SocketFlags.None, SendCallback, penguinSocks);
            }catch(System.Exception writeEx) {
                Out.Logger.WriteOutput("Could not send to socket: " + writeEx.Message, Out.Logger.LogLevel.Error);
                disconnectCall();
            }
        }

        /**
         * Ends asynchronous sending.
         *
         * @param asyncResult
         *   The IAsyncResult returned from the asynchronous sending.
         */
        public void SendCallback(System.IAsyncResult asyncResult) {
            try {
                if(penguinSocks.Connected) penguinSocks.EndSend(asyncResult);
            }catch(System.Exception sendEx) {
                Out.Logger.WriteOutput("Could not end send to socket: " + sendEx.Message);
            }
        }


    }
    
    /**
     * Buffer state object which stores data received from the socket until it is ready to be handled (i.e. we reach \0)
     */
    public class BufferState {
        private const int intBuffer = 1024; //< Buffer length to attempt to read from the socket.
        private byte[] arrBuffer = new byte[intBuffer]; //< Byte array of data.
        private string strBuffer = ""; //< String representation of the received bytes.

         //! Gets the length to attempt to read from the socket.
        public int BufferSize {
            get { return intBuffer; }
        }
        //! Gets or sets the byte array of data.
        public byte[] Buffer {
            get { return arrBuffer; }
            set { arrBuffer = value; }
        }
        //! Gets or sets the string representation of the byte array of data.
        public string BufferString {
            get { return strBuffer; }
            set { strBuffer = value; }
        }

        /**
         * Clears the byte array to start fresh for the next receive.
         */
        public void ClearBuffer() {
            Buffer = new byte[BufferSize];
        }
    }
    
    /**
     * Connect state object which contains the connect callback and host and port we are connecting to.
     */
    public class ConnectState {
        private string connectHost; //< Host we are connecting to.
        private int connectPort; //< Port we are connecting to.
        private PenguinConnectCallback connectCallback; //< Delegate called when connection had succeeded or failed.
        
        //! Gets the host we are connecting to.
        public string Host {
            get { return connectHost; }
        }
        //! Gets the port we are connecting to.
        public int Port {
            get { return connectPort; }
        }
        //! Gets the connect callback.
        public PenguinConnectCallback ConnectCallback {
            get { return connectCallback; }
        }
        
        public ConnectState(string host, int port, PenguinConnectCallback callback) {
            connectHost = host;
            connectPort = port;
            connectCallback = callback;
        }
    
    }
}
