using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    public class RangeCheck : MonoBehaviour
    {
        [SerializeField] private float[] _ranges;
        [SerializeField] private BlackboardCondtions[] _condtions;

        [Header("AI")]
        [SerializeField] private BehaviorTree _behaviorTree;


        private Dictionary<BlackboardCondtions, float> _keyValuePairs;
        private Player _player;

        void Start()
        {
            _keyValuePairs = new Dictionary<BlackboardCondtions, float>();

            if (_ranges.Length != 0 && _condtions.Length != 0 && _ranges.Length == _condtions.Length)
            {
                for (int i = 0; i < _ranges.Length; i++)
                {
                    _keyValuePairs.Add(_condtions[i], _ranges[i]);
                }
            }
            else
            {
                Debug.LogWarning("No matching ranges for condtions");
            }

            _player = FindObjectOfType<Player>();

        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);

            if (_keyValuePairs.Count != 0)
            {
                foreach (var item in _keyValuePairs)
                {
                    if (item.Value >= distanceToPlayer) { SetBlackboardCondtion(item.Key); }
                }
            }
        }

        private void SetBlackboardCondtion(BlackboardCondtions condtion)
        {
            if (_behaviorTree.Blackboard.BoolCondtions.ContainsKey(condtion))
            {
                _behaviorTree.Blackboard.BoolCondtions[condtion] = true;
            }
        }
    }
}