using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MyUtilty;

namespace ValhalaProject
{
    public class MenuElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _highlighttColor;

        private TextMeshProUGUI _spellNameText;
        private RadialMenu _radialMenu;
        private Image _backgroundImage;

        void Start()
        {
            _spellNameText = GetComponentInChildren<TextMeshProUGUI>();
            _backgroundImage = GetComponent<Image>();
            _radialMenu = GetComponentInParent<RadialMenu>();
            _backgroundImage.color = _defaultColor;
        }

        public void SetText(string text)
        {
            _spellNameText.text = text;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _backgroundImage.color = _highlighttColor;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _backgroundImage.color = _defaultColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _backgroundImage.color = _highlighttColor;

            if (_spellNameText.text == _radialMenu.CorrectSpell.SpellWord)
            {
                _radialMenu.CorrectSpellNameSelected.Raise(new GameEventArgs { spell = _radialMenu.CorrectSpell });
            }
            else
            {
                Debug.Log("Spell Fizzled");
            }
            _radialMenu.InputManager.EnableLookInputAction(true);
            _radialMenu.InputManager.EnableMovmentInputAction(true);
            _radialMenu.HideMenu();
        }
    }
}
