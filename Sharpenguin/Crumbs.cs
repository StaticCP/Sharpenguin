/**
 * @file Crumbs
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * Sharpenguin Data representation.
 */
namespace Sharpenguin.Data {
    using System.IO;
    using System.Net;
    using System.Xml;
    using System.Collections.Generic;

    /**
     * Crumbs class for containing all the crumbs required for the game. I may add some more soon.
     */
    public class CPCrumbs {
        private Dictionary<string, CrumbCollection> penguinCrumbs; //< Dictionary of all of the crumb collections.
        private const string crumbsPath = "sp_config/"; //< Location of crumbs.
        private const string crumbsUrl  = "http://sphen.clubpenguinphp.info/configs/"; //< Web location of crumbs.

        //! Gets the room crumbs.
        public CrumbCollection Rooms {
            get { return penguinCrumbs["rooms"]; }
        }
        //! Gets the server crumbs.
        public CrumbCollection Servers {
            get { return penguinCrumbs["servers"]; }
        }
        //! Gets the item crumbs.
        public CrumbCollection Items {
            get { return penguinCrumbs["items"]; }
        }
        //! Gets the error crumbs.
        public CrumbCollection Errors {
            get { return penguinCrumbs["errors"]; }
        }
        //! Gets the joke crumbs.
        public CrumbCollection Jokes {
            get { return penguinCrumbs["jokes"]; }
        }
        //! Gets the emoticon crumbs.
        public CrumbCollection Emoticons {
            get { return penguinCrumbs["emoticons"]; }
        }
        //! Gets the safechat crumbs.
        public CrumbCollection SafeMessages {
            get { return penguinCrumbs["safechat"]; }
        }
        //! Gets the login server crumbs.
        public CrumbCollection LoginServers {
            get { return penguinCrumbs["loginservers"]; }
        }

        /**
         * Constructor of CPCrumbs, loads the crumb files for the game.
         */
        public CPCrumbs() {
            if(Directory.Exists(crumbsPath) == false) Directory.CreateDirectory(crumbsPath);
            penguinCrumbs = new Dictionary<string, CrumbCollection>();
            loadCrumbs("rooms");
            loadCrumbs("servers");
            loadCrumbs("items");
            loadCrumbs("errors");
            loadCrumbs("jokes");
            loadCrumbs("emoticons");
            loadCrumbs("safechat");
            loadCrumbs("loginservers");
        }

        /**
         * Loads crumbs from XML files which are located at http://penguin.clubpenguinphp.info/configs/, downloads a copy for future runs.
         *
         * @param strCrumb
         *   Name of the crumb to load. Name must be equivalent to the file name.
         */

        private void loadCrumbs(string strCrumb) {
            try {
                if(File.Exists(crumbsPath + strCrumb + ".xml") == false) {
                        System.Console.WriteLine("Downloading crumbs for " + strCrumb + "...");
                        WebClient wcDownload = new WebClient();
                        wcDownload.DownloadFile(crumbsUrl + strCrumb + ".xml", crumbsPath + strCrumb + ".xml");
                }
                XmlDocument objXDoc = loadXml(crumbsPath + strCrumb + ".xml");
                penguinCrumbs.Add(objXDoc.DocumentElement.Name, new CrumbCollection(objXDoc.DocumentElement, objXDoc.DocumentElement.Name));
            }catch{
                System.Console.WriteLine("Could not load cumbs for " + strCrumb + "!");
                File.Delete(crumbsPath + strCrumb + ".xml");
                System.Environment.Exit(0);
            }
        }

        /**
         * Loads an XML file into an XmlDocument object.
         *
         * @return
         *   XmlDocument of the parsed XML
         */
        private XmlDocument loadXml(string strFile) {
            XmlDocument objXDoc = new XmlDocument();
            objXDoc.Load(strFile);
            return objXDoc;
        }

    }

    /**
     * An object to store data about crumb variables.
     */
    public class CrumbCollection {
        private string strName = ""; //< Name of crumbs.
        private string strSingular = ""; //< Crumb name without the trailing "s", if any, for use in exceptions.
        private Dictionary<int, Dictionary<string, string>> dicCrumbs; //< Dictionary to store crumb information.

        /**
         * Constructor of CrumbCollection, creates crumb data dictionary
         */
        public CrumbCollection(XmlElement xeXml, string strCrumb) {
            strName = strCrumb;
            strSingular = strName.EndsWith("s") ? strName.Remove(strName.Length -1) : strName;
            dicCrumbs = new Dictionary<int, Dictionary<string, string>>();
            parseXml(xeXml);
        }

        /*
         * Puts an XmlElement into the crumbs data dictionary.
         *
         * @param xeXml
         *   The XmlElement to put into the crumbs data dictionary.
         *
         */
        private void parseXml(XmlElement xeXml) {
            int intId;
            for(int intIndex = 0; xeXml.ChildNodes.Count > intIndex; intIndex++) {
                intId = int.Parse(xeXml.ChildNodes[intIndex].Attributes["id"].Value);
                dicCrumbs.Add(intId, new Dictionary<string, string>());
                foreach(XmlAttribute objAttr in xeXml.ChildNodes[intIndex].Attributes) {
                    dicCrumbs[intId].Add((string) objAttr.Name, (string) objAttr.Value);
                }
            }
        }

        /**
         * Gets a crumb entry by its id.
         *
         * @param intId
         *   The id of the crumb you wish to obtain.
         *
         * @return the crumb according to its id.
         */
        public Dictionary<string, string> GetById(int intId) {
            Dictionary<string, string> dicCrumb;
            if(dicCrumbs.TryGetValue(intId, out dicCrumb)) {
                return dicCrumb;
            }else{
                throw new Exceptions.NonExistantCrumbException(strSingular + " with id " + intId.ToString() + " does not exist.");
            }
        }

        /**
         * Gets an attribute of a crumb by the crumb's id.
         *
         * @param intId
         *   The id of the crumb you with to obtain an attribute from.
         * @param strAttribute
         *   The attribute to get from the crumb.
         *
         * @return
         *   Value of the specified attribute of the crumb entry.
         */
        public string GetAttributeById(int intId, string strAttribute) {
            bool blnId = dicCrumbs.ContainsKey(intId);
            if(blnId) {
                if(dicCrumbs[intId].ContainsKey(strAttribute)) {
                    return dicCrumbs[intId][strAttribute];
                }else{
                    throw new Exceptions.NonExistantCrumbException("Attribute " + strAttribute + " does not exist in " + strName + ".");
                }
            }else {
                throw new Exceptions.NonExistantCrumbException(strSingular + " with id " + intId.ToString() + " does not exist.");
            }
        }

        /**
         * Gets the id of a crumb by an attribute.
         *
         * @param strAttribute
         *   The name of the attribute.
         * @param strValue
         *   The value of the attribute.
         *
         * @return
         *   The id of the crumb entry.
         */
        public int GetIdByAttribute(string strAttribute, string strValue) {
            foreach(Dictionary<string, string> dicCrumb in dicCrumbs.Values) {
                if(dicCrumb[strAttribute].ToLower() == strValue.ToLower()) return int.Parse(dicCrumb["id"]);
            }
            throw new Exceptions.NonExistantCrumbException(strSingular + " with " + strAttribute + " \"" + strValue + "\" does not exist!");
        }

        /**
         * Gets a crumb entry by an attribute.
         *
         * @param strAttribute
         *   The name of the attribute.
         * @param strValue
         *   The value of the attribute.
         *
         * @return
         *   The crumb according to its attribute.
         */
        public Dictionary<string, string> GetByAttribute(string strAttribute, string strValue) {
            foreach(Dictionary<string, string> dicCrumb in dicCrumbs.Values) {
                if(dicCrumb[strAttribute].ToLower() == strValue.ToLower()) return dicCrumb;
            }
            throw new Exceptions.NonExistantCrumbException(strSingular + " with " + strAttribute + " \"" + strValue + "\" does not exist!");
        }

        /**
         * Checks whether a crumb with the specified id exists.
         *
         * @param intId
         *   The id to check.
         *
         * @return
         *   TRUE if the crumb exists, FALSE if it does not.
         */
        public bool ExistsById(int intId) {
            return dicCrumbs.ContainsKey(intId);
        }


        /**
         * Gets a pseudo-random id from the crumbs.
         */
        public int GetRandom() {
            List<int> lstKeys = new List<int>(dicCrumbs.Keys);
            System.Random randGen = new System.Random();
            return lstKeys[randGen.Next(dicCrumbs.Count)];
        }

        /**
         * Checks whether a crumb with the specified attribute and value exists.
         *
         * @param strAttribute
         *   The name of the attribute.
         * @param strValue
         *   The value of the attribute.
         *
         * @return
         *   TRUE if the crumb exists, FALSE if it does not.
         */
        public bool ExistsByAttribute(string strAttribute, string strValue) {
            foreach(Dictionary<string, string> dicCrumb in dicCrumbs.Values) {
                if(dicCrumb[strAttribute].ToLower() == strValue.ToLower()) return true;
            }
            return false;
        }

    }
}
