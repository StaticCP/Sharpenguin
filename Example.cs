namespace SharpenguinExample {
    using Threads = System.Threading;

    class Exmple {

        static void Main() {
            // Creates a new ExamplePenguin class which, in this instance, is a wrapper for Sharpenguin.
            ExamplePenguin myPenguin = new ExamplePenguin();
            // Creates a new ManualResetEvent. This is used to pause the thread so that the program doesn't exit.
            Threads.ManualResetEvent penguinDone = new Threads.ManualResetEvent(false);
            // Asks for the user's username.
            System.Console.Write("Enter your username: ");
            string penguinUsername = System.Console.ReadLine();
            // Asks for the user's password.
            System.Console.Write("Enter your password: ");
            string penguinPassword = System.Console.ReadLine();
            // Asks for the server to join.
            System.Console.Write("Enter the name of the server to join: ");
            string joinServer = System.Console.ReadLine();
            // Connects the penguin.
            myPenguin.Connect(penguinUsername, penguinPassword, joinServer);
            // Pauses the thread. [penguinDone.Set() would resume the thread, and so would exit the program.]
            penguinDone.Reset();
            penguinDone.WaitOne();
        }

    }

    class ExamplePenguin {
        // Sharpenguin connector.
        private Sharpenguin.Penguin examplePenguin;
        // Server name.
        private string serverName;

        public ExamplePenguin() {
            // Creates a new Penguin.
            examplePenguin = new Sharpenguin.Penguin();
            // Loads all of the handlers we will be using.
            LoadHandlers();
        }
        
        public void Connect(string penguinUsername, string penguinPassword, string joinServer) {
            // Checks if the specified server exists.
            if(examplePenguin.Crumbs.Servers.ExistsByAttribute("name", joinServer)) {
                // If so, continue.
                serverName = joinServer;
                examplePenguin.Login(penguinUsername, penguinPassword);
            }else{
                // Otherwise, display an error.
                Sharpenguin.Out.Logger.WriteOutput("Server with the name \"" + joinServer + "\" does not exist!", Sharpenguin.Out.Logger.LogLevel.Error);
            }
        }

        private void LoadHandlers() {
            // Sets the login handler. This is called when we have successfully authenticated to the login server and have received our login key.
            examplePenguin.onLogin = LoginHandler;
            // Sets the join handler. This is called when we have successfully authenticated to the game server with our login key.
            examplePenguin.onJoin = JoinHandler;
            // Sets the error handler. This is called when we receive an error.
            examplePenguin.onError = ErrorHandler;
            // Sets the disconnect handler. This is called when we are disconnected from a server.
            examplePenguin.onDisconnect = DisconnectHandler;
            // Sets the connection failure handler. This is called if we are unable to connect to a server.
            examplePenguin.onConnectionFailure = ConnectFailHandler;
            // Gives the player a handler for when they join a room. In this case, the handler is "jr".
            examplePenguin.Handler.Add("jr", JoinRoomHandler);
        }

        public void LoginHandler() {
            // Output a message stating we have authenticated to the login server.
            Sharpenguin.Out.Logger.WriteOutput("Player logged in!");
            examplePenguin.Join(serverName);
        }
        
        public void JoinHandler() {
            // Output a message stating we have authenticated to the game server.
            Sharpenguin.Out.Logger.WriteOutput("Player has joined the server \"" + serverName + "\"!");
        }
        
        public void ErrorHandler(int errorId) {
            // See if error exists.
            if(examplePenguin.Crumbs.Errors.ExistsById(errorId)) {
                // If the error exists, output the error message.
                Sharpenguin.Out.Logger.WriteOutput("An error has occurred [" + errorId.ToString() + "]: " + examplePenguin.Crumbs.Errors.GetAttributeById(errorId, "message"), Sharpenguin.Out.Logger.LogLevel.Error);
            }else{
                // Otherwise, give the ID of the error.
                Sharpenguin.Out.Logger.WriteOutput("An unknown error has occurred with ID of " + errorId.ToString() + ".", Sharpenguin.Out.Logger.LogLevel.Error);
            }
        }
        
        public void DisconnectHandler() {
            // Output a message stating we have disconnected from the server.
            Sharpenguin.Out.Logger.WriteOutput("Disconnected from server.");
        }
        
        public void ConnectFailHandler(string attemptedHost, int serverPort) {
            // Output a message stating we have failed to connect to a server.
            Sharpenguin.Out.Logger.WriteOutput("Could not connect to server " + attemptedHost + ":" + serverPort.ToString() + ".", Sharpenguin.Out.Logger.LogLevel.Error);
        }
        
        private void JoinRoomHandler(Sharpenguin.Data.PenguinPacket receivedPacket) {
            // Output a message when a room is joined.
            Sharpenguin.Out.Logger.WriteOutput("Joined room " + receivedPacket.Xt.Arguments[0] + " [" + examplePenguin.Crumbs.Rooms.GetAttributeById(int.Parse(receivedPacket.Xt.Arguments[0]), "name") + "].");
        }

    }

}
