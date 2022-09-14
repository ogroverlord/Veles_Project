using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Condtion Node", menuName = "Scriptable Objects/Behavior Tree Nodes/Condtion Node", order = 1)]
    public class CondtionNode : Node
    {
        [SerializeField] private BlackboardCondtions _condtionName;

        public override NodeState Evalue(Blackboard blackboard)
        {

            if (blackboard.BoolCondtions.TryGetValue(_condtionName, out bool condtion))
            {
                if (condtion)
                {
                    Debug.Log(_condtionName + " --  passed!");
                    _nodeState = NodeState.Success;
                    blackboard.BoolCondtions[_condtionName] = false; //Reset condtion after passing 
                    return _nodeState;
                }
            }

            _nodeState = NodeState.Failure;
            return _nodeState;
        }
    }
}