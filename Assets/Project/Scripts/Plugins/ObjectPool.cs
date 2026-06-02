using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Plugins
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly List<T> _objects;
        private readonly ICreator<T> _creator;
        private readonly Transform _container;

        public ObjectPool(ICreator<T> creator, int count, Transform container = null)
        {
            _objects = new List<T>(count);
            _creator = creator;
            _container = container;
            Allocate(count);
        }

        private void Allocate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T obj = _creator.Create();

                if (_container != null)
                    obj.transform.SetParent(_container);

                obj.gameObject.SetActive(false);

                _objects.Add(obj);
            }
        }

        public bool TryGetObject(out T obj)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].gameObject.activeSelf)
                    continue;

                obj = _objects[i];

                obj.gameObject.SetActive(true);

                return true;
            }

            obj = null;

            return false;
        }

        public bool TryGetActiveObject(out T obj)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (!_objects[i].gameObject.activeSelf)
                    continue;

                obj = _objects[i];

                return true;
            }

            obj = null;

            return false;
        }

        public void PushObject(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        public void PushAllObjects()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                PushObject(_objects[i]);
            }
        }

        public int ShowCountPool()
        {
            return _objects.Count;
        }
    }
}
