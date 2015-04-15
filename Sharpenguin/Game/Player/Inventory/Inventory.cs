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
                player.Connection.Send(new Packets.Send.Xt.Player.Inventory.AddItem(player.Connection, item.Id));
            }else{
                // Not that we care, we can make a money maker..
                throw new Money.NotEnoughCoinsException("You do not have enough coins to buy this item!");
            }
        }

        class GetInventoryHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            public string Handles {
                get { return "gi"; }
            }

            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                GameConnection game = connection as GameConnection;
                if(game != null) {
                    game.Player.Inventory.items.Clear();
                    foreach(string s in packet.Arguments) {
                        int id;
                        try {
                            if(int.TryParse(s, out id)) {
                                game.Player.Inventory.items.Add(Configuration.Configuration.Items[id]);
                            } else {
                                // Will be logged
                            }
                        } catch(Configuration.Game.NonExistentItemException ex) {
                            // Will be logged.
                        }
                    }
                }
            }
        }

        class AddItemHandler : Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            public string Handles {
                get { return "ai"; }
            }

            public void Handle(PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                GameConnection game = connection as GameConnection;
                if(packet.Arguments.Length >= 2 && game != null) {
                    int id;
                    int coins;
                    if(int.TryParse(packet.Arguments[0], out id) && int.TryParse(packet.Arguments[1], out coins)) {
                        Configuration.Game.Item item = Configuration.Configuration.Items[id];
                        game.Player.Inventory.items.Add(item);
                        game.Player.Wallet.Amount = coins;
                    }
                }
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

