using UnityEditor;
using UnityEngine;

namespace MyUtilty
{
    [CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
            {
                e.Raise(new GameEventArgs { });
            }
        }
    }
}