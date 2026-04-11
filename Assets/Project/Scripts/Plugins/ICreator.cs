namespace Project.Scripts.Plugins
{
    public interface ICreator<T> where T : ICreatable
    {
        public T Create();
    }
}