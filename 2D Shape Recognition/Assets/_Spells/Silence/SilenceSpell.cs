using System;
using UnityEngine;
using System.Linq;

namespace ValhalaProject
{
    public class SilenceSpell : Spell
    {

        [SerializeField] private float _duration;

        private SoundsEmitter _soundsEmitter;

        public override void PerformSpell(SpellCasterData spellManagerData)
        {
            if (ShadowSpellIsActive(spellManagerData.spellCaster.ActiveSpells.ToArray()))
            {
                Debug.Log("Wellcome to the shadow Veless relms bitch"); //TODO implemnt Veles relm
            }

            _timeCasted = Time.time;
            _soundsEmitter = spellManagerData.soundEmitter;
            _soundsEmitter.Enable(false);

            Debug.Log("Hush!"); 
        }

        private bool ShadowSpellIsActive(GameObject[] spellReferences)
        {

            var singleInstanceSpells = from spells in spellReferences
                                       where spells.TryGetComponent<ShadowSpell>(out ShadowSpell spell)
                                       select spells;

            if (singleInstanceSpells.Count() != 0) { return true; }
            else { return false; }
        }

        void Update()
        {
            if (_timeCasted + _duration <= Time.time)
            {
                _soundsEmitter.Enable(true);
                SpellEndedEvent.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }
}