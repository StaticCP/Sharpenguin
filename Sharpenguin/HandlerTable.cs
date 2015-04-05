/**
 * @file HandlerTable
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Xt {
    using System.Collections.Generic;

    /**
     * Class to handle Xt packets.
     */
    public class HandlerTable {
        private Dictionary<string, List<PacketHandler>> dicHandlers; //< Dictionary of handlers.

        /**
         * Handler table constructor. Creates the dictionary needed for storing handler delegates.
         */
        public HandlerTable() {
            dicHandlers = new Dictionary<string, List<PacketHandler>>();
        }

        /**
         * Adds a new handler.
         *
         * @param strCommand
         *   The xt command that this handler should be registered to.
         *
         * @param toAdd
         *   The delegate that the handler uses.
         */
        public void Add(string strCommand, PacketHandler toAdd) {
            if(dicHandlers.ContainsKey(strCommand) == false) dicHandlers.Add(strCommand, new List<PacketHandler>());
            dicHandlers[strCommand].Add(toAdd);
        }

        /**
         * Executes a handler by the command.
         *
         * @param strCommand
         *   The xt command of the received packet.
         * @param receivedPacket
         *   The packet that was received.
         */
        public void Execute(string strCommand, Data.PenguinPacket receivedPacket) {
            if(dicHandlers.ContainsKey(strCommand) == false) return;
            foreach(PacketHandler toExecute in dicHandlers[strCommand]) {
                toExecute(receivedPacket);
            }
        }


        /**
         * Removes a handler by the command.
         *
         * @param strCommand
         *   The xt command that the handler is registered to.
         * @param toRemove
         *   The delegate to remove.
         */
        public void Remove(string strCommand, PacketHandler toRemove) {
            if(dicHandlers.ContainsKey(strCommand) == false) return;
            dicHandlers[strCommand].Remove(toRemove);
        }

    }

}
