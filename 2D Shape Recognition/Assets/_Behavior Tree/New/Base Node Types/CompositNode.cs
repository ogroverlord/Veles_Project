using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree
{
    public abstract class CompositNode : Node
    {
        public List<Node> Children { get; set; } = new List<Node>();       
    }
}