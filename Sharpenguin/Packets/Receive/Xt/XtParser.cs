using Regex = System.Text.RegularExpressions;

namespace Sharpenguin.Packets.Receive.Xt {
    /*
     * Everyone told me making this class was pointless...
     * ... I proved them wrong. :')
     */

    /// <summary>
    /// Parses xt packets for easier handling.
    /// </summary>
    public class XtParser {
        private int room; //< Room that the packet came from.
        private string command; //< Packet's command, which is used to determine the handler.
        private string[] arguments; //< Packet's arguments.

        /// <summary>
        /// Gets the room the packet was sent from.
        /// </summary>
        /// <value>The room the packet was sent from.</value>
        public int Room {
            get { return room; }
        }

        /// <summary>
        /// Gets the packet's command.
        /// </summary>
        /// <value>The packet's command.</value>
        public string Command {
            get { return command; }
        }

        /// <summary>
        /// Gets the packet's arguments.
        /// </summary>
        /// <value>The packet's arguments.</value>
        public string[] Arguments {
            get { return arguments; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Packets.Receive.Xt.XtParser"/> class.
        /// </summary>
        /// <param name="data">Raw packet data.</param>
        public XtParser(string data) {
            LoadXt(data);
        }

        /// <summary>
        /// Loads an xt string into the object.
        /// </summary>
        /// <param name="data">The xt string.</param>
        private void LoadXt(string data) {
            if(data == null) throw new System.ArgumentException("Parameter cannot be null.", "data");
            if(IsXt(data)) {
                string[] dataArray = data.Split('%');
                command = GetCommand(dataArray);
                room = GetRoom(dataArray);
                arguments = GetArguments(dataArray);
            }else{
                throw new InvalidXtException("The supplied data is not a valid Xt packet!");
            }
        }


        /// <summary>
        /// Determines wether the string is an xt string or not.
        /// </summary>
        /// <param name="data">The xt string.</param>
        /// <returns>TRUE if the string is an xt string, FALSE if not.</returns>
        private bool IsXt(string data) {
            Regex.Match match = Regex.Regex.Match(data, @"^(%xt%[a-z|A-Z|0-9|#|_]*%-?[0-9]*%.*?)$");
            return match.Success;
        }

        /// <summary>
        /// Gets the command from the xt string.
        /// </summary>
        /// <param name="data">The xt string to get the command from.</param>
        private string GetCommand(string[] data) {
            if(data.Length >= 3) {
                return data[2];
            }else{
                throw new InvalidXtException("Could not load Xt Command.");
            }
        }

        /// <summary>
        /// Gets internal room id from the xt string
        /// </summary>
        /// <param name="data">The xt data to get the room from.</param>
        private int GetRoom(string[] data) {
            if(data.Length >= 4) {
                return System.Convert.ToInt32(data[3]);
            }else{
                throw new InvalidXtException("Could not load Xt Room.");
            }
        }

        /// <summary>
        /// Takes the arguments from the xt string and puts them into a string array.
        /// </summary>
        /// <param name="data">The xt data to get the arguments from.</param>
        private string[] GetArguments(string[] data) {
            try {
                if(data.Length >= 5) {
                    string[] arguments = new string[data.Length - 5];
                    for(int i = 4; i < data.Length - 1; i++) arguments[i - 4] = data[i];
                    return arguments;
                }else{
                    return new string[0];
                }
            }catch{
                throw new InvalidXtException("Could not load Xt Arguments.");
            }
        } 

    }


}