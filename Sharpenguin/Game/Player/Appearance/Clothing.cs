namespace Sharpenguin.Game.Player.Appearance {
    /// <summary>
    /// Represents the items a player is wearing.
    /// </summary>
    public class Clothing {
        internal int colour;
        internal int head;
        internal int face;
        internal int neck;
        internal int body;
        internal int hand;
        internal int feet;
        internal int flag;
        internal int background;
        private Player player;

        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        /// <value>The colour.</value>
        public int Colour {
            get { return colour; }
            set {
                colour = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                head = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                face = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                neck = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                body = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                hand = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                feet = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                flag = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
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
                background = value;
                if(player is MyPlayer) {
                    MyPlayer me = (MyPlayer) player;
                    me.Connection.Send(new Packets.Send.Xt.Player.Appearance.UpdateBackground(me.Connection, value));
                }else{
                    throw new NotMeException("You cannot set items other players are wearing!");
                }
            }
        }

        public Clothing(Player player) {
            this.player = player;
        }

    }
}

