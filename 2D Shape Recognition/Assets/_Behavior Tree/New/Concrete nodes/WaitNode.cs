using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree
{
    public class WaitNode : ActionNode
    {
        public float Duration { get; set; } = 1;

        private float _startTime;
        protected override void OnStart()
        {
            _startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Time.time - _startTime > Duration)
            {
                return State.Success;
            }
            else
            {
                return State.Running;
            }
        }
    }
}