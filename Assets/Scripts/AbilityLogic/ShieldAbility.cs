using UnityEngine;

namespace AbilityLogic
{
    public class ShieldAbility : Ability
    {
        [SerializeField] private Transform _spawnPointBullet;
    
        public override int GetIndexAbility() =>
            (int)IndexAbility.Shield;

        public override void Cast()
        {
            throw new System.NotImplementedException();
        }
    }
}