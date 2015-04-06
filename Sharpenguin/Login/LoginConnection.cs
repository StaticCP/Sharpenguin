using System;

namespace Sharpenguin.Login {
    public class LoginConnection : PenguinConnection {
        public event LoginHandler OnLogin; //< Event for handling login success.

        public LoginConnection(string username, string password) : base(username, password) {
        }

        /**
         * Disconnects from the login server and calls the onLogin event.
         */
        protected void LoginFinished() {
            isLogin = false;
            Disconnect();
            if(OnLogin != null) OnLogin();
        }
    }
}

