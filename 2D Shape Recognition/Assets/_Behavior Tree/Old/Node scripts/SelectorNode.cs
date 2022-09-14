using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Selector Node", menuName = "Scriptable Objects/Behavior Tree Nodes/Selector Node", order = 1)]
    public class SelectorNode : Node
    {
        public override NodeState Evalue(Blackboard blackboard)
        {
            foreach (var node in _children)
            {
                switch (node.Evalue(blackboard))
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        _nodeState = NodeState.Success;
                        return _nodeState;
                    case NodeState.Running:
                        _nodeState = NodeState.Running;
                        return _nodeState;
                    default:
                        continue;
                }
            }

                _nodeState = NodeState.Failure;
                return _nodeState;
            }
        }
    }