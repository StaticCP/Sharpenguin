/**
 * @file Logger
 * @author Static
 * @url http://clubpenguinphp.info/
 * @license http://www.gnu.org/copyleft/lesser.html
 */

/**
 * Sharpenguin STDIN output.
 */
namespace Sharpenguin.Out {
    using Console = System.Console;
    using ConsoleColor = System.ConsoleColor;

    /**
     * Static class for handling logging in penguiniscidae.
     */
    public static class Logger {
        /**
        * Enum of log levels for the writeOutput method.
        */
        public enum LogLevel {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        /**
         * Determines the string of the LogLevel
         *
         * @param outputLevel
         *    The LogLevel enumeration value.
         *
         * @return
         *   The string of the LogLevel
         */
        private static string LogLevelString(LogLevel outputLevel) {
            switch(outputLevel) {
                case LogLevel.Trace:
                    return "Trace";
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Info:
                    return "Info";
                case LogLevel.Warn:
                    return "Warn";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Fatal:
                    return "Fatal";
                default:
                    return "?";
            }
        }


        /**
         * Writes to stdout.
         *
         * Outputs text in the following format; "[TYPE][TIME] >> MESSAGE"
         *
         * @param strOutput
         *   The message to output.
         *
         * @param outputLevel
         *   The log level of the output
         */
        public static void WriteOutput(string strOutput, LogLevel outputLevel = LogLevel.Info) {
            string strType = LogLevelString(outputLevel);
            string strTime = System.DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine("[{0}][{1}] >> {2}", strType, strTime, strOutput);
            if(strType == "Fatal") System.Environment.Exit(0);
        }

    }

}
