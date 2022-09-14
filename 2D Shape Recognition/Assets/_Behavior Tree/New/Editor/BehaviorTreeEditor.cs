using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace MyBehaviorTree
{
    public class BehaviorTreeEditor : EditorWindow
    {
        [MenuItem("Window/Behavior Tree Editor")]
        private static void OpenWindow()
        {
            BehaviorTreeEditor window = GetWindow<BehaviorTreeEditor>();
            window.titleContent = new GUIContent("Behavior Tree Editor");
        }

        private List<Node> nodes;
        private string[] nodeNames; //Used to populate context menu
        private GUIStyle nodeStyle;

        void OnGUI()
        {
            DrawNodes();
            ProcessEvents(Event.current);
            ProcessNodeEvents(Event.current);

            if (GUI.changed) Repaint();
        }

        void OnEnable()
        {
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.hover.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            nodeStyle.fontSize = 12;
            nodeStyle.fontStyle = FontStyle.Bold;
            nodeStyle.alignment = TextAnchor.MiddleCenter;
            nodeNames = LoadNodeTypesNames();
        }


        private void DrawNodes()
        {
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Draw();
                }
            }
        }

        private void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1) { ProcessContextMenu(e.mousePosition); }
                    break;
            }
        }
        private void ProcessNodeEvents(Event e)
        {
            if (nodes != null)
            {
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged = nodes[i].ProcessEvents(e);

                    if (guiChanged) { GUI.changed = true; }
                }
            }
        }

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            foreach (var name in nodeNames)
            {
                genericMenu.AddItem(new GUIContent(name), false, () => OnClickAddNode(mousePosition, name));
            }
            genericMenu.ShowAsContext();
        }

        private void OnClickAddNode(Vector2 mousePosition, string name)
        {
            if (nodes == null) { nodes = new List<Node>(); }

            Node node = ScriptableObject.CreateInstance(name) as Node;
            node.rect.position = mousePosition;
            node.rect.width = 200f;
            node.rect.height = 50f;
            node.Title = name;
            node.Style = nodeStyle;
            node.Id = GUID.Generate();

            nodes.Add(node);

            //AssetDatabase.AddObjectToAsset(node, this); TODO this should be added under active behavior tree SO
        }

        private string[] LoadNodeTypesNames()
        {
            //TODO keep in mind this will break if you move the files
            string[] names = Directory.GetFiles(@"C:\Users\karol\Documents\Programing\My Unity Projects\Valhala_Project\2D Shape Recognition\Assets\_Behavior Tree\New\Concrete nodes\", "*.cs");

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Remove(0, 132);
                var indexToRemove = names[i].IndexOf(".cs");
                names[i] = names[i].Remove(indexToRemove, 3);

            }

            //foreach (var item in names) { Debug.Log(item); }
            return names;
        }
    }
}