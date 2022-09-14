using System.Collections.Generic;
using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public class RadialMenu : MonoBehaviour
    {

        public Spell CorrectSpell { get; private set; }

        [Header("Events")]
        public GameEvent CorrectSpellNameSelected;

        [Header("Input")]
        public InputManagerSO InputManager;


        private MenuElement[] _menuElements;
        private RectTransform _rectTransform;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _menuElements = GetComponentsInChildren<MenuElement>();
            HideMenu();
        }

        public void ShowMenu(GameEventArgs args)
        {
            CorrectSpell = args.spells[args.integer];

            if (!args.boolian) //Valued used for custom editor debug script
            {
                _rectTransform.localScale = Vector3.one;
                GenerateSpellNames(args.integer, args.spells);
            }
            else
            {
                CorrectSpellNameSelected.Raise(new GameEventArgs { spell = CorrectSpell });
            }
        }
        public void HideMenu()
        {
            _rectTransform.localScale = Vector3.zero;
        }
        private void GenerateSpellNames(int correctSpellIndex, Spell[] spells)
        {
            List<int> excluded = new List<int>();
            excluded.Add(correctSpellIndex);

            var correctMenuElementIndex = Random.Range(0, 4);
            _menuElements[correctMenuElementIndex].SetText(spells[correctSpellIndex].SpellWord);

            for (int i = 0; i < _menuElements.Length; i++)
            {
                if (i != correctMenuElementIndex)
                {
                    var selected = SelectRandomNumberWithExclusions(spells.Length, excluded);
                    excluded.Add(selected);
                    _menuElements[i].SetText(spells[selected].SpellWord);
                }
            }
        }
        private int SelectRandomNumberWithExclusions(int range, List<int> excludedList)
        {
            System.Random Randommizer = new System.Random();
            int result = Randommizer.Next(range - excludedList.Count);

            for (int i = 0; i < excludedList.Count; i++)
            {
                //if (result < excludedList[i]) 
                if (result != excludedList[i]) { return result; }
                result++;
            }
            return result;
        }
    }
}