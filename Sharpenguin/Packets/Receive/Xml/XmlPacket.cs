using XML = System.Xml;

namespace Sharpenguin.Packets.Receive.Xml {
    public sealed class XmlPacket : Packet {
        /// <summary>
        /// The xml data.
        /// </summary>
        private XML.XmlElement xmlData;

        /// <summary>
        /// Gets the parsed xml data.
        /// </summary>
        /// <value>
        /// The parsed xml data.
        /// </value>
        public XML.XmlElement XmlData {
            get { return xmlData; }
        }

        /// <summary>
        /// XmlPacket Constructor.
        /// </summary>
        public XmlPacket(string data) : base(data) {
            LoadXml(data);
            if(xmlData.ChildNodes[0] != null && xmlData.ChildNodes[0].Attributes["action"] != null)
                command = xmlData.ChildNodes[0].Attributes["action"].Value;
            else
                throw new UnhandledPacketException("Unknown XML packet, cannot process.");
        }

        /// <summary>
        /// Parses the xml string.
        /// </summary>
        /// <param name="data">The xml string.</param>
        private void LoadXml(string data) {
            XML.XmlReaderSettings xmlSettings = new XML.XmlReaderSettings();
            xmlSettings.ProhibitDtd = true;
            xmlSettings.XmlResolver = null;
            using(System.IO.StringReader stringRead = new System.IO.StringReader(data)) {
                using(XML.XmlReader xmlRead = XML.XmlReader.Create(stringRead, xmlSettings)) {
                    XML.XmlDocument xmlDoc = new XML.XmlDocument();
                    xmlDoc.Load(xmlRead);
                    xmlData = xmlDoc.DocumentElement;
                }
            }
        }


    }
}
