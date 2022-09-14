using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public class LightningSpell : Spell
    {

        [SerializeField] private float _duration;
        [SerializeField] private float _speedIncrease;
        [SerializeField] private FloatVariable _playerSpeed;

        public override void PerformSpell(SpellCasterData spellManagerData)
        {
            _timeCasted = Time.time;
            _playerSpeed.ModifyValueBy(_speedIncrease);
        }

        void Update()
        {
            if (_timeCasted + _duration <= Time.time)
            {
                _playerSpeed.ModifyValueBy(-_speedIncrease);
                SpellEndedEvent.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }
}
