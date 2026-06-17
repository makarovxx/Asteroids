namespace Project.Scripts.Gameplay.Entities
{
    public interface ICollidable
    {
        CollisionHandlePriority CollisionPriority { get; }
    }
}