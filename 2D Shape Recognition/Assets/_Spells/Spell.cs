using System;
using UnityEngine;

namespace ValhalaProject
{
    [Serializable]
    public abstract class Spell : MonoBehaviour
    {
        public int MadnessCost
        {
            get { return _madnessCost; }
            private set { _madnessCost = value; }
        }
        public string SpellWord
        {
            get { return _spellWord; }
            private set { _spellWord = value; }
        }
        public string[] Gestures
        {
            get { return _gestures; }
            set { _gestures = value; }
        }
        public bool FirstCast { get; protected set; }
        public bool SingleInstanceSpell
        {
            get { return _singleInstanceSpell; }
            set { _singleInstanceSpell = value; }
        }

        [SerializeField] protected int _madnessCost;
        [SerializeField] protected string[] _gestures;
        [SerializeField] protected string _spellWord;
        [SerializeField] protected bool _singleInstanceSpell;

        protected float _timeCasted;
        public Action<Spell> SpellEndedEvent;
        protected int _castCount; // field used by some spells to keep track of number of casts 
        public abstract void PerformSpell(SpellCasterData spellManagerData);   
    }
}