using UnityEngine;
using System.Collections.Generic;
using System;

namespace AHJO { 

    [System.Serializable]
	public class ObjectPool<T> : IObjectPool 
        where T : MonoBehaviour, IPoolable, new () {

        private bool autoExpand;
        private int defaultSize, maxSize, currentSize;

        private T prototype;
        private Queue<T> objectQueue;

        private bool useParent = false;
        private Transform poolObjectParent;

        public IObjectPool GetObjectPool { get { return this; } }

        public ObjectPool (T prototype, int defaultSize, bool autoExpand = false, int maxSize = 0) {
            this.prototype = prototype;
            this.defaultSize = defaultSize;
            this.maxSize = (defaultSize > maxSize) ? defaultSize : maxSize;
            this.autoExpand = autoExpand;

            Init ();
        }

        public void SetPoolParent (Transform parent) {
            useParent = true;
            poolObjectParent = parent;
        }

        void Init () {
            objectQueue = new Queue<T> (defaultSize);
            currentSize = defaultSize;
           
            for (int i = 0; i < defaultSize; i++) {
                NewPoolObject ();
            }          
        }

        void NewPoolObject () {
            tempObject = prototype.CreateNew<T> (this);
            if (tempObject) {
                objectQueue.Enqueue (tempObject);
                if (useParent) {
                    tempObject.transform.SetParent (poolObjectParent);
                }
            }
        }

        T tempObject;
        public T GetObject () {
            if (objectQueue.Count > 0) {
                return (T) objectQueue.Dequeue ();
            } else if (autoExpand && currentSize < maxSize) {
                NewPoolObject ();
                return tempObject;
            }
            return default (T);
        }

        public void ReturnToPool (T poolObject) {
            objectQueue.Enqueue (poolObject);
        }
    }

}
