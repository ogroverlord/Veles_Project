using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree
{
    public class BehaviorTree : ScriptableObject
    {
        public Node RootNode { get; set; }
        public Node.State TreeState { get; set; } = Node.State.Running;

        public Node.State Update()
        {
            if (RootNode.CurrentState == Node.State.Running)
            {
                TreeState = RootNode.Update();
            }
            return TreeState;
        }

    }
}