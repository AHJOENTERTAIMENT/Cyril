using UnityEngine;
using System.Collections.Generic;

namespace AHJO   { 

	public interface IPoolable {
        T CreateNew<T> (IObjectPool objectPool);
    }
	
    public interface IObjectPool {
        IObjectPool GetObjectPool { get; }
    }
}
