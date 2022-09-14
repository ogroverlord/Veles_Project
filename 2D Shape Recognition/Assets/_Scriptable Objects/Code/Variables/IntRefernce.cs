using System;

namespace MyUtilty
{
    [Serializable]
    public class IntRefernce
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public int Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public IntRefernce()
        { }

        public IntRefernce(int value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public static implicit operator int(IntRefernce reference)
        {
            return reference.Value;
        }
    }
}