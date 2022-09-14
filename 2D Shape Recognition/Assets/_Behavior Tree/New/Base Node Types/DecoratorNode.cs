using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree
{
    public abstract class DecoratorNode : Node
    {
        public Node Child { get; set; }
    }
}