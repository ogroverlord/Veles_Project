using UnityEngine;

namespace ValhalaProject
{
    public class TeleportSpell : Spell
    {
        [SerializeField] private float _teleportDistance;

        public override void PerformSpell(SpellCasterData spellManagerData)
        {
            spellManagerData.characterControler.Move(spellManagerData.cameraTransfrom.forward * _teleportDistance);
            SpellEndedEvent.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}
