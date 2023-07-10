using Infrastructure;
using PlayerLogic;
using TurretLogic;
using UnityEngine;

namespace EnemyLogic.AI.States
{
    [RequireComponent(typeof(Enemy))]
    public class SearchTargetState : State
    {
        private readonly Collider[] _overlapColliders = new Collider[16];
        private Enemy _enemy;
        public Vector3 CurrentTarget { get; private set; } = Vector3.zero;

        private void Awake()
        {
            CheckOverlap();
            _enemy = Get<Enemy>();
        }

        private void CheckOverlap()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, Constants.EnemyRadiusCheck, _overlapColliders);

            for (int colliderIterator = 0; colliderIterator < overlapCount; colliderIterator++)
            {
                Collider overlapCollider = _overlapColliders[colliderIterator];

                if (overlapCollider.gameObject.TryGetComponent(out Hero hero))
                {
                    SetTarget(hero.transform.position);
                    Fsm.Enter<AttackState>();
                    return;
                }
                
                if (overlapCollider.gameObject.TryGetComponent(out Turret turret))
                {
                    SetTarget(turret.transform.position);
                    Fsm.Enter<AttackState>();
                    return;
                }
                
                SetTarget(Vector3.zero);
                Fsm.Enter<MovementState>();
            }
        }

        private void SetTarget(Vector3 newTarget)
        {
            CurrentTarget = newTarget;
            _enemy.InjectTarget(CurrentTarget);
        }
    }
}