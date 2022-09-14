using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    public class Cultist : MonoBehaviour, IDamagable, ICanHear
    {
        public int Health
        {
            get { return _health; }
            private set { _health = value; }
        }

        [SerializeField] private int _health;
        [SerializeField] private BehaviorTree _behaviorTree;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0) { Kill(); }
        }
        public void Kill()
        {
            Destroy(gameObject);
        }

        public void SetSoundHeardFlag(Transform transfrom)
        {
            if (_behaviorTree != null)
            {
                _behaviorTree.Blackboard.TargetLocation = transfrom;
                _behaviorTree.Blackboard.BoolCondtions[BlackboardCondtions.SoundHeard] = true;
            }
        }
    }


    public interface IDamagable
    {
        public void TakeDamage(int damage);
        public void Kill();
    }

    public interface ICanHear
    {
        public void SetSoundHeardFlag(Transform transfrom);
    }
}