using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Go To Target Location Node", menuName = "Scriptable Objects/Behavior Tree Nodes/Go To Target Location Node", order = 1)]
    public class GoToTargetLocationNode : Node
    {
        private bool _targetSet = false;

        public override NodeState Evalue(Blackboard blackboard)
        {
            if (!_targetSet && blackboard.TargetLocation != null)
            {
                blackboard.EnemyNavmeshAgent.SetDestination(blackboard.TargetLocation.position);
                _targetSet = true;
            }

            if (blackboard.TargetLocation != null)
            {
                _nodeState = NodeState.Failure;
                return _nodeState;
            }

            if (_nodeState != NodeState.Success
                && _targetSet
                && Vector3.Distance(blackboard.EnemyNavmeshAgent.transform.position, blackboard.TargetLocation.position) <= 0.5f)
            {
                _targetSet = false;
                blackboard.TargetLocation = null;
                _nodeState = NodeState.Success;
                return _nodeState;
            }
            else
            {
                _nodeState = NodeState.Running;
                return _nodeState;
            }


        }
    }
}