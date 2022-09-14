using UnityEngine;
using System.Collections.Generic;
using ValhalaProject;

namespace MyUtilty
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {

        private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

        public void Raise(GameEventArgs args)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--) { eventListeners[i].OnEventRaised(args); }

        }
        public void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener)) { eventListeners.Add(listener); }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener)) { eventListeners.Remove(listener); }
        }
    }

    public struct GameEventArgs
    {
        public string text;
        public int integer;
        public float floatingpoint;
        public bool boolian;
        public Spell[] spells;
        public Spell spell;
    }
}