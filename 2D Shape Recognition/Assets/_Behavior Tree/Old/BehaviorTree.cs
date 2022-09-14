using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ValhalaProject
{
    public class BehaviorTree : MonoBehaviour
    {
        [SerializeField] private Node _root;

        public Blackboard Blackboard { get; set; }

        void Start()
        {
            Blackboard = new Blackboard
            {
                Player = FindObjectOfType<Player>(),
                PlayerTransfrom = FindObjectOfType<Player>().transform,
                EnemyNavmeshAgent = GetComponent<NavMeshAgent>(),
                TargetLocation = null,
                BoolCondtions = new Dictionary<BlackboardCondtions, bool>()
            };

            Blackboard.BoolCondtions.Add(BlackboardCondtions.SoundHeard, false); 
            Blackboard.BoolCondtions.Add(BlackboardCondtions.PlayerSeen, false); 
            Blackboard.BoolCondtions.Add(BlackboardCondtions.PlayerInAttackRange, false); 
        }

        void Update()
        {
            if (_root != null) { _root.Evalue(Blackboard); }
        }
    }

    public class Blackboard
    { 
        public Player Player { get; set; }
        public Transform PlayerTransfrom { get; set; }
        public NavMeshAgent EnemyNavmeshAgent { get; set; }
        public Transform TargetLocation { get; set; }
        public Dictionary<BlackboardCondtions, bool> BoolCondtions { get; set; }
    }

    public enum BlackboardCondtions
    {
        SoundHeard = 1,
        PlayerSeen = 2,
        PlayerInAttackRange = 3,
    }
}
