using BehaviorDesigner.Runtime.Tasks;
using Infrastructure;
using PlayerLogic;
using TurretLogic;
using UnityEngine;

namespace EnemyLogic.EnemyAI.AttackState
{
    public class BotAttack : Action
    {
        private readonly Collider[] _overlapColliders = new Collider[Constants.AmountNonAllocColliders];
        private Animator _animator;

        public override void OnAwake()=> 
            _animator = GetComponent<Animator>();

        public override void OnStart() => 
            _animator.SetTrigger(Constants.EnemyAttack);

        private void OnAttack()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, Constants.EnemyRadiusCheckAttack, _overlapColliders);
            
            for (int colliderIterator = 0; colliderIterator < overlapCount; colliderIterator++)
            {
                Collider overlapCollider = _overlapColliders[colliderIterator];
                
                if (overlapCollider.gameObject == gameObject)
                    continue;

                if (overlapCollider.TryGetComponent(out Hero hero)) 
                    hero.TakeDamage(Constants.EnemyDamage);

                if (overlapCollider.TryGetComponent(out Turret turret)) 
                    turret.TakeDamage(Constants.EnemyDamage);
            }
        }
    }
}