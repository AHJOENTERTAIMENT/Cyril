
namespace AHJO {

    [System.Flags]
    public enum LogLevel {
        WARNING = 1,
        ERROR = 2,
        INFO = 4
    }

    public static class LogLevelExtensions {

        public static bool HasFlag (this LogLevel logLevel, LogLevel flag) {
            return (logLevel & flag) == flag;
        }

    }

}