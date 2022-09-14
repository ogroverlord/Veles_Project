using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "NoOtherEffectInstancePresentCondition", menuName = "Scriptable Objects/Condtions/NoOtherEffectInstancePresentCondition", order = 1)]
    public class NoOtherEffectInstancePresentCondition : Condtion
    {

        [SerializeField] private string effectName; //TODO strings are evil, find better way 

        public override bool CheckCondtion()
        {
            var effectList = FindObjectsOfType<MadnessEffect>();

            if (effectList == null)
            {
                return true;
            }
            else
            {
                foreach (var effect in effectList)
                {
                    if (effect.name.ToLower() == effectName.ToLower()) { return false; }
                }

                return true;

            }
        }
    }
}