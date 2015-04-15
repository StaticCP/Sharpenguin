namespace Sharpenguin.Packets.Receive {
    public class HandlerTable<T> where T : Packet {
        /// <summary>
        /// A dictionary of packet handlers, indexed by command.
        /// </summary>
        private System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Generic.List<IPacketHandler<T>>> table = new System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Generic.List<IPacketHandler<T>>>();

        /// <summary>
        /// Adds a packet handler to the handler table.
        /// </summary>
        /// <param name="handler">The handler to add to the handler table.</param>
        public void Add(IPacketHandler<T> handler) {
            table.AddOrUpdate(
                handler.Handles,
                new System.Collections.Generic.List<IPacketHandler<T>>(new IPacketHandler<T>[] { handler }),
                (key, handlers) => {
                                       lock(handlers) handlers.Add(handler);
                                       return handlers;
                                   }
            );
        }

        /// <summary>
        /// Removes a packet handler from the handler table.
        /// </summary>
        /// <param name="handler">The handler to remove from the handler table.</param>
        public bool Remove(IPacketHandler<T> handler) {
            System.Collections.Generic.List<IPacketHandler<T>> handlers = null;
            if(table.TryGetValue(handler.Handles, out handlers))
                lock(handlers) return handlers.Remove(handler);
            return false;
        }

        /// <summary>
        /// Calls the appropriate handlers to handle a packet.
        /// </summary>
        /// <param name="receiver">The connection that received the packet.</param>
        /// <param name="packet">The packet.</param>
        public bool Handle(PenguinConnection receiver, T packet) {
            System.Collections.Generic.List<IPacketHandler<T>> list;
            IPacketHandler<T>[] handlers;
            if(table.TryGetValue(packet.Command, out list)) {
                lock(list) handlers = list.ToArray();
                foreach(IPacketHandler<T> handler in handlers)
                    handler.Handle(receiver, packet);
                return true;
            }
            return false;
        }

    }
}
