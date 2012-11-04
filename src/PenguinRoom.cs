/**
 * @file PenguinRoom
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin.Data {
    using System.Collections.Generic;

    /**
     * Stores information about a room, including players, name and ids.
     */
    public class PenguinRoom {
        private MyPlayer myPlayer;
        private string strName = ""; //< Room name.
        private int intRoomID  = 0; //< Room internal id.
        private int intExtID   = 0; //< Room external id.
        private Dictionary<int, Player> dicPlayers; //< Dictionary of players.

        //! Gets the name of the room.
        public string Name {
            get { return strName; }
        }
        //! Gets the internal ID of the room.
        public int IntID {
            get { return intRoomID; }
        }
        //! Gets the external ID of the room.
        public int ExtID {
            get { return intExtID; }
        }
        //! Gets the dictionary of players.
        public Player[] Players {
            get {
                Player[] roomPlayers = new Player[dicPlayers.Count];
                dicPlayers.Values.CopyTo(roomPlayers, 0);
                return roomPlayers;
            }
        }
        //! Gets your own player.
        public MyPlayer Self {
            get { return myPlayer; }
        }

        public PenguinRoom() {
            dicPlayers = new Dictionary<int, Player>();
        }

        /**
         * Changes the room.
         *
         * @param roomName
         *   The name of the room.
         *
         * @param roomId
         *   The internal id of the room.
         *
         * @param extId
         *   The external id of the room.
         */
        public void ChangeRoom(string roomName, int roomId, int extId) {
            dicPlayers.Clear();
            strName = roomName; 
            intRoomID = roomId;
            intExtID = extId;
        }

        /**
         * Adds your own player to the room.
         *
         * @param newPlayer
         *   Your player object.
         */
        public void AddSelf(MyPlayer newPlayer) {
            myPlayer = newPlayer;
        }

        /**
         * Adds a player to the room.
         *
         * @param newPlayer
         *   The player to add.
         */
        public void AddPlayer(Player newPlayer) {
            if(dicPlayers.ContainsKey(newPlayer.Id) == false) {
                dicPlayers.Add(newPlayer.Id, newPlayer);
            }else{
                dicPlayers[newPlayer.Id] = newPlayer;
            }
        }

        /**
         * Gets a player from the room via their ID.
         *
         * @param intId
         *   The player's id.
         */
        public Player GetPlayer(int intId) {
            if(myPlayer != null && intId == myPlayer.Id) return myPlayer;
            Player gotPlayer;
            if(dicPlayers.TryGetValue(intId, out gotPlayer)) {
                return gotPlayer;
            }else{
                throw new Exceptions.NonExistantPlayerException("Player with id " + intId.ToString() + " does not exist!");
            }
        }

        /**
         * Tries to get a player from the room via their ID.
         *
         * @param intId
         *   The player's id.
         * @param gotPlayer
         *   The found player (if it exists).
         *
         * @return
         *   TRUE if the player is found, FALSE if it is not.
         */
        public bool TryGetPlayer(int intId, out Player gotPlayer) {
            if(intId == myPlayer.Id) {
                gotPlayer = (Player) myPlayer;
                return true;
            }
            gotPlayer = null;
            return dicPlayers.TryGetValue(intId, out gotPlayer);
        }
        
        /**
         * Tries to get a player from the room via their name.
         *
         * @param penuinName
         *  The username of the penguin.
         *
         * @param gotPlayer
         *  The found player (if it exists).
         *
         * @return
         *   TRUE if the player is found, FALSE if it is not.
         */
         public bool TryGetPlayerByName(string penguinName, out Player gotPlayer) {
            foreach(Player checkPenguin in dicPlayers.Values) {
                if(checkPenguin.Username == penguinName) {
                    gotPlayer = checkPenguin;
                    return true;
                }
            }
            gotPlayer = null;
            return false;
         }

        /**
         * Removes a player from the room via their ID.
         *
         * @param intId
         *   The id of the player to remove.
         *
         * @return
         *   TRUE on success, FALSE on failure.
         */
        public bool RemovePlayer(int intId) {
            if(dicPlayers.ContainsKey(intId)) {
                dicPlayers.Remove(intId);
                return true;
            }
            return false;
        }
        
        /**
         * Removes a player from the room via their name.
         *
         * @param penguinName
         *  The the name of the player to remove.
         *
         * @return
         *   TRUE on success, FALSE on failure.
         */
         public bool RemovePlayerByName(string penguinName) {
            foreach(int penguinIndex in dicPlayers.Keys) {
                if(dicPlayers[penguinIndex].Username == penguinName) {
                    dicPlayers.Remove(penguinIndex);
                    return true;
                }
            }
            return false;
         }

        /**
         * Determines whether the player is in the room or not.
         *
         * @param intId
         *   The id of the player to find.
         *
         * @return
         *   TRUE if the player is in the room, FALSE if they are not.
         */
        public bool HasPlayer(int intId) {
            if(intId == myPlayer.Id && myPlayer != null) return true;
            return dicPlayers.ContainsKey(intId);
        }
    }

}
