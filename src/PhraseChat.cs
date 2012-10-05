/**
 * @file PhaseChat
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * Sharpenguin PhraseChat handling.
 */
namespace Sharpenguin.Phrase {
    using System.Collections;
    public delegate void PhraseReceiveCallback(string strData, Data.PenguinPacket receivedPacket, int intStatusCode, string strError);

    /**
     * Phase chat class for handling phase chat messages.
     */
    public static class PhraseChat {
        /**
         * Gets a phrase by it's id.
         *
         * @param strId
         *   The id of the phrase to get.
         *
         * @return
         *   The phrase according to it's id.
         */
        public static string PhraseById(string strId) {
            string strJson = Utils.DownloadString("http://phrasechat.disney.go.com/phrasechatsvc/api/1.1/pen/en/phrase/" + strId);
            Hashtable htlJson = Procurios.Public.JSON.JsonDecode(strJson) as Hashtable;
            if(Successful(htlJson)) {
                return GetPhrase(htlJson);
            }else{
                throw new Exceptions.PhraseChatErrorException(GetError(htlJson));
            }
        }

        public static void BeginPhraseById(PhraseReceiveCallback phraseLoaded, Data.PenguinPacket receivedPacket) {
            System.Threading.Thread asyncPhrase = new System.Threading.Thread(new System.Threading.ThreadStart(delegate { AsyncPhrase(phraseLoaded, receivedPacket); }));
            asyncPhrase.IsBackground = true;
            asyncPhrase.Start();
        }

        private static void AsyncPhrase(PhraseReceiveCallback phraseLoaded, Data.PenguinPacket receivedPacket) {
            try {
                string strJson = Utils.DownloadString("http://phrasechat.disney.go.com/phrasechatsvc/api/1.1/pen/en/phrase/" + receivedPacket.Xt.Arguments[1]);
                Hashtable htlJson = Procurios.Public.JSON.JsonDecode(strJson) as Hashtable;
                if(Successful(htlJson)) {
                    phraseLoaded(GetPhrase(htlJson), receivedPacket, 200, null);
                }else{
                    phraseLoaded(null, receivedPacket, 200, GetError(htlJson));
                }
            }catch(System.Net.WebException webEx){
                phraseLoaded(null, receivedPacket, (int)webEx.Status, null);
            }
        }

        /**
         * Gets a phrase from the parsed JSON hashtable.
         *
         * @param htlJson
         *   The hashtable of the parsed JSON.
         *
         * @return
         *   The phrase from the hashtable.
         */
        private static string GetPhrase(Hashtable htlJson) {
            Hashtable htlResult = htlJson["result"] as Hashtable;
            Hashtable htlData = htlResult["data"] as Hashtable;
            return htlData["phrase"] as string;
        }

        /**
         * Gets whether the request was successful from the parsed JSON hashtable.
         *
         * @param htlJson
         *   The hashtable of the parsed JSON.
         *
         * @return
         *   The success boolean from the hashtable.
         */
        private static bool Successful(Hashtable htlJson) {
            Hashtable htlResult = htlJson["result"] as Hashtable;
            return ((bool) htlResult["success"]);
        }

        /**
         * Gets the error message from the parsed JSON hashtable.
         *
         * @param htlJson
         *   The hashtable of the parsed JSON.
         *
         * @return
         *   The error message from the hashtable.
         */
        private static string GetError(Hashtable htlJson) {
            Hashtable htlResult = htlJson["result"] as Hashtable;
            Hashtable htlError = htlResult["error"] as Hashtable;
            return htlError["message"] as string;
        }

    }

}
