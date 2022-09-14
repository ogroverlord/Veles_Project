using System.Collections.Generic;
using UnityEngine;
using MyUtilty;
using System.Linq;


namespace ValhalaProject
{
    public class SpellCaster : MonoBehaviour
    {
        [Header("Madness")]
        [SerializeField] private IntVariable _currentMadnes;

        [Header("Events")]
        [SerializeField] private GameEvent _madnessChanged;

        private Player _player;
        private SpellCasterData _spellManagerData;

        public List<GameObject> ActiveSpells { get; set; }


        void Start()
        {
            _player = FindObjectOfType<Player>();
            ActiveSpells = new List<GameObject>();

            _spellManagerData = new SpellCasterData
            {
                player = _player,
                characterControler = _player.GetComponent<CharacterController>(),
                playerTransform = _player.GetComponent<Transform>(),
                cameraTransfrom = Camera.main.transform,
                playerMeshRenderer = _player.GetComponentInChildren<MeshRenderer>(),
                spellCasterTransform = GetComponent<Transform>(),
                spellCaster = GetComponent<SpellCaster>(),
                soundEmitter = _player.GetComponent<SoundsEmitter>(),
            };
        }
        public void CastSpell(GameEventArgs args)
        {
            if (!OtherThrowableSpellOfDiffrentTypeActive(args) && !OtherSingleInstanceSpellOfTheSameTypeActive(args))
            {
                GameObject instance = Instantiate(args.spell.gameObject, _spellManagerData.spellCasterTransform);
                Spell spell = instance.GetComponent<Spell>();
                ActiveSpells.Add(instance);
                spell.SpellEndedEvent += RemoveFromActiveSpellsList;
                spell.PerformSpell(_spellManagerData);

                _currentMadnes.ModifyValueBy(args.spell.MadnessCost);
                _madnessChanged.Raise(new GameEventArgs { });
            }
            else
            {
                Debug.Log("Spell Fizzled!");
            }
        }
        public void RemoveFromActiveSpellsList(Spell spell)
        {
            spell.SpellEndedEvent -= RemoveFromActiveSpellsList;
            ActiveSpells.Remove(spell.gameObject);
        }

        private bool OtherThrowableSpellOfDiffrentTypeActive(GameEventArgs args)
        {
            if (args.spell is IThrowable) //TODO probalby all of this can be done in one LINQ querry, figure it out dumbass
            {
                var throwableSpells = from spells in ActiveSpells
                                      where spells.TryGetComponent<IThrowable>(out IThrowable throwComponent)
                                              && spells.TryGetComponent<Spell>(out Spell spell)
                                              && throwComponent.Thrown == false 
                                              && spell.FirstCast == true
                                      select spells;

                foreach (var spell in throwableSpells)
                {
                    if (spell.GetComponent<Spell>().GetType() != args.spell.GetType())
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        private bool OtherSingleInstanceSpellOfTheSameTypeActive(GameEventArgs args)
        {
            if (args.spell.SingleInstanceSpell) 
            {
                var singleInstanceSpells = from spells in ActiveSpells
                                      where spells.TryGetComponent<Spell>(out Spell spell)
                                              && spell.SingleInstanceSpell == true
                                      select spells;

                foreach (var spell in singleInstanceSpells)
                {
                    if (spell.GetComponent<Spell>().GetType() == args.spell.GetType())
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }
    }




    public struct SpellCasterData
    {
        public Player player;
        public CharacterController characterControler;
        public Transform playerTransform;
        public Transform cameraTransfrom;
        public MeshRenderer playerMeshRenderer;
        public Transform spellCasterTransform;
        public SpellCaster spellCaster;
        public SoundsEmitter soundEmitter;
    }
}