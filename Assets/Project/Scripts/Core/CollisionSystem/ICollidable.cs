namespace Project.Scripts.Core.CollisionSystem
{
    public interface ICollidable
    {
        CollisionHandlePriority CollisionPriority { get; }
    }

    public enum CollisionHandlePriority
    {
        None = 0,
        Low = 1,
        High = 2
    }
}