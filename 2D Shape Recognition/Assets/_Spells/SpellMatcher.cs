using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public class SpellMatcher : MonoBehaviour
    {
        [Header("Spells")]
        public Spell[] AvilableSpells;

        [Header("Events")]
        [SerializeField] private GameEvent _spellMatched;

        [Header("Debug")]
        public bool SkipSpellWordSelection;

        [HideInInspector] public int SpellIndex; //Needed for debug editor

        public void FindDesiredSpell(GameEventArgs args)
        {
            for (int i = 0; i < AvilableSpells.Length; i++)
            {
                foreach (var spellGestures in AvilableSpells[i].Gestures)
                {
                    if (spellGestures == args.text)
                    {
                        _spellMatched.Raise(new GameEventArgs {spells = AvilableSpells, integer = i, boolian = SkipSpellWordSelection });
                    }
                }
            }
        }

    }
}