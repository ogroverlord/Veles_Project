using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "Enemy Attack", menuName = "Scriptable Objects/Enemy Attack", order = 1)]
    public class EnemyAttack : ScriptableObject
    {
        public int HealthDamage
        {
            get { return _healthDamage; }
            private set { _healthDamage = value; }
        }
        public int MadnessDamage
        {
            get { return _madnessDamage; }
            private set { _madnessDamage = value; }
        }
        public float AttackCooldown
        {
            get { return _attackCooldown; }
            private set { _attackCooldown = value; }
        }
        public bool Ready
        {
            get { return _ready; }
            set { _ready = value; }
        }
        public float LastTimeUsed
        {
            get { return _lastTimeUsed; }
            set { _lastTimeUsed = value; }
        }

        private bool _ready = false;
        private float _lastTimeUsed = 0f;

        [SerializeField] private int _healthDamage;
        [SerializeField] private int _madnessDamage;
        [SerializeField] private float _attackCooldown;

    }
}