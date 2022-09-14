using System.Collections.Generic;
using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public abstract class MadnessEffect : MonoBehaviour
    {
        [SerializeField] protected List<Condtion> _conditions;
        [SerializeField] protected float _duration;
        protected float _timeApplied;

        public virtual void ApplyEffect() { }

        public virtual bool CheckIfAllCondtionsAreMet()
        {
            bool condtionPassed = false;

            foreach (var condition in _conditions)
            {
                condtionPassed = condition.CheckCondtion();
                if (!condtionPassed) { return false; }
            }

            return true;
        }
    }
}