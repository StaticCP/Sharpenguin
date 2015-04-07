/**
 * @file PenguinBase
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

namespace Sharpenguin {

    /**
     * An extension of PenguinBase with all of the tasks you can perform on the Club Penguin game servers.
     */
    public abstract class Tasks {

        /**
         *
         */
        public void SendBlocked(string strMessage) {
            SendData("%xt%s%m#mm%" + Room.IntID.ToString() + "%" + ID.ToString() + "%" + strMessage + "%");
        }
        
        /**
         * Sends an emoticon to the room.
         *
         * @param objEmoteId
         *   The id, or text representation, of the emote. (note, id is faster.)
         *
         * @return
         *   TRUE if emote exists, FALSE if it does not.
         */
        public bool SendEmote(object objEmoteId) {
            int intEmoteId;
            if(objEmoteId is int) {
                intEmoteId = (int) objEmoteId;
            }else if(objEmoteId is string) {
                if(Crumbs.Emoticons.ExistsByAttribute("value", objEmoteId as string) == false) return false;
                intEmoteId = int.Parse(Crumbs.Emoticons.GetByAttribute("value", objEmoteId as string)["id"]);
            }else{
                return false;
            }
            SendData("%xt%s%u#se%" + Room.IntID.ToString() + "%" + intEmoteId.ToString() + "%");
            return true;
        }

        /**
         * Sends a joke to the room.
         *
         * @param intJoke
         *   The id of the joke.
         *
         * @return
         *   TRUE if the joke exists, FALSE if it does not.
         */
        public bool SendJoke(int intJoke) {
            if(Crumbs.Jokes.ExistsById(intJoke) == false) return false;
            SendData("%xt%s%u#sj%" + Room.IntID.ToString() + "%" + intJoke.ToString() + "%");
            return true;
        }

        /**
         * Sends a safe message to the room.
         *
         * @param intSafe
         *   The id of the safe message.
         *
         * @return
         *   TRUE if the message exists, FALSE if it does not.
         */
        public bool SendSafe(int intSafe){
            if(Crumbs.SafeMessages.ExistsById(intSafe) == false) return false;
            SendData("%xt%s%u#ss%" + Room.IntID.ToString() + "%" + intSafe.ToString() + "%");
            return true;
        }

        /**
         *
         */
        public void SendLine(int intMessageID){
            SendData("%xt%s%u#sl%" + Room.IntID.ToString() + "%" + intMessageID.ToString() + "%");
        }

        /**
         *
         */
        public void SendQuick(int intMessageID){
            SendData("%xt%s%u#sq%" + Room.IntID.ToString() + "%" + intMessageID.ToString() + "%");
        }
        
        /**
         * Sends a tour guide message to the server.
         *
         * @param intMessageID
         *   The id of the tour guide message.
         */
        public void SendGuide(int intMessageID){
            SendData("%xt%s%u#sg%" + Room.IntID.ToString() + "%" + intMessageID.ToString() + "%");
        }

        /**
         * Sends a post card to a penguin.
         *
         * @param intPenguinID
         *   The id of the penguin to Send to.
         * @param intCardID
         *   The id of the card to Send.
         */
        public void SendMail(int intPenguinID, int intCardID){
            SendData("%xt%s%l#ms%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%" + intCardID.ToString() + "%");
        }

        /*
        public void buddyRequest(int intPenguinID){
            SendData("%xt%s%b#br%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
        }
        
        public void buddyAccept(int intPenguinID){
            SendData("%xt%s%b#ba%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
        }
        
        public void removeBuddy(int intPenguinID){
            SendData("%xt%s%b#rb%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        public void buddyCheat(int intPenguinID){
            SendData("%xt%s%b#br%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
            SendData("%xt%s%b#ba%" + Room.IntID.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        I believe these are broken due to the protocol change in buddies - I will add support for XMPP eventually.
        */

        /**
         * Adds a stamp - WARNING, YOU MAY BE BANNED. USE WITH CAUTION.
         *
         * @param intId
         *   Id of the stamp to add.
         */
        public void AddStamp(int intId){
            SendData("%xt%s%st#sse%" + Room.IntID.ToString() + "%" + intId.ToString() + "%");
        }

        /**
         * Opens the newspaper.
         */
        public void OpenNewspaper(){
            SendData("%xt%s%t#at%" + Room.IntID.ToString() + "%1%1%");
        }

        /**
         * Closes the newspaper.
         */
        public void CloseNewspaper(){
            SendData("%xt%s%t#rt%" + Room.IntID.ToString() + "%1%");
        }

        /**
         * Gets coins from a game.
         *
         * @param intAmount
         *   Amount of coins to receive.
         */
        public void GetCoins(int intAmount){
            SendData("%xt%z%zo%" + Room.IntID.ToString() + "%" + intAmount.ToString() + "%");
        }

        /**
         * Buys a new puffle.
         *
         * @param intPuffleID
         *   The item id of the puffle.
         * @param strName
         *   The name of your puffle.
         */
        public void BuyPuffle(int intPuffleID, string strName){
            SendData("%xt%s%p#pn%" + Room.IntID.ToString() + "%" + intPuffleID + "%" + strName + "%");
        }

        /**
         * Alias of join room, since the two are the same.
         *
         * @param gameRoom
         *   The id of the game's room.
         */
        public void JoinGame(object gameRoom) {
            JoinRoom(gameRoom);
        }

        /**
         * Requests EPF messages from the server.
         *
         * Requests EPF messages, which are usually displayed on the EPF phone.
         */
        public void RequestEpfMessages() {
            SendData("%xt%s%f#epfgm%" + Room.IntID.ToString() + "%");
        }

        /**
         * Joins a room.
         *
         * @param objRoom
         *   The id, or name, of the room - note, id is faster.
         *
         * @param intX
         *   The x position to enter the room.
         * @param intY
         *   The y position to enter the toom.
         *
         * @return
         *   TRUE if room exists, FALSE if it does not.
         */
        public bool JoinRoom(object objRoom, int intX = 0, int intY = 0) {
            int targetRoom;
            if((objRoom is int || objRoom is string) && ((objRoom is int) ? Crumbs.Rooms.ExistsById((int) objRoom) : Crumbs.Rooms.ExistsByAttribute("name", objRoom as string))) {
                targetRoom = (objRoom is int) ? (int) objRoom : int.Parse(Crumbs.Rooms.GetByAttribute("name", objRoom as string)["id"]);
            }else{
                return false;
            }
            if(targetRoom != Room.ExtID && (currentRoom.Self.IsMember || int.Parse(Crumbs.Rooms.GetAttributeById(targetRoom, "is_member")) == 0 )) {
                SendData("%xt%s%j#jr%" + Room.IntID.ToString() + "%" + targetRoom.ToString() + "%" + intX.ToString() + "%" + intY.ToString() + "%");
                return true;
            }else{
                return false;
            }
        }

        /**
         * Sends a phrase message to the room
         *
         * @param strId
         *   ID of the phrase.
         */
        public void SendPhraseMessage(string strId) {
            SendData("%xt%s%m#sc%" + Room.IntID.ToString() + "%" + playerID.ToString() + "%" + strId + "%");
        }


    }

}
