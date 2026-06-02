namespace Project.Scripts.Plugins
{
    public interface IPool<T>
    {
        bool TryGetObject(out T obj);
        bool TryGetActiveObject(out T obj);

        void PushObject(T obj);

        void PushAllObjects();

        int ShowCountPool();
    }
}
