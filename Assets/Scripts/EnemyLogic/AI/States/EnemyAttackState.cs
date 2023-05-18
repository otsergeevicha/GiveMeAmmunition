using UnityEngine;

namespace EnemyLogic.AI.States
{
    public class EnemyAttackState : State
    {
        private Transform _target;
        private Animator _animator;
        private int _damage;
        private Transform _transform;

        public EnemyAttackState(Transform target, Animator animator, int damage, Transform transform)
        {
            _transform = transform;
            _damage = damage;
            _target = target;
            _animator = animator;
        }

        public override void Enable() =>
            _animator.SetBool(Constants.EnemyAttackHash, true);

        public override void Disable() =>
            _animator.SetBool(Constants.EnemyAttackHash, false);

        private void OnStartAttack() =>
            _transform.LookAt(_target);

        private void OnEndAttack() => 
            _target.transform.GetComponentInParent<IHealth>().TakeDamage(_damage);
    }
}