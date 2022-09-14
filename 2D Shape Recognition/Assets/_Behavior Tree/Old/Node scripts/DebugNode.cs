using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Debug Node", menuName = "Scriptable Objects/Behavior Tree Nodes/Debug Node", order = 1)]
    public class DebugNode : Node
    {
        [SerializeField] private string _message;
        [SerializeField] private bool succeed;

        public override NodeState Evalue(Blackboard blackboard)
        {
            if (succeed)
            {
                Debug.Log(_message);
                return NodeState.Success;
            }
            else { return NodeState.Failure; }

        }
    }
}