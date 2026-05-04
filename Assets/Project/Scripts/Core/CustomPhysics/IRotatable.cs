namespace Project.Scripts.Core.CustomPhysics
{
    public interface IRotatable
    {
        void SetRotation(float angle);
        void RotateSmoothly(float deltaTime, float angle);
    }
}