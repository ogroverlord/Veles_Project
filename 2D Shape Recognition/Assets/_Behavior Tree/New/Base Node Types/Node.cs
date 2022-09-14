using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyBehaviorTree
{
    public abstract class Node : ScriptableObject
    {
       public enum State
        {
            Running,
            Failure,
            Success
        }

        #region Editor stuff

        public Rect rect;
        public string Title { get; set; }
        public GUID Id { get; set; }
        public GUIStyle Style { get; set; }

        public bool IsDragged { get; set; }


        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public void Draw()
        {
            GUI.Box(rect, Title, Style);
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            IsDragged = true;
                            GUI.changed = true;
                        }
                        else
                        {
                            GUI.changed = true;
                        }
                    }
                    break;

                case EventType.MouseUp:
                    IsDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && IsDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }
        #endregion


        #region Node logic

        public State CurrentState { get; set; } = State.Running;
        public bool Started { get; set; } = false;


        public State Update()
        {
            if (!Started)
            {
                OnStart();
                Started = true;
            }

            CurrentState = OnUpdate();

            if(CurrentState == State.Failure || CurrentState == State.Success)
            {
                OnStop();
                Started = false;

            }

            return CurrentState;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
        #endregion

    }
}