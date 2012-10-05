/**
 * @file PenguinExceptions
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * Sharpenguin Exceptions.
 */
namespace Sharpenguin.Exceptions {

    /**
     * Penguin base exception class.
     */
    public class PenguinException : System.Exception {
        public PenguinException() : base() { }
        public PenguinException(string strMessage) : base(strMessage) { }
        public PenguinException(string strMessage, PenguinException objException) : base(strMessage, objException) { }
    }

    /**
     * Exception for when the configuration file is invalid for whatever reason.
     */
    public class InvalidConfigException : PenguinException {
        public InvalidConfigException() : base() { }
        public InvalidConfigException(string strMessage) : base(strMessage) { }
        public InvalidConfigException(string strMessage, InvalidConfigException objException) : base(strMessage, objException) { }
    }

    /**
     * Exception for when the XtParser failed to parse a string.
     */
    public class InvalidXtException : PenguinException {
        public InvalidXtException() : base() { }
        public InvalidXtException(string strMessage) : base(strMessage) { }
        public InvalidXtException(string strMessage, InvalidXtException objException) : base(strMessage, objException) { }
    }

    /**
     * Exception for when a crumb which does not exist was attempted to be accessed.
     */
    public class NonExistantCrumbException : PenguinException {
        public NonExistantCrumbException() : base() { }
        public NonExistantCrumbException(string strMessage) : base(strMessage) { }
        public NonExistantCrumbException(string strMessage, NonExistantCrumbException objException) : base(strMessage, objException) { }
    }

    /**
     * Exception for when there was an error in parsing the phrase chat message.
     */
    public class PhraseChatErrorException : PenguinException {
        public PhraseChatErrorException() : base() { }
        public PhraseChatErrorException(string strMessage) : base(strMessage) { }
        public PhraseChatErrorException(string strMessage, PhraseChatErrorException objException) : base(strMessage, objException) { }
    }

    /**
     * Exception for when the client tried to join a game server before login.
     */
    public class EarlyJoinException : PenguinException {
        public EarlyJoinException() : base() { }
        public EarlyJoinException(string strMessage) : base(strMessage) { }
        public EarlyJoinException(string strMessage, EarlyJoinException objException) : base(strMessage, objException) { }
    }

    /**
     * Exception for when a player is not in the room, but the client tried to get their player object.
     */
    public class NonExistantPlayerException : PenguinException {
        public NonExistantPlayerException() : base() { }
        public NonExistantPlayerException(string strMessage) : base(strMessage) { }
        public NonExistantPlayerException(string strMessage, NonExistantPlayerException objException) : base(strMessage, objException) { }
    }

    /**
     * Exception for when the server returns that the given API version is incorrect.
     */
    public class InvalidAPIException : PenguinException {
        public InvalidAPIException() : base() { }
        public InvalidAPIException(string strMessage) : base(strMessage) { }
        public InvalidAPIException(string strMessage, InvalidAPIException objException) : base(strMessage, objException) { }
    }

}
