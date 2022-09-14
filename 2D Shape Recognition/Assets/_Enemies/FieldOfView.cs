using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValhalaProject
{
    public class FieldOfView : MonoBehaviour
    {
        [Header("FOV")]
        [SerializeField] private float _radius; 
        [SerializeField] [Range(0, 360)] private float _angle;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstructionMask;

        [Header("AI")]
        [SerializeField] private BehaviorTree _behaviorTree;


        public float Radius
        {
            get { return _radius; }
            private set { _radius = value; }
        }
        public float Angle
        {
            get { return _angle; }
            private set { _angle = value; }
        }
        public bool CanSeeTarget { get; private set; }
        public Player Target { get; private set; } //Used to draw debug line when player is in a line of sight


        void Start()
        {
            Target = FindObjectOfType<Player>();
            StartCoroutine(FOVRoutine()); //Using a coroutine reduce a number of checks per frame
        }

        private IEnumerator FOVRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, _targetMask);

            if (rangeChecks.Length != 0) //Check if anything was found
            {
                Transform target = rangeChecks[0].transform; //Now it only supports one object 
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                    {
                        CanSeeTarget = true;
                        SetSoundHeardFlag(target);
                    }
                    else { CanSeeTarget = false; }
                }
                else { CanSeeTarget = false; }
            }
            else if (CanSeeTarget) { CanSeeTarget = false; }
        }

        public void SetSoundHeardFlag(Transform transfrom)
        {
            if (_behaviorTree != null)
            {
                _behaviorTree.Blackboard.TargetLocation = transfrom;
                _behaviorTree.Blackboard.BoolCondtions[BlackboardCondtions.PlayerSeen] = true;
            }
        }
    }

}