using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Infrastructure;
using UnityEngine;

namespace EnemyLogic
{
    public class GetNearbyObject<TNearbyObject, TSharedNearbyObject> : Conditional where TNearbyObject : Component where TSharedNearbyObject : SharedVariable<TNearbyObject>
    {
        public TSharedNearbyObject NearbyObjectReturn;

        private readonly Collider[] _overlapColliders = new Collider[Constants.AmountNonAllocColliders];

        public override TaskStatus OnUpdate()
        {
            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, Constants.EnemyRadiusCheckTarget, _overlapColliders);
            TNearbyObject nearbyObject = null;
            float nearbyObjectDistance = float.PositiveInfinity;
            
            for (int colliderIterator = 0; colliderIterator < overlapCount; colliderIterator++)
            {
                Collider overlapCollider = _overlapColliders[colliderIterator];
                
                if (overlapCollider.gameObject == gameObject)
                    continue;

                if (overlapCollider.TryGetComponent(out TNearbyObject detectedObject))
                {
                    float distance = Vector3.Distance(transform.position, detectedObject.transform.position);
                    
                    if (distance < nearbyObjectDistance)
                    {
                        nearbyObject = detectedObject;
                        nearbyObjectDistance = distance;
                    }
                }
            }

            NearbyObjectReturn.Value = nearbyObject;
            return nearbyObject != null ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}