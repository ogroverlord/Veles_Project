using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using MyUtilty;

namespace ValhalaProject
{
    public class TiltedVision : MadnessEffect
    {
        [SerializeField] float _changePerTick;
        [SerializeField] float _TransitionTime;
        [SerializeField] private IntVariable _curremtMadness;

        private Volume _volume;
        private LensDistortion lenseDistortion;

        public override void ApplyEffect()
        {
            //TODO Cinemamchine doesn't gives easy access to camera position and rotation, needs a workaround
            _timeApplied = Time.time;

            _volume = GetComponent<Volume>();
            _volume.profile.TryGet(out lenseDistortion);
            StartCoroutine(ChangeLenseDistortionOverTime());
        }

        void Update()
        {
            if (_timeApplied + _duration < Time.time)
            {
                StopCoroutine(ChangeLenseDistortionOverTime());
                Destroy(this.gameObject);
            }
        }
        public IEnumerator ChangeLenseDistortionOverTime()
        {
            float timeDelay = 0.1f;

            for (float i = _timeApplied; i < _timeApplied + _TransitionTime; i += timeDelay)
            {
                lenseDistortion.intensity.value += _changePerTick;
                yield return new WaitForSeconds(timeDelay);
            }
        }
    }
}