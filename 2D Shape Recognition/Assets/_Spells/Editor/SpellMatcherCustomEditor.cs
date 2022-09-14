using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ValhalaProject;

namespace MyUtilty
{
    [CustomEditor(typeof(SpellMatcher))]
    public class SpellMatcherCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpellMatcher script = (SpellMatcher)target;

            string[] spellNames = new string[script.AvilableSpells.Length];

            for (int i = 0; i < spellNames.Length; i++)
            {
                spellNames[i] = script.AvilableSpells[i].name;
            }

            GUILayoutOption layoutOptions = GUILayout.Width(200f);
            GUI.enabled = Application.isPlaying;

            EditorGUILayout.BeginHorizontal();
            script.SpellIndex = EditorGUILayout.Popup(script.SpellIndex, spellNames, layoutOptions);

            GUIContent buttonName = new GUIContent("Cast");
            if (GUILayout.Button(buttonName))
            {
                script.FindDesiredSpell(new GameEventArgs 
                { text = script.AvilableSpells[script.SpellIndex].Gestures[0], boolian = script.SkipSpellWordSelection });
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}