using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ValhalaProject
{
    public class Darkness : MadnessEffect
    {
        [SerializeField] float _changePerTick;

        private Volume _volume;
        private LiftGammaGain liftGamaGain;

        public override void ApplyEffect()
        {
            _timeApplied = Time.time;
            _volume = GetComponent<Volume>();
            _volume.profile.TryGet(out liftGamaGain);
            StartCoroutine(ChangeGammaOverTime());
        }


        void Update()
        {
            if (_timeApplied + _duration < Time.time)
            {
                StopCoroutine(ChangeGammaOverTime());
                Destroy(this.gameObject);
            }
        }
        public IEnumerator ChangeGammaOverTime()
        {
            float timeDelay = 0.25f;
            float changeMultiplier = 0f;

            for (float i = _timeApplied; i < _timeApplied + _duration; i += timeDelay)
            {
                changeMultiplier -= _changePerTick;
                liftGamaGain.gamma.value = Vector4.one * changeMultiplier;
                yield return new WaitForSeconds(timeDelay);
            }
        }

    }
}