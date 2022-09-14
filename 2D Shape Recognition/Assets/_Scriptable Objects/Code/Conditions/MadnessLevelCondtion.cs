using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "MadnessLevelCondtion", menuName = "Scriptable Objects/Condtions/MadnessLevelCondtion", order = 1)]
    public class MadnessLevelCondtion : Condtion
    {
        [SerializeField] private IntVariable _currentMadness;
        [SerializeField] private int _madnessThreshold;
        public override bool CheckCondtion()
        {
            return _currentMadness.Value >= _madnessThreshold ? true : false;
        }
    }
}