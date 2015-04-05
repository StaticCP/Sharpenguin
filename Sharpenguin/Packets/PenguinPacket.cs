/**
 * @file PenguinPacket
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Data {

    /**
     * Stores and parses information about packets.
     */
    public class PenguinPacket {
        private PacketType ptType; //< Packet type.
        private Xt.XtParser xpParser; //< Parsed Xt (If packet is an Xt Packet)
        private int intLength; //< Length of packet.
        private string strData; //< String of data in the packet.
        private System.Xml.XmlElement objXml; //< Parsed Xml (If Packet is an Xml Packet)

        //! Gets the type of packet (Xt, Xml, Unknown)
        public PacketType Type {
            get { return ptType; }
        }
        //! Gets the string of data received in the packet.
        public string Data {
            get { return strData; }
        }
        //! Gets the parsed Xt string object (if the packet is Xt).
        public Xt.XtParser Xt {
            get { return xpParser; }
        }
        //! Gets the parsed Xml string object (if the packet is Xml).
        public System.Xml.XmlElement Xml {
            get { return objXml; }
        }
        //! Gets the length of the packet.
        public int Length {
            get { return intLength; }
        }

        /**
         * Enumeration of packet types.
         */
        public enum PacketType {
            Xml,
            Xt,
            Unknown
        }

        /**
         * PenguinPacket constructor. Stores and parses the packet.
         *
         * @param strPacket
         *   Packet to parse and store.
         */
        public PenguinPacket(string strPacket) {
            strData = strPacket;
            intLength = strPacket.Length;
            try {
                if(strPacket.StartsWith("%")) {
                    Xt.XtParser xpParse = new Xt.XtParser();
                    xpParse.LoadXt(strPacket);
                    ptType = PacketType.Xt;
                    xpParser = xpParse;
                }else if(strPacket.StartsWith("<")) {
                    System.Xml.XmlDocument objXDoc = new System.Xml.XmlDocument();
                    objXDoc.LoadXml(strPacket);
                    objXml = objXDoc.DocumentElement;
                    ptType = PacketType.Xml;
                }else{
                    ptType = PacketType.Unknown;
                }
            }catch{
                ptType = PacketType.Unknown;                
            }
        }

    }

}
