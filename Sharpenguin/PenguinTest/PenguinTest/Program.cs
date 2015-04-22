using System;
using System.Collections.Generic;
using System.Linq;

namespace PenguinTest {
    class MainClass {

        public static void Main(string[] args) {
            // Get the username and password
            System.Console.Write("Input username: ");
            string username = System.Console.ReadLine();
            System.Console.Write("Input password: ");
            string password = System.Console.ReadLine();
            Penguin p = new Penguin(username, password); // Create our penguin
            p.Initialise(); // Initialise the penguin
            System.Console.WriteLine("Press any key to stop...");
            System.Console.ReadKey();
        }
            
    }

    class Penguin {
        private string username; // Our username
        private string password; // Our password
        private Sharpenguin.Game.Player.MyPlayer me; // Our player
        private Sharpenguin.Game.Player.Player following; // The player we're following

        public Penguin(string username, string password) {
            this.username = username;
            this.password = password;
        }

        public void Initialise() {
            Sharpenguin.Login.LoginConnection login = new Sharpenguin.Login.LoginConnection(username, password); // Create a new login connection
            // Attatch the event handlers to the login connection
            login.OnLogin += HandleLogin;
            login.OnError += HandleError;
            login.OnDisconnect += HandleDisconnect;
            // Connect to the login server and authenticate with it.
            login.Authenticate(Sharpenguin.Configuration.Configuration.LoginServers[1]);
        }

        public void HandleLogin(Sharpenguin.Game.GameConnection game) {
            // Attatch our game events
            game.OnJoin += HandleJoin;
            game.Room.OnJoin += HandleJoinRoom;
            game.OnDisconnect += HandleDisconnect;
            game.Room.OnLeave += HandleLeaveRoom;
            // Initiate the join to blizzard (ID 3100)..
            game.Authenticate(Sharpenguin.Configuration.Configuration.GameServers[3100]);
        }

        public void HandleJoin(Sharpenguin.Game.GameConnection game) {
            System.Console.WriteLine("Joined!"); // We joined the server, hurrah!
            game.OnLoad += HandleLoad; // Attatch the OnLoad event
        }

        public void HandleLoad(Sharpenguin.Game.Player.MyPlayer me) {
            // We've got the MyPlayer instance, hoorah!
            this.me = me;
            // Attatch the buddy find event
            me.Buddies.OnFound += BuddyFindHandler;
            // Go to the town..
            me.Enter(100);
        }

        public void HandleMove(Sharpenguin.Game.Player.Player player, Sharpenguin.Game.Player.Position position) {
            me.Position.Set(position.X, position.Y); // Follow the player..
        }

        public void HandleMessage(Sharpenguin.Game.Player.Player player, string message) {
            if(message.ToLower() == "follow me") { // Did someone ask to be followed?
                Follow(player); // Follow them!
            }else if(player == following) { // Is the person we're following speaking?
                me.Say(message); // Repeat them!
            }
        }

        public void HandleEmote(Sharpenguin.Game.Player.Player player, Sharpenguin.Game.Player.Emotes emote) {
            // Repeat their emotion
            me.Say(emote);
        }

        public void HandleJoinRoom(Sharpenguin.Game.Player.Player player) {
            // Someone joined the room..
            // Did we join a new room?
            if(player is Sharpenguin.Game.Player.MyPlayer) {
                // If so, try to find the player we were following..
                if(following != null) {
                    try {
                        IEnumerable<Sharpenguin.Game.Player.Player> players = me.Connection.Room.Players.Where(p => p.Id == following.Id );
                        if(players.Count() != 0) Follow(players.First());
                    }catch{
                        System.Console.WriteLine("Could not find who we wish to follow.");
                    }
                }
                // And listen to all of the players talking, we need to know if they say "follow me"!
                foreach(Sharpenguin.Game.Player.Player inside in me.Connection.Room.Players)
                    inside.OnSpeak += HandleMessage;
            }else{
                // Someone else joined the room, we need to listen to them talking.
                player.OnSpeak += HandleMessage;
                // This shouldn't really happen (unless the player logs in again), but they could be the player we're following, we must follow them!
                if(following != null && player.Id == following.Id)
                    Follow(player);
            }
        }

        public void HandleLeaveRoom(Sharpenguin.Game.Player.Player player) {
            // Someone left the room, was it the player we're following? If so, we must find them!
            if(player == following)
                me.Buddies.Find(following.Id);
        }

        // Begins following the given player..
        private void Follow(Sharpenguin.Game.Player.Player player) {
            if(following != null) {
                // Detatch event handlers from the previous player.
                following.Position.OnMove -= HandleMove;
                following.OnEmoticon -= HandleEmote;
            }
            // Attatch event handlers to the new player.
            player.Position.OnMove += HandleMove;
            player.OnEmoticon += HandleEmote;
            // Go to the player.
            me.Position.Set(player.Position.X, player.Position.Y);
            // Set the player we're now following.
            following = player;
        }

        public void HandleError(Sharpenguin.PenguinConnection connection, int error) {
            // We got an error! Output the message.
            System.Console.WriteLine(Sharpenguin.Configuration.Configuration.Errors[error].Message);
        }

        public void HandleDisconnect(Sharpenguin.PenguinConnection connection) {
            // We disconnected, handy to know.
            System.Console.WriteLine("Disconnected!");
        }

        public void BuddyFindHandler(Sharpenguin.Game.Player.MyPlayer me, int id, int room) {
            // If it's who we're trying to follow and they're not offline, go to them!
            if(id == following.Id && room != -1)
                me.Enter(room);
        }
    }
}
