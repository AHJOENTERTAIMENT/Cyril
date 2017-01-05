using UnityEngine;

namespace AHJO {

    public abstract class SingletonBehaviour<T> : SingletonBehaviourBase
       where T : SingletonBehaviour<T> {

        private static T _instance;

        /// <summary>
        /// As this should not be null, null checking should  not be nesesssary in most cases.
        /// </summary>
        public static T Instance {
            get {
                return _instance;
            }
            protected set {
                _instance = value;
            }
        }

        /// <summary>
        /// Awake is used for singleton set up and should not be used. 
        /// Use CyrilAwake instead.
        /// </summary>
        protected sealed override void Awake () {
            if (_instance) {
                if (_instance.persisting) {
                    Destroy (this.gameObject);
                    return;
                } else {
                    Destroy (_instance.gameObject);
                    _instance = (T) this;
                }
            } else {
                _instance = (T) this;
            }

            if (_instance.persisting) {
                if (this.transform.parent == null) {
                    DontDestroyOnLoad (this.gameObject);
                }
            }

            LogCreation ();
            SingletonAwake ();
        }


        protected override void OnDestroy () {
            _instance = null;
            OnSingletonDestroyed ();
        }
    }

    public abstract class SingletonBehaviourBase : MonoBehaviour {

        [Header ("Singleton Behaviour")]
        [SerializeField]
        [ShowOnly]
        protected bool isActive;

        // Determines if new instances replace old ones
        [SerializeField]
        protected bool persisting;
        [SerializeField]
        [MaskField]
        protected LogLevel logLevel;

        // Get the reference to the instance. 
        // This should not be null as long as there is a Script in the scene with object inheritin form SingletonBehaviour..
        private bool _markForDestroy;

        protected readonly string typeName;
        protected readonly string logName;

        protected SingletonBehaviourBase () {
            typeName = this.GetType ().Name;
            logName = "[" + typeName + "]: ";
        }

        protected void LogCreation () {
            if (logLevel == LogLevel.INFO) {
                Debug.Log ("Singleton Created for " + typeName);
            }
        }

        protected void SetPersisting (bool flag) {
            persisting = flag;
        }

        protected virtual void OnDestroy () {
            OnSingletonDestroyed ();
        }

        protected virtual void SingletonAwake () {
        }

        protected virtual void OnSingletonDestroyed () {
        }

        protected abstract void Awake ();
    }
	
}
