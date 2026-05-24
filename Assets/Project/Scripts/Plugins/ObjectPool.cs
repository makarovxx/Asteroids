using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Plugins
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, ICreatable
    {
        private readonly int _maxInstances;
        private readonly IFactory<T> _factory;
        private readonly List<T> _pooledObjects;

        public ObjectPool(IFactory<T> factory, int maxInstances, Transform container)
        {
            _factory = factory;
            _maxInstances = maxInstances;
            _pooledObjects = new List<T>();

            AllocatePool(container);
        }

        private void AllocatePool(Transform container = null)
        {
            for (int i = 0; i < _maxInstances; i++)
            {
                T obj = _factory.Create();
                if(container) obj.transform.SetParent(container);
                
                PushObject(obj);
                _pooledObjects.Add(obj);
            }
        }

        public void PushObjectsByCondition(Func<T, bool> condition)
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                T obj = _pooledObjects[i];

                if (!obj.gameObject.activeSelf)
                    continue;

                if (condition(obj))
                    PushObject(obj);
            }
        }

        public void PushAllObjects() => _pooledObjects.ForEach(PushObject);

        public bool TryGetObjects(int count,out List<T> objects)
        {
            objects = _pooledObjects.Where(obj => !obj.gameObject.activeSelf).Take(count).ToList();
            if (objects.Count < count)
                return false;

            objects.ForEach(GetObject);
            return true;
        }

        public bool TryGetObject(out T obj)
        {
            obj = _pooledObjects.FirstOrDefault(item => item.gameObject.activeSelf == false);
            if(obj)
                obj.gameObject.SetActive(true);

            return obj;
        }

        public bool TryGetActiveObject(out T obj)
        {
            obj = _pooledObjects.FirstOrDefault(item => item.gameObject.activeSelf);
            return obj;
        }

        public void PushObject(T obj) => obj.gameObject.SetActive(false);
        
        public void GetObject(T obj) => obj.gameObject.SetActive(true);
    }
}
