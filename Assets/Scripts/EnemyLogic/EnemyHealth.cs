using System;

namespace EnemyLogic
{
    public class EnemyHealth
    {
        private readonly int _maxHealth;
        private int _currentHealth;

        public event Action Died;

        public EnemyHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void ApplyDamage(int damage)
        {
            if (_currentHealth>= damage) 
                Spend(damage);

            if (_currentHealth <= 0) 
                Died?.Invoke();
        }

        public void ReturnMaxHealth() => 
            _currentHealth = _maxHealth;

        private void Spend(int damage) => 
            _currentHealth -= damage;
    }
}