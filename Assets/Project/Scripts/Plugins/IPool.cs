using System;

namespace Project.Scripts.Plugins
{
    public interface IPool<T>
    {
        void PushObject(T obj);
        
        void GetObject(T obj);
        bool TryGetObject(out T obj);
        bool TryGetActiveObject(out T obj);

        void PushObjectsByCondition(Func<T, bool> condition);

        void PushAllObjects();
    }
}
