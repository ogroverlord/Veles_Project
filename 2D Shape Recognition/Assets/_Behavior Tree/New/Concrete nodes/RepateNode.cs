using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree
{
    public class RepateNode : DecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            //TODO consider adding property to control how many times it should try to repate no matter the return state
            //TODO or repate until success etc. 

            Child.Update();
            return State.Running;
        }
    }
}