using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ValhalaProject;
using UnityEditor;

namespace MyUtilty
{
    [CustomEditor(typeof(Node), true)]
    public class NodeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Node scriptableObject = (Node)target;

            EditorGUILayout.BeginHorizontal();
            GUIContent buttonName = new GUIContent("Set parent for children");
            if (scriptableObject.Children != null)
            {
                GUI.enabled = scriptableObject.Children.Count != 0 ? true : false; 
            }
            if (GUILayout.Button(buttonName)) { scriptableObject.SetParentForChildren(scriptableObject); }
            EditorGUILayout.EndHorizontal();
        }
    }
}