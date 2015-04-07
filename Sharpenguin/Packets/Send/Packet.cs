namespace Sharpenguin.Packets.Send {
    public abstract class Packet {
        private string data;
        private int length;

        public string Data {
            get { return data; }
        }

        public int Length {
            get { return length; }
        }

        public Packet(string packet) {
            data = packet;
            length = data.Length;
        }
    }
}
