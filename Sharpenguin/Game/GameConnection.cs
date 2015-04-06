using System;

namespace Sharpenguin.Game {
    public class GameConnection : PenguinConnection {
        public event JoinHandler OnJoin; //< Event for handling join success.

        public GameConnection(string username, string loginkey) : base(username, loginkey) {
        }


        /**
         * Joins a game server via id.
         *
         * @param serverId
         *   The ID of the server to connect to.
         */
        public void Join(int serverId) {
            if(loginKey == "") throw new Exceptions.EarlyJoinException("You must login before you can join a game server!");
            System.Collections.Generic.Dictionary<string, string> serverCrumbs = Crumbs.Servers.GetById(serverId);
            serverName = serverCrumbs["name"];
            isJoin = true;
            InitialiseConnection(serverCrumbs["ip"], int.Parse(serverCrumbs["port"]));
        }

        /**
         * Joins a game server via name.
         *
         * @param serverName
         *   The name of the server to connect to.
         */
        public void Join(string serverName) {
            Join(Crumbs.Servers.GetIdByAttribute("name", serverName));
        }

        /**
         * Calls the onJoin event.
         */
        protected void JoinFinished() {
            isJoin = false;
            authenticated = true;
            if(OnJoin != null) OnJoin();
        }
    }
}

