using UnityEngine;
using System.Collections.Generic;
using System;

namespace AHJO.Cyril {

    public interface IManager {

        void ManagerUpdate ();

        T GetManagerInstance<T> () where T : SingletonBehaviourBase;
    }

    public static class IMangerExtensions {

        public static bool Implements<I> (this Type type, I @interface) where I : class{
            Type it = @interface as Type;
            if (it == null || !it.IsInterface) return false;
            return it.IsAssignableFrom (type);
        }
    }
	
}
