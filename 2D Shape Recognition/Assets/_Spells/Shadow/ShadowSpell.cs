using UnityEngine;
using System.Linq;

namespace ValhalaProject
{
    public class ShadowSpell : Spell
    {
        [SerializeField] private float _duration;
        [SerializeField] private Material _shadowMaterial;

        private Material _cachedMaterial;
        private MeshRenderer _playerMeshRenderer;

        public override void PerformSpell(SpellCasterData spellManagerData)
        {
            if (SilanceSpellIsActive(spellManagerData.spellCaster.ActiveSpells.ToArray()))
            {
                Debug.Log("Wellcome to the shadow Veless relms bitch"); //TODO implemnt Veles relm
            }

            _playerMeshRenderer = spellManagerData.playerMeshRenderer;
            _cachedMaterial = spellManagerData.playerMeshRenderer.material;
            spellManagerData.playerMeshRenderer.material = _shadowMaterial;
            _timeCasted = Time.time;
        }

        private bool SilanceSpellIsActive(GameObject[] spellReferences)
        {

            var singleInstanceSpells = from spells in spellReferences
                                       where spells.TryGetComponent<SilenceSpell>(out SilenceSpell spell)
                                       select spells;

            if (singleInstanceSpells.Count() != 0) { return true; }
            else { return false; }
        }

        void Update()
        {
            if (_timeCasted + _duration <= Time.time)
            {
                _playerMeshRenderer.material = _cachedMaterial;
                SpellEndedEvent.Invoke(this);
                Destroy(this.gameObject);
            }
        }
    }
}