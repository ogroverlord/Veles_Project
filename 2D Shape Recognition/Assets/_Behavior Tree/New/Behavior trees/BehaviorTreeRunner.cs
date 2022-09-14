using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        private BehaviorTree tree;
        void Start()
        {
            tree = ScriptableObject.CreateInstance<BehaviorTree>();
            var root = ScriptableObject.CreateInstance<RootNode>();
            var log = ScriptableObject.CreateInstance<DebugLogNode>();
            var loop = ScriptableObject.CreateInstance<RepateNode>();
            var wait = ScriptableObject.CreateInstance<WaitNode>();
            var seq = ScriptableObject.CreateInstance<SequencerNode>();

            tree.RootNode = root;
            root.Child = loop;
            loop.Child = seq;

            seq.Children.Add(log);
            seq.Children.Add(wait);

            wait.Duration = 3f;
            log.Message = "Test!";

        }

        void Update()
        {
            tree.Update();
        }
    }
}