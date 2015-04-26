using System;

namespace Sharpenguin.Packets.Send.Xml {
    /// <summary>
    /// Represents a version check packet.
    /// </summary>
    public class VersionCheck : Packets.Send.Packet {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Send.Xml.VersionCheck"/> class.
        /// </summary>
        /// <param name="version">Version.</param>
        public VersionCheck(int version) : base("<msg t='sys'><body action='verChk' r='0'><ver v='" + version.ToString() + "' /></body></msg>") {
        }
    }
}

