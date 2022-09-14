using UnityEngine;

namespace MyUtilty
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Scriptable Objects/BoolVariable", order = 1)]
    public class BoolVariable : ScriptableObject
    {
        public bool Value;

        public void SetValue(bool value)
        {
            Value = value;
        }
        public void SetValue(BoolVariable value)
        {
            Value = value.Value;
        }
    }
}
