﻿using System.Collections.Generic;
using System.Linq;

namespace Sharpenguin.Game.Player.Appearance {
    /// <summary>
    /// Represents the items a player is wearing.
    /// </summary>
    public class Clothing {
        /// <summary>
        /// The player's colour.
        /// </summary>
        private int colour;
        /// <summary>
        /// The player's head item.
        /// </summary>
        private int head;
        /// <summary>
        /// The player's face item.
        /// </summary>
        private int face;
        /// <summary>
        /// The player's neck item.
        /// </summary>
        private int neck;
        /// <summary>
        /// The player's body item.
        /// </summary>
        private int body;
        /// <summary>
        /// The player's hand item.
        /// </summary>
        private int hand;
        /// <summary>
        /// The player's feet item.
        /// </summary>
        private int feet;
        /// <summary>
        /// The player's flag item.
        /// </summary>
        private int flag;
        /// <summary>
        /// The player's background item.
        /// </summary>
        private int background;
        /// <summary>
        /// The player.
        /// </summary>
        private Player player;

        /// <summary>
        /// Occurs when the clothing is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdate;
        /// <summary>
        /// Occurs when the colour is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateColour;
        /// <summary>
        /// Occurs when the head item is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateHead;
        /// <summary>
        /// Occurs when the face item is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateFace;
        /// <summary>
        /// Occurs when the neck item is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateNeck;
        /// <summary>
        /// Occurs when the body item is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateBody;
        /// <summary>
        /// Occurs when the hand item is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateHand;
        /// <summary>
        /// Occurs when the feet item is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateFeet;
        /// <summary>
        /// Occurs when the flag (pin) is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateFlag;
        /// <summary>
        /// Occurs when the background (photo) is updated.
        /// </summary>
        public event ClothingUpdateEventHandler OnUpdateBackground;

        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        /// <value>The colour.</value>
        public int Colour {
            get { return colour; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    colour = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateColour(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the head.
        /// </summary>
        /// <value>The head.</value>
        public int Head {
            get { return head; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    head = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateHead(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the face.
        /// </summary>
        /// <value>The face.</value>
        public int Face {
            get { return face; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    face = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateFace(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the neck.
        /// </summary>
        /// <value>The neck.</value>
        public int Neck {
            get { return neck; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    neck = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateNeck(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public int Body {
            get { return body; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    body = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateBody(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the hand.
        /// </summary>
        /// <value>The hand.</value>
        public int Hand {
            get { return hand; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    hand = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateHands(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the feet.
        /// </summary>
        /// <value>The feet.</value>
        public int Feet {
            get { return feet; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    feet = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateFeet(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        /// <value>The flag.</value>
        public int Flag {
            get { return flag; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    flag = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateFlag(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public int Background {
            get { return background; }
            set {
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    background = value;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateBackground(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sharpenguin.Game.Player.Appearance.Clothing"/> class.
        /// </summary>
        /// <param name="player">The parent player.</param>
        /// <param name="colour">Current colour.</param>
        /// <param name="head">Current head item.</param>
        /// <param name="face">Current face item.</param>
        /// <param name="neck">Current neck item.</param>
        /// <param name="body">Current body item.</param>
        /// <param name="hand">Current hand item.</param>
        /// <param name="feet">Current feet item.</param>
        /// <param name="flag">Current flag item.</param>
        /// <param name="background">Cutrent background item.</param>
        public Clothing(Player player, int colour, int head, int face, int neck, int body, int hand, int feet, int flag, int background) {
            this.player = player;
            this.colour = colour;
            this.head = head;
            this.face = face;
            this.neck = neck;
            this.body = body;
            this.hand = hand;
            this.feet = feet;
            this.flag = flag;
            this.background = background;
        }

        /// <summary>
        /// Represents a general clothing update handler.
        /// </summary>
        abstract class UpdateHandler {
            /// <summary>
            /// The type character (e.g. "h" for head.)
            /// </summary>
            private char type;

            /// <summary>
            /// Gets the command that this packet handler handles.
            /// </summary>
            /// <value>The command that this packet handler handles.</value>
            public string Handles {
                get { return "up" + type; }
            }

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.UpdateHandler"/> class.
            /// </summary>
            /// <param name="type">The type characted.</param>
            public UpdateHandler(char type) {
                this.type = type;
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
                int id;
                int item;
                if(game != null && int.TryParse(packet.Arguments[0], out id) && int.TryParse(packet.Arguments[1], out item)) {
                    if(id == game.Id) {
                        Update(game.Player, item);
                        if(game.Player.Clothing.OnUpdate != null) game.Player.Clothing.OnUpdate(game.Player, game.Player.Clothing);
                    }else{
                        IEnumerable<Player> players = game.Room.Players.Where(p => p.Id == id);
                        foreach(Player player in players) {
                            Update(player, item);
                            if(player.Clothing.OnUpdate != null) game.Player.Clothing.OnUpdate(player, player.Clothing);
                        }
                    }
                }
            }

            protected abstract void Update(Player player, int item);
        }

        /// <summary>
        /// Represents a colour update handler.
        /// </summary>
        class ColourUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.ColourUpdateHandler"/> class.
            /// </summary>
            public ColourUpdateHandler() : base('c') { }

            /// <summary>
            /// Update the colour of the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.colour = item;
                if(player.Clothing.OnUpdateColour != null) player.Clothing.OnUpdateColour(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a head item update handler.
        /// </summary>
        class HeadUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.HeadUpdateHandler"/> class.
            /// </summary>
            public HeadUpdateHandler() : base('h') { }

            /// <summary>
            /// Update the head item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.head = item;
                if(player.Clothing.OnUpdateHead != null) player.Clothing.OnUpdateHead(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a face item update handler.
        /// </summary>
        class FaceUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.FaceUpdateHandler"/> class.
            /// </summary>
            public FaceUpdateHandler() : base('f') { }

            /// <summary>
            /// Update the face item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.face = item;
                if(player.Clothing.OnUpdateFace != null) player.Clothing.OnUpdateFace(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a neck item update handler.
        /// </summary>
        class NeckUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.NeckUpdateHandler"/> class.
            /// </summary>
            public NeckUpdateHandler() : base('n') { }

            /// <summary>
            /// Update the neck item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.neck = item;
                if(player.Clothing.OnUpdateNeck != null) player.Clothing.OnUpdateNeck(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a body item update handler.
        /// </summary>
        class BodyUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.BodyUpdateHandler"/> class.
            /// </summary>
            public BodyUpdateHandler() : base('b') { }

            /// <summary>
            /// Update the body item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.body = item;
                if(player.Clothing.OnUpdateBody != null) player.Clothing.OnUpdateBody(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a hand item update handler.
        /// </summary>
        class HandUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.HandUpdateHandler"/> class.
            /// </summary>
            public HandUpdateHandler() : base('a') { }

            /// <summary>
            /// Update the hand item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.hand = item;
                if(player.Clothing.OnUpdateHand != null) player.Clothing.OnUpdateHand(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a feet item update handler.
        /// </summary>
        class FeetUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.FeetUpdateHandler"/> class.
            /// </summary>
            public FeetUpdateHandler() : base('e') { }

            /// <summary>
            /// Update the feet item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.feet = item;
                if(player.Clothing.OnUpdateFeet != null) player.Clothing.OnUpdateFeet(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a flag item update handler.
        /// </summary>
        class FlagUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.FlagUpdateHandler"/> class.
            /// </summary>
            public FlagUpdateHandler() : base('l') { }

            /// <summary>
            /// Update the flag item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.flag = item;
                if(player.Clothing.OnUpdateFlag != null) player.Clothing.OnUpdateFlag(player, player.Clothing);
            }
        }

        /// <summary>
        /// Represents a background item update handler.
        /// </summary>
        class BackgroundUpdateHandler : UpdateHandler, Packets.Receive.IGamePacketHandler<Sharpenguin.Packets.Receive.Xt.XtPacket> {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Sharpenguin.Game.Player.Appearance.Clothing.BackgroundUpdateHandler"/> class.
            /// </summary>
            public BackgroundUpdateHandler() : base('p') { }

            /// <summary>
            /// Update the background item on the specified player.
            /// </summary>
            /// <param name="player">Player.</param>
            /// <param name="item">Item identifier.</param>
            protected override void Update(Player player, int item) {
                player.Clothing.background = item;
                if(player.Clothing.OnUpdateBackground != null) player.Clothing.OnUpdateBackground(player, player.Clothing);
            }
        }
    }
}

