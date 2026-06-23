namespace Project.Scripts.Gameplay.Entities.Projectile
{
    public sealed class LaserProvider
    {
        public Laser Target { get; private set; }

        public void Init(Laser laser)
        {
            Target = laser;
        }
    }
}
