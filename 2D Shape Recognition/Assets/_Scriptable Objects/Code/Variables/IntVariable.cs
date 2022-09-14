using UnityEngine;

namespace MyUtilty
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Scriptable Objects/IntVariable", order = 1)]
    public class IntVariable : ScriptableObject
    {
        public int Value;

        [SerializeField] private int _maxValue;
        [SerializeField] private bool _usesMaxValue;

        public void SetValue(int value)
        {
            if (CheckIfMaximuValueWasExceded(value) && _usesMaxValue) { Value = _maxValue; }
            else { Value = value; }
        }
        public void SetValue(IntVariable value)
        {
            if (CheckIfMaximuValueWasExceded(value.Value) && _usesMaxValue) { Value = _maxValue; }
            else { Value = value.Value; }
        }
        public void ModifyValueBy(int amount)
        {
            if (CheckIfMaximuValueWasExceded(Value + amount) && _usesMaxValue) { Value = _maxValue; }
            else { Value += amount; }
        }
        public void ModifyValueBy(IntVariable amount)
        {
            if (CheckIfMaximuValueWasExceded(Value + amount.Value) && _usesMaxValue) { Value = _maxValue; }
            else { Value += amount.Value; }
        }


        private bool CheckIfMaximuValueWasExceded(int currentValue)
        {
            return currentValue > _maxValue ? true : false;
        }
    }
}