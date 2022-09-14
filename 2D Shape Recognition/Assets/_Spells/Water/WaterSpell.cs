using UnityEngine;
using MyUtilty;
using System.Linq;

namespace ValhalaProject
{
    public class WaterSpell : Spell, IThrowable
    {
        [Header("Spell data")]
        [SerializeField] private int _healAmount;
        [SerializeField] private IntVariable _currentPlayerHelath;
        [SerializeField] private float _sphereLivespan;
        [SerializeField] private ParticleSystem _particleSystem;

        [Header("Throwing")]
        [SerializeField] private float _throwSpeed;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Input")]
        [SerializeField] private InputManagerSO _inputManager;

        private Transform _cameraTransform;
        private WaterSpell _refernceToFirstCast;


        public float SphereLivespan
        {
            get { return _sphereLivespan; }
            private set { _sphereLivespan = value; }
        }
        public float ThrowSpeed
        {
            get { return _throwSpeed; }
            set { _throwSpeed = value; }
        }
        public bool Thrown { get; private set; }
        public bool FirstCastThrown { get; private set; }
        public bool DealDamage { get; private set; } //Not used yet
        public bool Freezes { get; private set; } //Not used yet

        void Update()
        {
            if (_timeCasted + _sphereLivespan <= Time.time)
            {
                SpellEndedEvent.Invoke(this);
                Destroy(this.gameObject);
            }
            if (_inputManager.Activate() && _castCount == 1 && !Thrown && FirstCast == true) { Activate(); }
            if (_inputManager.Aim() && FirstCast == true && _castCount == 2 &&_inputManager.Throw()) { Throw(); }

            if (_refernceToFirstCast != null) //Block first cast from null refernce exception
            {
                if (!FirstCastThrown && _refernceToFirstCast.Thrown) { FirstCastThrown = true; } //Checks if first cast was thrown to mark other casts to be fitler out by LINQ
            }
        }
        public override void PerformSpell(SpellCasterData spellManagerData)
        {
              _castCount = (from spell in spellManagerData.spellCaster.ActiveSpells
                          where spell.TryGetComponent<WaterSpell>(out WaterSpell component)
                                  && component.Thrown == false
                                  && component.FirstCastThrown == false
                          select spell).Count();

            if (_castCount == 1)
            {
                _cameraTransform = spellManagerData.cameraTransfrom; //Needed to allow throwing in the current camera forward direction
                transform.parent = _cameraTransform.GetChild(0);
                this.transform.localPosition = new Vector3(0, 0, 0);
                FirstCast = true;
            }
            else if (_castCount == 2)
            {
                _particleSystem.Stop();
                this.transform.localScale = Vector3.zero;
                FindFirstInstance(spellManagerData);
            }
            else
            {
                spellManagerData.spellCaster.RemoveFromActiveSpellsList(this);
                Destroy(this.gameObject);
            }

            _timeCasted = Time.time;
        }
        private void FindFirstInstance(SpellCasterData spellManagerData)
        {
            var firstCastInstance = from spell in spellManagerData.spellCaster.ActiveSpells
                                    where spell.TryGetComponent<WaterSpell>(out WaterSpell component)
                                            && component.FirstCast == true
                                            && component.Thrown == false
                                    select spell;

            WaterSpell[] fistWaterSpell = firstCastInstance.First<GameObject>().GetComponents<WaterSpell>();

            if (fistWaterSpell[0] != null)
            {
                _refernceToFirstCast = fistWaterSpell[0];
                if (!fistWaterSpell[0].Thrown) { fistWaterSpell[0].UpdateFirstInstanceState(_castCount); }
                //Each instance has its own castCount that is why it must be passed this way. Instance shouldnt be updated when it was thrown
            }
            else { Debug.LogWarning("There is no first istance:  " + fistWaterSpell[0]); }
        } //TODO find a way to move it to a Spell class
        public void UpdateFirstInstanceState(int currentCount)
        {
            _castCount = currentCount;

            if (currentCount == 2)
            {
                this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                DealDamage = true;
                Freezes = true;
            }
            ExtendSphereLiveSpan(5f);
        } //TODO find a way to move it to a Spell class
        public void ExtendSphereLiveSpan(float value)
        {
            SphereLivespan += value;
        }
        public void Throw()
        {
            transform.parent = null;
            Thrown = true;
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = _cameraTransform.forward * ThrowSpeed;
        }
        public void Activate()
        {
            _currentPlayerHelath.ModifyValueBy(_healAmount);
            SpellEndedEvent.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}