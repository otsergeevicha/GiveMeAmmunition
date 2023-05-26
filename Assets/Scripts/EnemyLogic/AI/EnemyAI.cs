using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using Tree = BehaviourTree.Tree;

namespace EnemyLogic.AI
{
    public class EnemyAI : Tree
    {
        [SerializeField] private Transform[] _wayPoints;

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckEnemyInAttackRange(transform),
                    new TaskAttack(transform)
                }),
                new Sequence(new List<Node>
                {
                    new CheckEnemyFOVRange(transform),
                    new TaskGoToTarget(transform)
                }),
                new TaskPatrol(transform, _wayPoints)
            });
            return root;
        }
    }
}