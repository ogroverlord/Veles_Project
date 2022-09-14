using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public class MadnessManager : MonoBehaviour
    {
        [SerializeField] private IntVariable _currentMadness;
        [SerializeField] private MadnessEffect[] _madnessEffects;

        void Start()
        {
            _currentMadness.SetValue(0);
        }

        public void TestMadnessEffects()
        {
            foreach (var madnessEffect in _madnessEffects)
            {
                if (madnessEffect.CheckIfAllCondtionsAreMet())
                {
                    GameObject effect = Instantiate(madnessEffect.gameObject, this.transform);
                    effect.GetComponent<MadnessEffect>().ApplyEffect();
                }
            }
        }
    }
}
