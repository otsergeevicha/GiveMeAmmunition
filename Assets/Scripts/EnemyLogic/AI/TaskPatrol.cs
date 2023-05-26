using BehaviourTree;
using UnityEngine;

namespace EnemyLogic.AI
{
    public class TaskPatrol : Node
    {
        private Transform _transform;
        private Transform[] _wayPoints;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f;
        private float _waitCounter = 0f;
        private bool _waiting;

        public TaskPatrol(Transform transform, Transform[] wayPoints)
        {
            _wayPoints = wayPoints;
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;

                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                }
                else
                {
                    Transform wp = _wayPoints[_currentWaypointIndex];

                    if (Vector3.Distance(_transform.position, wp.position) < .01f)
                    {
                        _transform.position = wp.position;
                        _waitCounter = 0f;
                        _waiting = true;

                        _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
                    }
                    else
                    {
                        _transform.position = Vector3.MoveTowards(_transform.position, wp.position,
                            Constants.EnemySpeed * Time.deltaTime);
                        _transform.LookAt(wp.position);
                    }
                }
            }

            State = NodeState.Running;
            return State;
        }
    }
}