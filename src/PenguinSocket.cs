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

    public delegate void PenguinReceiveCallback(Data.PenguinPacket objPacket);
    public delegate void PenguinDisconnectCallback();

    /**
     * Socket wrapper for penguin. Incorporates asynchronous reading until \0.
     */
    public class PenguinSocket {
        private Sockets.Socket penguinSocks; //< Socket connection.
        private PenguinReceiveCallback receiveCall;
        private PenguinDisconnectCallback disconnectCall;

        public bool Connected {
            get { return penguinSocks.Connected; }
        }

        public class BufferState {
            private const int intBuffer = 1024;
            private byte[] arrBuffer = new byte[intBuffer];
            private string strBuffer = "";

            public int BufferSize {
                get { return intBuffer; }
            }
            public byte[] Buffer {
                get { return arrBuffer; }
                set { arrBuffer = value; }
            }
            public string BufferString {
                get { return strBuffer; }
                set { strBuffer = value; }
            }
            public void ClearBuffer() {
                Buffer = new byte[BufferSize];
            }
        }

        /**
         * Creates a new socket and connects to the specified host and port.
         *
         * @param strHost
         *   Host to connect to.
         * @param intPort
         *   Port to connect to.
         */
        public bool Connect(string strHost, int intPort) {
            try {
                penguinSocks = new Sockets.Socket(Sockets.AddressFamily.InterNetwork, Sockets.SocketType.Stream, Sockets.ProtocolType.Tcp);
                penguinSocks.Connect(System.Net.IPAddress.Parse(strHost), intPort);
                penguinSocks.Blocking = true;
                return true;
            }catch{
                return false;
            }
        }

        /**
         * Closes the socket connection.
         */
        public void Disconnect() {
            penguinSocks.Shutdown(Sockets.SocketShutdown.Both);
            penguinSocks.Disconnect(true);
            disconnectCall();
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
            penguinSocks.BeginReceive(receiveState.Buffer, 0, receiveState.BufferSize, 0, new System.AsyncCallback(ReceiveCallback), receiveState);
        }

        private int EndReceive(System.IAsyncResult asyncResult) {
            try {
                return penguinSocks.EndReceive(asyncResult);
            }catch(System.Exception readEx) {
                Out.Logger.WriteOutput("Could not end receive: " + readEx.Message);
                return 0;
            }
        }

        private void ReceiveCallback(System.IAsyncResult asyncResult) {
            BufferState receiveState = (BufferState) asyncResult.AsyncState;
            int bytesRead = 0;
            if(penguinSocks.Connected) bytesRead = EndReceive(asyncResult);
            if(bytesRead > 0 && penguinSocks.Connected) {
                receiveState.BufferString += System.Text.Encoding.ASCII.GetString(receiveState.Buffer).Substring(0, 1024);
                receiveState.ClearBuffer();
                HandleData(receiveState);
                try {
                    penguinSocks.BeginReceive(receiveState.Buffer, 0, receiveState.BufferSize, 0, new System.AsyncCallback(ReceiveCallback), receiveState);
                }catch(System.Exception readEx) {
                    Out.Logger.WriteOutput("Could not start read from socket: " + readEx.Message, Out.Logger.LogLevel.Error);
                    disconnectCall();
                }
            }else{
                disconnectCall();
            }
        }

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
                penguinSocks.BeginSend(arrData, 0, arrData.Length, Sockets.SocketFlags.None, new System.AsyncCallback(SendCallback), penguinSocks);
            }catch(System.Exception writeEx) {
                Out.Logger.WriteOutput("Could not send to socket: " + writeEx.Message, Out.Logger.LogLevel.Error);
                disconnectCall();
            }
        }

        public void SendCallback(System.IAsyncResult asyncResult) {
            try {
                if(penguinSocks.Connected) penguinSocks.EndSend(asyncResult);
            }catch(System.Exception sendEx) {
                Out.Logger.WriteOutput("Could not end send to socket: " + sendEx.Message);
            }
        }


    }
}
