namespace Project.Scripts.Plugins
{
    public interface IFactory<out T> where T : ICreatable
    {
        public T Create();
    }
}