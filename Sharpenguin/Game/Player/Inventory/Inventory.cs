using System.Collections.Generic;
using System.Linq;

namespace Sharpenguin.Game.Player.Inventory {
    /// <summary>
    /// Describes a player's inventory.
    /// </summary>
    public class Inventory {
        /// <summary>
        /// The items in the inventory.
        /// </summary>
        private List<Configuration.Game.Item> items = new List<Configuration.Game.Item>();
        private object _lock = new object();

        /// <summary>
        /// The connection this inventory belongs to.
        /// </summary>
        private MyPlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.Inventory.Inventory"/> class.
        /// </summary>
        /// <param name="player">The parent player.</param>
        public Inventory(MyPlayer player) {
            if(player == null) throw new System.ArgumentNullException("player", "Argument cannot be null.");
            this.player = player;
        }

        /// <summary>
        /// Adds the item with the specified ID.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Add(int id) {
            Add(Configuration.Configuration.Items[id]);
        }

        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(Configuration.Game.Item item) {
            if(item == null) throw new System.ArgumentNullException("item", "Argument cannot be null.");
            if(player.Wallet.Amount >= item.Price) {
                player.Connection.Send(new Packets.Send.Xt.Player.Inventory.AddItem(player.Connection, item.Id));
            }else{
                // Not that we care, we can make a money maker..
                throw new Money.NotEnoughCoinsException("You do not have enough coins to buy this item!");
            }
        }

        class GetInventoryHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "gi"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="connection">The connection that received the packet.</param>
            /// <param name="packet">The packet.</param>
            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                GameConnection game = connection as GameConnection;
                if(game != null) {
                    game.Player.Inventory.items.Clear();
                    lock(game.Player.Inventory._lock) {
                        foreach(string s in packet.Arguments) {
                            int id;
                            try {
                                if(int.TryParse(s, out id)) {
                                    game.Player.Inventory.items.Add(Configuration.Configuration.Items[id]);
                                } else {
                                    Configuration.Configuration.Logger.Error("Could not parse inventory item id as an integer. The given id was: " + s);
                                }
                            } catch(Configuration.Game.NonExistentItemException ex) {
                                Configuration.Configuration.Logger.Info(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        class AddItemHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "ai"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="connection">The connection that received the packet.</param>
            /// <param name="packet">The packet.</param>
            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                GameConnection game = connection as GameConnection;
                if(packet.Arguments.Length >= 2 && game != null) {
                    int id;
                    int coins;
                    if(int.TryParse(packet.Arguments[0], out id) && int.TryParse(packet.Arguments[1], out coins)) {
                        Configuration.Game.Item item = Configuration.Configuration.Items[id];
                        lock(game.Player.Inventory._lock)
                            game.Player.Inventory.items.Add(item);
                        game.Player.Wallet.Amount = coins;
                    }
                }
            }
        }

        /// <summary>
        /// Search for an item by any of it's properties.
        /// </summary>
        /// <param name="predictae">Search predicate.</param>
        public IEnumerable<Configuration.Game.Item> Where(System.Func<Configuration.Game.Item, bool> predicate) {
            if(predicate == null) throw new System.ArgumentNullException("predicate", "Argument cannot be null.");
            lock(_lock)
                return items.Where<Configuration.Game.Item>(predicate);
        }

        /// <summary>
        /// Gets whether an item with the specified properties exists.
        /// </summary>
        /// <param name="predicate">Search predicate.</param>
        public bool Exists(System.Func<Configuration.Game.Item, bool> predicate) {
            if(predicate == null) throw new System.ArgumentNullException("predicate", "Argument cannot be null.");
            return Where(predicate).Count() != 0;
        }
    }
}

