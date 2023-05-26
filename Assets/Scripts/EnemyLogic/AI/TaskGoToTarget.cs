using BehaviourTree;
using UnityEngine;

namespace EnemyLogic.AI
{
    public class TaskGoToTarget : Node
    {
        private Transform _transform;

        public TaskGoToTarget(Transform transform)
        {
            _transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("target");

            if (Vector3.Distance(_transform.position, target.position) > .01f)
                _transform.position = Vector3.MoveTowards(_transform.position, target.position,
                    Constants.EnemySpeed * Time.deltaTime);

            return NodeState.Running;
        }
    }
}