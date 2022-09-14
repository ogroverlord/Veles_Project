using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public class SoundsEmitter : MonoBehaviour
    {
        [Header("Sound Range")] //TODO think how to make it more universal for othe things like casting spells,
        [SerializeField] float _crouchSoundRadius;
        [SerializeField] float _walkSoundRadius;

        [Header("Input")]
        [SerializeField] InputManagerSO _inputManager;
        [SerializeField] BoolVariable _playerCrouching;

        public bool Enabled { get; private set; }

        public void Enable(bool value)
        {
            Enabled = value;
        }

        void Start()
        {
            Enable(true);
        }

        public void EmitSound()
        {
            if (Enabled)
            {
                if (_playerCrouching.Value)
                {
                    //TODO add methods from metla project to change layer to layer mask
                    Collider[] colliders = Physics.OverlapSphere(transform.position,
                                                                    _crouchSoundRadius, LayerMask.GetMask(new string[] { "Enemy" }));
                    NotifyICanHearEntities(colliders);
                }
                else
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position,
                                                                    _walkSoundRadius, LayerMask.GetMask(new string[] { "Enemy" }));
                    NotifyICanHearEntities(colliders);
                }
            }
            else
            {
                Debug.Log("Emitter disabled");
            }

        }

        private void NotifyICanHearEntities(Collider[] colliders)
        {
            if (colliders != null)
            {
                foreach (var collider in colliders)
                {
                    if (collider.transform.gameObject.TryGetComponent<ICanHear>(out ICanHear component))
                    {
                        component.SetSoundHeardFlag(transform);
                    }
                }
            }
        }

        void Update()
        {
            if (_inputManager.GetPlayerMovment() != Vector2.zero) { EmitSound(); }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _crouchSoundRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _walkSoundRadius);
        }

    }
}