﻿using System;
using System.Collections.Generic;

namespace Sharpenguin.Game.Room {
    public class Room {
        private List<Game.Player.Player> players = new List<Game.Player.Player>();
        public event JoinRoomEventHandler OnJoin;
        public event LeaveRoomEventHandler OnLeave;

        public IReadOnlyList<Game.Player.Player> Players {
            get {
                return players.AsReadOnly();
            }
        }

        public Game.Player.MyPlayer Self {
            get;
            internal set;
        }

        public int Id {
            get;
            internal set;
        }

        public int External {
            get;
            internal set;
        }

        public string Name {
            get;
            internal set;
        }

        internal void Add(Game.Player.Player player) {
            players.Add(player);
            if(OnJoin != null)
                OnJoin(player);
        }

        internal void Remove(Game.Player.Player player) {
            players.Remove(player);
            if(OnLeave != null)
                OnLeave(player);
        }

        public void Join(Configuration.Game.Room room) {
            Join(room.Id);
        }

        public void Join(int id) {
            if(id <= 1000) {

            } else {
                //SendData("%xt%s%g#gm%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
                //SendData("%xt%s%p#pg%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
                //SendData("%xt%s%j#jp%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
            }
        }
    }
}
