namespace Project.Scripts.Core.TickableSystem
{
    public interface IBehaviourTickable
    {
        void Tick(float deltaTime);
    }

    public interface IPhysicsTickable
    {
        void Tick(float fixedDeltaTime);
    }
}