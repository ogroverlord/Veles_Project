using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace ValhalaProject
{
    public abstract class Node : ScriptableObject
    {
        [SerializeField] [ReadOnly,] protected Node _parent; //TODO read only doesn't seem to work
        [SerializeField] protected List<Node> _children;

        public Node Parent
        {
            get { return _parent; }
            private set { _parent = value; }
        }
        public List<Node> Children
        {
            get { return _children; }
            private set { _children = value; }
        }
        protected NodeState _nodeState = NodeState.Failure;

        public virtual NodeState Evalue(Blackboard blackboard)
        {
            return NodeState.Failure;
        }

        public void SetParentForChildren(Node parent) //Method used by custom script to assing parent nodes
        {
            foreach (var childNode in Children) { childNode.Parent = parent; }
        }
    }

    public enum NodeState
    {
        Running = 1,
        Success = 2,
        Failure = 3
    }
}