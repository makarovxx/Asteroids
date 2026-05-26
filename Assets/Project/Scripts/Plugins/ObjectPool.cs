using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Plugins
{
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, ICreatable
    {
        private readonly List<T> _objects;

        public ObjectPool(ICreator<T> creator, int count, Transform container = null)
        {
            _objects = new List<T>(count);

            Allocate(creator, count, container);
        }

        private void Allocate(ICreator<T> creator, int count, Transform container)
        {
            for (int i = 0; i < count; i++)
            {
                T obj = creator.Create();

                if (container != null)
                    obj.transform.SetParent(container);

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
    }
}