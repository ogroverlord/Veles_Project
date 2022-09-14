using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "HealthCondtion", menuName = "Scriptable Objects/Condtions/HealthCondtion", order = 1)]
    public class HealthCondition : Condtion
    {
        [SerializeField] private IntVariable _playerHealth;
        [SerializeField] private int _healthThreshold;
        public override bool CheckCondtion()
        {
            return _playerHealth.Value <= _healthThreshold ? true : false;
        }
    }
}