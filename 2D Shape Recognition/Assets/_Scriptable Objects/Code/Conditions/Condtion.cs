using UnityEngine;

namespace MyUtilty
{
    public abstract class Condtion : ScriptableObject
    {
        public virtual bool CheckCondtion()
        {
            return true;
        }
    }
}