using System;

namespace Sharpenguin.Game.Player.Relations.Buddies {
    // This still all needs to be implemented, merely here for buddy find for now.

    /// <summary>
    /// Represents the player's buddy list.
    /// </summary>
    public class Buddies {
        /// <summary>
        /// The parent connection.
        /// </summary>
        private MyPlayer me;

        /// <summary>
        /// Occurs when a buddy is found.
        /// </summary>
        public event BuddyFindEventHandler OnFound;

        /// <summary>
        /// The ID of the player currently being found.
        /// </summary>
        private int finding = 0; // Annoyingly, Club Penguin doesn't return it..


        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.Relations.Buddies.Buddies"/> class.
        /// </summary>
        public Buddies(MyPlayer me) {
            this.me = me;
        }

        /// <summary>
        /// Find the buddy with the specified id.
        /// </summary>
        /// <param name="id">Buddy identifier.</param>
        public void Find(int id) {
            if(finding != 0) return;
            finding = id;
            me.Connection.Send(new Packets.Send.Xt.Player.Relations.Buddies.FindBuddy(me.Connection, id));
        }

        class BuddyFindHandler : Sharpenguin.Game.Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "bf"; }
            }

            /// <summary>
            /// Handle the given packet.
            /// </summary>
            /// <param name="connection">The connection that received the packet.</param>
            /// <param name="packet">The packet.</param>
            public void Handle(Sharpenguin.PenguinConnection connection, Sharpenguin.Packets.Receive.Xt.XtPacket packet) {
                if(connection == null) throw new System.ArgumentNullException("connection", "Argument cannot be null.");
                if(packet == null) throw new System.ArgumentNullException("packet", "Argument cannot be null.");
                Sharpenguin.Game.GameConnection game = connection as Sharpenguin.Game.GameConnection;
                if(game != null) {
                    int id = game.Player.Buddies.finding;
                    if(id != 0) {
                        int room;
                        game.Player.Buddies.finding = 0;
                        if(game != null && game.Player != null && packet.Arguments.Length >= 1 && int.TryParse(packet.Arguments[0], out room))
                            game.Player.Buddies.OnFound(game.Player, id, room);
                    }
                }
            }

        }

    }
}

