using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Follow Target Node", menuName = "Scriptable Objects/Behavior Tree Nodes/Follow Target Node", order = 1)]
    public class FollowTargetNode : Node
    {
        public override NodeState Evalue(Blackboard blackboard)
        {
            blackboard.EnemyNavmeshAgent.SetDestination(blackboard.PlayerTransfrom.position); 
            return NodeState.Success;
        }
    }
}