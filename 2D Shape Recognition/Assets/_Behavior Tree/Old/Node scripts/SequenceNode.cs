using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Sequence Node", menuName = "Scriptable Objects/Behavior Tree Nodes/Sequence Node", order = 1)]
    public class SequenceNode : Node
    {
        public override NodeState Evalue(Blackboard blackboard)
        {
            bool anyChildrenRunning = false;

            foreach (var node in _children)
            {
                switch (node.Evalue(blackboard))
                {
                    case NodeState.Failure:
                        _nodeState = NodeState.Failure;
                        return _nodeState;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        anyChildrenRunning = true;
                        continue;
                    default:
                        _nodeState = NodeState.Success;
                        return _nodeState;
                }
            }

            _nodeState = anyChildrenRunning ? NodeState.Running : NodeState.Success;

            return _nodeState;
        }

    }
}