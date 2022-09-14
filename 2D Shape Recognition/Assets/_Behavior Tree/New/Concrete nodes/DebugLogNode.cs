using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree
{
    public class DebugLogNode : ActionNode
    {
        public string Message { get; set; }

        protected override void OnStart()
        {
            Debug.Log($"OnStrt{Message}");
        }

        protected override void OnStop()
        {
            Debug.Log($"OnStop{Message}");
        }

        protected override State OnUpdate()
        {
            Debug.Log($"OnUpdate{Message}");
            return State.Success;
        }

        public DebugLogNode(Vector2 position, float width, float height, GUIStyle nodeStyle)
        {
            rect = new Rect(position.x, position.y, width, height);
            Style = nodeStyle;
        }
    }
}