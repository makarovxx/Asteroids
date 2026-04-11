namespace Project.Scripts.Core.Health
{
    public class Health
    {
        private int _currentHealth;
        private int _maxHealth;

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
        }
    }
}