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
    public abstract class Tasks : PenguinBase {
        /**
         * Constructor, extended from the base class.
         */
        public Tasks() : base() {}

        /**
         * Sends a message to everyone in the room.
         *
         * @param strMessage
         *   The message to Send.
         */
        public void SendMessage(string strMessage) {
            SendData("%xt%s%m#sm%" + intRoom.ToString() + "%" + ID.ToString() + "%" + strMessage + "%");
        }
        
        /**
         *
         */
        public void SendBlocked(string strMessage) {
            SendData("%xt%s%m#mm%" + intRoom.ToString() + "%" + ID.ToString() + "%" + strMessage + "%");
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
                intEmoteId = System.Convert.ToInt32(Crumbs.Emoticons.GetByAttribute("value", objEmoteId as string)["id"]);
            }else{
                return false;
            }
            SendData("%xt%s%u#se%" + intRoom.ToString() + "%" + intEmoteId.ToString() + "%");
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
            SendData("%xt%s%u#sj%" + intRoom.ToString() + "%" + intJoke.ToString() + "%");
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
            SendData("%xt%s%u#ss%" + intRoom.ToString() + "%" + intSafe.ToString() + "%");
            return true;
        }

        /**
         *
         */
        public void SendLine(int intMessageID){
            SendData("%xt%s%u#sl%" + intRoom.ToString() + "%" + intMessageID.ToString() + "%");
        }

        /**
         *
         */
        public void SendQuick(int intMessageID){
            SendData("%xt%s%u#sq%" + intRoom.ToString() + "%" + intMessageID.ToString() + "%");
        }
        
        /**
         * Sends a tour guide message to the server.
         *
         * @param intMessageID
         *   The id of the tour guide message.
         */
        public void SendGuide(int intMessageID){
            SendData("%xt%s%u#sg%" + intRoom.ToString() + "%" + intMessageID.ToString() + "%");
        }

        /**
         * Sends a new player position to the room, effectively moving your player.
         *
         * @param intX
         *   X Position to move to.
         * @param intY
         *   Y Position to move to.
         */
        public void SendPosition(int intX, int intY){
            SendData("%xt%s%u#sp%" + intRoom.ToString() + "%" + intX.ToString() + "%" + intY.ToString() + "%");
        }

        /**
         * Throws a snowball.
         *
         * Throws a snowball to the specified x and y position.
         *
         * @param intX
         *   The x position to throw the snowball.
         * @param intY
         *   The y position to throw the snowball.
         */
        public void snowBall(int intX, int intY){
            SendData("%xt%s%u#sb%" + intRoom.ToString() + "%" + intX.ToString() + "%" + intY.ToString() + "%");
        }

        /**
         * Send an action to the room.
         *
         * @param intActionID
         *   The id of the action to Send.
         */
        public void SendAction(int intActionID){
            SendData("%xt%s%u#sa%" + intRoom.ToString() + "%" + intActionID.ToString() + "%");
        }

        /**
         * Sends a frame to the room.
         *
         * @param intFrameID
         *  The id of the frame to Send.
         */
        public void SendFrame(int intFrameID){
            SendData("%xt%s%u#sf%" + intRoom.ToString() + "%" + intFrameID.ToString() + "%");
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
            SendData("%xt%s%l#ms%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%" + intCardID.ToString() + "%");
        }

        /*
        public void buddyRequest(int intPenguinID){
            SendData("%xt%s%b#br%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }
        
        public void buddyAccept(int intPenguinID){
            SendData("%xt%s%b#ba%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }
        
        public void removeBuddy(int intPenguinID){
            SendData("%xt%s%b#rb%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        public void buddyCheat(int intPenguinID){
            SendData("%xt%s%b#br%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
            SendData("%xt%s%b#ba%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        I believe these are broken due to the protocol change in buddies - I will add support for XMPP eventually.
        */

        /**
         * Finds a penguin by their ID.
         *
         * @param intPenguinID
         *   The id of the penguin to find
         */
        public void findBuddy(int intPenguinID){
            SendData("%xt%s%u#bf%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        /**
         * Ignores a player by their id.
         *
         * @param intPenguinID
         *   The id of the penguin to ignore.
         */
        public void addIgnore(int intPenguinID){
            SendData("%xt%s%n#an%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        /**
         * Removes player from ignore list bt their id.
         *
         * @param intPenguinID
         *   The id of the penguin to ignore.
         */
        public void removeIgnore(int intPenguinID){
            SendData("%xt%s%n#rn%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        /**
         * Adds an item to the player's inventory.
         *
         * @param intItemId
         *   Id of the item to add.
         */
        public void addItem(int intItemId){
            SendData("%xt%s%i#ai%" + intRoom.ToString() + "%" + intItemId.ToString() + "%");
        }

        /**
         * Adds a stamp - WARNING, YOU MAY BE BANNED. USE WITH CAUTION.
         *
         * @param intId
         *   Id of the stamp to add.
         */
        public void addStamp(int intId){
            SendData("%xt%s%st#sse%" + intRoom.ToString() + "%" + intId.ToString() + "%");
        }

        /**
         * Change's the penguin's colour.
         *
         * @param intItemID
         *   The id of the colour to change to.
         */
        public void updateColour(int intItemID){
            SendData("%xt%s%s#upc%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's head item.
         *
         * @param intItemID
         *   The id of the head item to wear.
         */
        public void updateHead(int intItemID){
            SendData("%xt%s%s#uph%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's face item.
         *
         * @param intItemID
         *   The id of the face item to wear.
         */
        public void updateFace(int intItemID){
            SendData("%xt%s%s#upf%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's neck item.
         *
         * @param intItemID
         *   The id of the neck item to wear.
         */
        public void updateNeck(int intItemID){
            SendData("%xt%s%s#upn%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's body item.
         *
         * @param intItemID
         *   The id of the body item to wear.
         */
        public void updateBody(int intItemID){
            SendData("%xt%s%s#upb%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's hand item.
         *
         * @param intItemID
         *   The id of the hand item to wear.
         */
        public void updateHand(int intItemID){
            SendData("%xt%s%s#upa%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's feet item.
         *
         * @param intItemID
         *   The id of the feet item to wear.
         */
        public void updateFeet(int intItemID){
            SendData("%xt%s%s#upe%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's flag item.
         *
         * @param intItemID
         *   The id of the flag item to wear.
         */
        public void updateFlag(int intItemID){
            SendData("%xt%s%s#upl%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Change's the player's photo item.
         *
         * @param intItemID
         *   The id of the photo item to wear.
         */
        public void updatePhoto(int intItemID){
            SendData("%xt%s%s#upp%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }
        
        public void updateRemove(int intItemID){
            SendData("%xt%s%s#upr%" + intRoom.ToString() + "%" + intItemID.ToString() + "%");
        }

        /**
         * Joins a player's igloo.
         *
         * @param intPenguinID
         *   The id of the penguin who's igloo you wish to go to.
         */
        public void joinIgloo(int intPenguinID){
            if(extRoom == intPenguinID) return;
            SendData("%xt%s%g#gm%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
            SendData("%xt%s%p#pg%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
            SendData("%xt%s%j#jp%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        /**
         * Opens the newspaper.
         */
        public void openNewspaper(){
            SendData("%xt%s%t#at%" + intRoom.ToString() + "%1%1%");
        }

        /**
         * Closes the newspaper.
         */
        public void closeNewspaper(){
            SendData("%xt%s%t#rt%" + intRoom.ToString() + "%1%");
        }

        /**
         * Gets coins from a game.
         *
         * @param intAmount
         *   Amount of coins to receive.
         */
        public void getCoins(int intAmount){
            SendData("%xt%z%zo%" + intRoom.ToString() + "%" + intAmount.ToString() + "%");
        }

        public void getPlayer(int intPenguinID){
            SendData("%xt%s%u#gp%" + intRoom.ToString() + "%" + intPenguinID.ToString() + "%");
        }

        /**
         * Buys a new puffle.
         *
         * @param intPuffleID
         *   The item id of the puffle.
         * @param strName
         *   The name of your puffle.
         */
        public void buyPuffle(int intPuffleID, string strName){
            SendData("%xt%s%p#pn%" + intRoom.ToString() + "%" + intPuffleID + "%" + strName + "%");
        }

        /**
         * Alias of join room, since the two are the same.
         *
         * @param intGameRoom
         *   The id of the game's room.
         */
        public void joinGame(object objGameRoom) {
            joinRoom(objGameRoom);
        }

        /**
         * Requests EPF messages from the server.
         *
         * Requests EPF messages, which are usually displayed on the EPF phone.
         */
        public void requestEpfMessages() {
            SendData("%xt%s%f#epfgm%" + intRoom.ToString() + "%");
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
        public bool joinRoom(object objRoom, int intX = 0, int intY = 0) {
            int targetRoom;
            if(objRoom is int) {
                if(Crumbs.Rooms.ExistsById((int) objRoom) == false) return false;
                targetRoom = (int) objRoom;
            }else if(objRoom is string) {
                if(Crumbs.Rooms.ExistsByAttribute("name", objRoom as string) == false) return false;
                targetRoom = System.Convert.ToInt32(Crumbs.Rooms.GetByAttribute("name", objRoom as string)["id"]);
            }else{
                return false;
            }
            if(targetRoom != extRoomID && (currentRoom.Self.IsMember || System.Convert.ToInt32(Crumbs.Rooms.GetAttributeById(targetRoom, "is_member")) == 0 )) {
                SendData("%xt%s%j#jr%" + intRoom.ToString() + "%" + targetRoom.ToString() + "%" + intX.ToString() + "%" + intY.ToString() + "%");
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
            SendData("%xt%s%m#sc%" + intRoom.ToString() + "%" + intPlayerID.ToString() + "%" + strId + "%");
        }

        /**
         * Sends a join packet to a game server.
         *
         * @param strLoginKey
         *   The login key we were given by the login server.
         */
        public void SendJoin(string strLoginKey) {
            SendData("%xt%s%j#js%" + intRoomID.ToString() + "%" + intPlayerID.ToString() + "%" + strLoginKey + "%en%");
        }

        /**
         * Asks the server for your inventory.
         *
         * @param strLoginKey
         *   The login key we were given by the login server.
         */
        public void SendGetInventory() {
            SendData("%xt%s%i#gi%" + intRoomID.ToString() + "%");
        }

    }

}
