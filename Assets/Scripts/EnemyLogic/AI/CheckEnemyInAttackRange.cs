using BehaviourTree;
using UnityEngine;

namespace EnemyLogic.AI
{
    public class CheckEnemyInAttackRange : Node
    {
        private Transform _transform;
        private Animator _animator;

        public CheckEnemyInAttackRange(Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");

            if (target == null)
                return NodeState.Failure;

            Transform newtarget = (Transform)target;

            if (Vector3.Distance(_transform.position, newtarget.position) <= Constants.AttackRange)
                return NodeState.Success;

            return NodeState.Failure;
        }
    }
}