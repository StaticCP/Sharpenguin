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

        /// <summary>
        /// The connection this inventory belongs to.
        /// </summary>
        private MyPlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Inventory.Inventory"/> class.
        /// </summary>
        /// <param name="connection">The connection this inventory belongs to.</param>
        public Inventory(MyPlayer player) {
            this.player = player;
        }

        /// <summary>
        /// Load the inventory from the specified packet.
        /// </summary>
        /// <param name="packet">The packet to load the inventory from.</param>
        public void Load(Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
            if(packet.Command == "gi") {
                items.Clear();
                foreach(string i in packet.Arguments) {
                    try {
                        int id = int.Parse(i);
                        items.Add(Configuration.Configuration.Items[id]);
                    }catch(Configuration.Game.NonExistentItemException ex) {
                        // Will be logged.
                    }catch(System.FormatException ex) {
                        // Will be logged.
                    }
                }
            }
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
            if(player.Wallet.Amount >= item.Price) {
                items.Add(item);
                player.Connection.Send(new Packets.Send.Xt.Player.Inventory.AddItem(player.Connection, item.Id));
            }else{
                // Not that we care, we can make a money maker..
                throw new Money.NotEnoughCoinsException("You do not have enough coins to buy this item!");
            }
        }

        /// <summary>
        /// Search for an item by any of it's properties.
        /// </summary>
        /// <param name="predictate">Search predictate.</param>
        public IEnumerable<Configuration.Game.Item> Where(System.Func<Configuration.Game.Item, bool> predictate) {
            return items.Where<Configuration.Game.Item>(predictate);
        }
    }
}

