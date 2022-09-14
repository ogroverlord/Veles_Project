using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Attack Target Node", menuName = "Scriptable Objects/Behavior Tree Nodes/Attack Target Node", order = 1)]
    public class AttackTarget : Node
    {
        [SerializeField] private EnemyAttack[] _attacks;

        public override NodeState Evalue(Blackboard blackboard)
        {
            if (_attacks.Length != 0)
            {
                for (int i = 0; i < _attacks.Length; i++)
                {
                    if (_attacks[i].LastTimeUsed + _attacks[i].AttackCooldown <= Time.time)
                    {
                        _attacks[i].Ready = true;
                    }
                }

                if (_attacks[0].Ready)
                {
                    PerformAttack(_attacks[0], blackboard.Player);
                    return NodeState.Success;
                }
                else { return NodeState.Failure; }
            }
            else { return NodeState.Failure; }
        }


        private void PerformAttack(EnemyAttack attack, IDamagable target)
        {
            target.TakeDamage(-attack.HealthDamage);
            attack.LastTimeUsed = Time.time;
            attack.Ready = false;
        }
    }



}