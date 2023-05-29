using UnityEngine;

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