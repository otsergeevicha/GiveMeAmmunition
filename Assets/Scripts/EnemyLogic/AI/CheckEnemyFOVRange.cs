using BehaviourTree;
using UnityEngine;

namespace EnemyLogic.AI
{
    public class CheckEnemyFOVRange : Node
    {
        private Transform _transform;

        public CheckEnemyFOVRange(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");

            if (target == null)
            {
                Collider[] colliders =
                    Physics.OverlapSphere(_transform.position, Constants.FOVRange, Constants.EnemyLayerMask);

                if (colliders.Length > 0)
                {
                    Parent.Parent.SetData("target", colliders[0].transform);

                    return NodeState.Success;
                }

                return NodeState.Success;
            }

            return NodeState.Success;
        }
    }
}