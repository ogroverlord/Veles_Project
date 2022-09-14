using UnityEngine;

namespace MyUtilty
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Scriptable Objects/FloatVariable", order = 1)]
    public class FloatVariable : ScriptableObject //TODO add toggle so that selected values reset and other don't when restarting the game
    {
        public float Value;

        [SerializeField] private float _maxValue;
        [SerializeField] private bool _usesMaxValue;

        public void SetValue(float value)
        {
            if (CheckIfMaximuValueWasExceded(value) && _usesMaxValue) { Value = _maxValue; }
            else { Value = value; }
        }
        public void SetValue(FloatVariable value)
        {
            if (CheckIfMaximuValueWasExceded(value.Value) && _usesMaxValue) { Value = _maxValue; }
            else { Value = value.Value; }
        }
        public void ModifyValueBy(float amount)
        {
            if (CheckIfMaximuValueWasExceded(Value + amount) && _usesMaxValue) { Value = _maxValue; }
            else { Value += amount; }
        }
        public void ModifyValueBy(FloatVariable amount)
        {
            if (CheckIfMaximuValueWasExceded(Value + amount.Value) && _usesMaxValue) { Value = _maxValue; }
            else { Value += amount.Value; }
        }


        private bool CheckIfMaximuValueWasExceded(float currentValue)
        {
            return currentValue > _maxValue ? true : false;
        }
    }
}