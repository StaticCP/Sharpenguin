using System;

namespace Sharpenguin {
    /**
     * Penguin base exception class.
     */
    public class PenguinException : System.Exception {
        public PenguinException() : base() { }
        public PenguinException(string strMessage) : base(strMessage) { }
        public PenguinException(string strMessage, PenguinException objException) : base(strMessage, objException) { }
    }
}

