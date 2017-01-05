namespace AHJO {

    public abstract class Singleton<T> where T : class, new() {

        // Determines if new instances replace the current one
        protected bool overwritable = true;

        protected LogLevel logLevel;
        public LogLevel LogLevel { get { return logLevel; } }

        // Defining the instance with check for it already existing
        private static T _instance = _instance ?? new T ();
        public static T Instance {
            get {
                return _instance;
            }
            protected set {
                _instance = value;
            }
        }

        protected Singleton () {
            // TODO: Register this for at clearing at Application Exit.
        }

        protected void ClearInstance () {
            _instance = null;
        }

    }

}
