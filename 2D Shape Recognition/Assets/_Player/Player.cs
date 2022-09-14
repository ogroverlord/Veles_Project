using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public class Player : MonoBehaviour, IDamagable
    {
        public IntVariable _currentHealth;
        public InputManagerSO _inputManager;

        void Start()
        {
            _currentHealth.SetValue(100);
        }

        public void Kill()
        {
            _inputManager.DisablePlayerControls(); //TODO for now only this, later on some other methods will be used
        }
        public void TakeDamage(int damage)
        {
            _currentHealth.ModifyValueBy(damage);
        }
    }
}