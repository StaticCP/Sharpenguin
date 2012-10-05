/**
 * @file Penguin
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * The main namespace for Sharpenguin.
 */
namespace Sharpenguin {
    /*
     * Sharpenguin 1.0.0 - The C# Club Penguin Client.
     *
     * Designed for .NET Framework version 4.0.
     *
     * COPYRIGHT (C) 2012 LEWIS (STATIC).
     *
     * SPECIAL THANKS TO:
     * TIM (TEST)
     * -------------------
     * SAM (STANLEY)
     * -------------------
     *
     * LICENSED UNDER THE GNU LESSER GENERAL PUBLIC LICENSE - http://www.gnu.org/copyleft/lesser.html
     * YOU MAY NOT REMOVE THIS LICENSE, OR THE CREDITS INSIDE THIS LIBRARY WITHOUT EXPLICIT WRITTEN
     * PERMISSION FROM THE AUTHORS.
     *
     * IF YOU ARE AFFILIATED WITH DISNEY, CLUB PENGUIN ENTERTAINMENT INC OR NEW HORIZON INTERACTIVE
     * IN ANY WAY, YOU MAY NOT USE THIS SOFTWARE, VIEW THE SOURCE CODE OF THIS SOFTWARE OTHER THAN THIS
     * LICENSE. BY USING THIS SOFTWARE, YOU ARE STATING THAT YOU AGREE TO THESE TERMS.
     * IF YOU DO NOT AGREE TO THESE TERMS, DELETE ALL FOLDERS/FILES/SOFTWARE RELATED TO SHARPENGUIN
     * FROM YOUR COMPUTER(S).
     *
     * THE CREATORS OF THIS LIBRARY ARE IN NO WAY RESPONSIBLE FOR MISUSE OF THIS LIBRARY.
     * THIS LIBRARY IS IN NO WAY ILLEGAL AND SHOULD NEVER BE USED FOR ILLEGAL PURPOSES.
     */

    using Convert = System.Convert;

    public class Penguin : Tasks {

        /**
         * Penguin construct. Starts the loading of the system handlers.
         */
        public Penguin() : base() {
            System.Console.WriteLine("You are using Sharpenguin, the C# Club Penguin Client, which was created by Lewis (Static) and is registered under the LGPL license.");
            System.Console.WriteLine("Website: http://clubpenguinphp.info/\n");
            loadSystemHandlers();
            currentRoom.AddSelf(new Data.MyPlayer());
        }

        /**
         * Loads all of the system handlers into the handler table.
         */
        private void loadSystemHandlers() {
            Handler.Add("l", HandleLogin);
            Handler.Add("e", HandleError);
            Handler.Add("js", HandleJoinServer);
            Handler.Add("jr", HandleJoinRoom);
            Handler.Add("ap", HandleAddPlayer);
            Handler.Add("rp", HandleRemovePlayer);
            Handler.Add("sp", HandlePlayerMove);
            Handler.Add("sf", HandleSendFrame);
            Handler.Add("jg", HandleJoinGame);
            Handler.Add("lp", HandleLoadPlayer);
            Handler.Add("zo", HandleGameOver);
            Handler.Add("upc", HandleUpdateItem);
            Handler.Add("uph", HandleUpdateItem);
            Handler.Add("upf", HandleUpdateItem);
            Handler.Add("upn", HandleUpdateItem);
            Handler.Add("upb", HandleUpdateItem);
            Handler.Add("upa", HandleUpdateItem);
            Handler.Add("upe", HandleUpdateItem);
            Handler.Add("upl", HandleUpdateItem);
            Handler.Add("upp", HandleUpdateItem);
            Handler.Add("gi", HandleInventoryList);
        }

        /**
         * Handles the successful login packet, (handler "l"). Sent from both the login and game.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleLogin(Data.PenguinPacket receivedPacket) {
            if(blnIsLogin) {
                intPlayerID = Convert.ToInt32(receivedPacket.Xt.Arguments[0]);
                strLoginKey = receivedPacket.Xt.Arguments[2];
                LoginFinished();
            }else if(blnIsJoin) {
                SendJoin(strLoginKey);
            }
        }

        /**
         * Handles the error packet, (handler "e").
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleError(Data.PenguinPacket receivedPacket) {
            if(penguinErrorEvent != null) penguinErrorEvent(Convert.ToInt32(receivedPacket.Xt.Arguments[0]));
        }


        /**
         * Handles the join server packet, (handler "js"). Sent from the game.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleJoinServer(Data.PenguinPacket receivedPacket) {
            blnIsJoin = false;
            JoinFinished();
            SendGetInventory();
        }

        /**
         * Handles the Data.Player move packet, (handler "sp"), so that we can change the x and y positions of players in the Player classes.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandlePlayerMove(Data.PenguinPacket receivedPacket) {
            while(currentRoom == null) {} // Wait until the room object is set.
            Data.Player updatePlayer;
            if(currentRoom.TryGetPlayer(Convert.ToInt32(receivedPacket.Xt.Arguments[0]), out updatePlayer)) {
                updatePlayer.Position.SetX(Convert.ToInt32(receivedPacket.Xt.Arguments[1]));
                updatePlayer.Position.SetY(Convert.ToInt32(receivedPacket.Xt.Arguments[2]));
            }
        }

        /**
         * Handles the load Player packet, (handler "lp"). This is to load our Player object.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleLoadPlayer(Data.PenguinPacket receivedPacket) {
            currentRoom.Self.LoadData(receivedPacket.Xt.Arguments[0]);
            currentRoom.Self.SetCoins(Convert.ToInt32(receivedPacket.Xt.Arguments[1]));
            currentRoom.Self.SetAge(Convert.ToInt32(receivedPacket.Xt.Arguments[5]));
            currentRoom.Self.SetMinutesPlayed(Convert.ToInt32(receivedPacket.Xt.Arguments[7]));
        }

        /**
         * Handles the join room packet, (handler "jr"). This is so we can change the room numbers and load the new room object and Player objects.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleJoinRoom(Data.PenguinPacket receivedPacket) {
            extRoomID = Convert.ToInt32(receivedPacket.Xt.Arguments[0]);
            intRoomID =  receivedPacket.Xt.Room;
            string strName = (extRoomID < 999) ? Crumbs.Rooms.GetById(extRoomID)["name"] : "Igloo";
            currentRoom.ChangeRoom(strName, intRoomID, extRoomID);
            for(int intIndex = 1; intIndex < receivedPacket.Xt.Arguments.Length; intIndex++) {
                Data.Player newPlayer = new Data.Player();
                newPlayer.LoadData(receivedPacket.Xt.Arguments[intIndex]);
                currentRoom.AddPlayer(newPlayer);
            }
        }

        /**
         * Handles the add layer packet, (handler "ap"). This is so we can add a Data.Player to the room as they join.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleAddPlayer(Data.PenguinPacket receivedPacket) {
            Data.Player newPlayer = new Data.Player();
            newPlayer.LoadData(receivedPacket.Xt.Arguments[0]);
            if(newPlayer.Id != ID) {
                currentRoom.AddPlayer(newPlayer);
            }
        }

        /**
         * Handles the remove Player packet, (handler "rp"). This is so we can remove players from the room as they leave.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleRemovePlayer(Data.PenguinPacket receivedPacket) {
            int toRemove = Convert.ToInt32(receivedPacket.Xt.Arguments[0]);
            currentRoom.RemovePlayer(toRemove);
        }

        /**
         * Handles the change frame packet, (handler "sf"). This is so we can change their frame in their object when they change frame.
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleSendFrame(Data.PenguinPacket receivedPacket) {
            int intId = Convert.ToInt32(receivedPacket.Xt.Arguments[0]);
            Data.Player updatePlayer;
            if(currentRoom.TryGetPlayer(intId, out updatePlayer)) {
                updatePlayer.Position.SetFrame(Convert.ToInt32(receivedPacket.Xt.Arguments[1]));
            }
        }

        /**
         * Handles the join game packet, (handler "jg").
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleJoinGame(Data.PenguinPacket receivedPacket) {
            intRoomID = receivedPacket.Xt.Room;
            extRoomID = Convert.ToInt32(receivedPacket.Xt.Arguments[0]);
        }

        /**
         * Handles the game over packet, (handler "zo").
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleGameOver(Data.PenguinPacket receivedPacket) {
            currentRoom.Self.SetCoins(Convert.ToInt32(receivedPacket.Xt.Arguments[0]));
        }

        /**
         * Handles the update item packet, (handler "up*").
         *
         * @param receivedPacket
         *   The packet to handle.
         */
        private void HandleUpdateItem(Data.PenguinPacket receivedPacket) {
            Data.Player updatePlayer;
            int intItem = Convert.ToInt32(receivedPacket.Xt.Arguments[1]);
            if(Convert.ToInt32(receivedPacket.Xt.Arguments[1]) == ID) {
                updatePlayer = currentRoom.Self;
            }else{
                if(currentRoom.TryGetPlayer(Convert.ToInt32(receivedPacket.Xt.Arguments[0]), out updatePlayer) == false) return;
            }
            updatePlayer.Item.SetByCode(receivedPacket.Xt.Command, intItem);
        }

        /**
         * Handles the inventory list packet, (handler "gi").
         * @param receivedPacket
         *   The packet to handle.
         */
        public void HandleInventoryList(Data.PenguinPacket receivedPacket) {
            foreach(string itemId in receivedPacket.Xt.Arguments) currentRoom.Self.AddInventoryItem(Convert.ToInt32(itemId)); 
        }
    }
}
