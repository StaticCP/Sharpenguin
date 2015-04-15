namespace Sharpenguin.Packets.Send.Xt {
    public abstract class XtPacket : Send.Packet {
        public XtPacket(PenguinConnection sender, string handler) : base("%xt%s%" + handler + "%" + sender.InternalRoom.ToString() + "%") {}
        public XtPacket(PenguinConnection sender, string handler, string[] args) : base("%xt%s%" + handler + "%" + sender.InternalRoom.ToString() + "%" + string.Join("%", args) + "%") {}
    }
}
