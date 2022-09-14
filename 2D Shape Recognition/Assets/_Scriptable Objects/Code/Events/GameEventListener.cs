
using UnityEngine;
using UnityEngine.Events;


namespace MyUtilty
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        [SerializeField] private GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] private UnityEvent<GameEventArgs> Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(GameEventArgs args)
        {
            Response.Invoke(args);
        }
    }

}