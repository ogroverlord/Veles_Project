using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace ValhalaProject
{
    public class LightSpell : Spell, IThrowable
    {
        [Header("Spell data")]
        [SerializeField] private Light _light;
        [SerializeField] private float _sphereLivespan;
        [SerializeField] private int _fireballDamage;

        [Header("Throwing")]
        [SerializeField] private float _throwSpeed;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Input")]
        [SerializeField] private InputManagerSO _inputManager;

        private Transform _cameraTransform;
        private LightSpell _refernceToFirstCast;
        private SpellCaster _spellCaster;


        public float SphereLivespan
        {
            get { return _sphereLivespan; }
            private set { _sphereLivespan = value; }
        }
        public float ThrowSpeed
        {
            get { return _throwSpeed; }
            private set { _throwSpeed = value; }
        }
        public bool Thrown { get; private set; }
        public bool FirstCastThrown { get; private set; }

        public bool DealDamage { get; private set; } 
        public bool Blinds { get; private set; } //Not used yet


        void Update()
        {
            if (_timeCasted + _sphereLivespan <= Time.time)
            {
                SpellEndedEvent.Invoke(this);
                Destroy(this.gameObject);
            }

            if (_inputManager.Aim() && FirstCast == true && !Thrown && _inputManager.Throw()) { Throw(); }

            if (_refernceToFirstCast != null) //Block first cast from null refernce exception
            {
                if (!FirstCastThrown && _refernceToFirstCast.Thrown) { FirstCastThrown = true; } //Checks if first cast was thrown to mark other casts to be fitler out by LINQ
            }
        }
        public override void PerformSpell(SpellCasterData spellManagerData)
        {
            _castCount = (from spell in spellManagerData.spellCaster.ActiveSpells
                          where spell.TryGetComponent<LightSpell>(out LightSpell component)
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
            else if (_castCount >= 2 && _castCount < 4)
            {
                this.transform.localScale = Vector3.zero;
                _light.enabled = false;

                FindFirstInstance(spellManagerData);
            }
            else
            {
                spellManagerData.spellCaster.RemoveFromActiveSpellsList(this);
                Destroy(this.gameObject);
            }

            _spellCaster = spellManagerData.spellCaster;
            _timeCasted = Time.time;
        }
        private void FindFirstInstance(SpellCasterData spellManagerData)
        {
            var firstCastInstance = from spell in spellManagerData.spellCaster.ActiveSpells
                                    where spell.TryGetComponent<LightSpell>(out LightSpell component)
                                            && component.FirstCast == true
                                            && component.Thrown == false
                                    select spell;

            LightSpell[] fistSpell = firstCastInstance.First<GameObject>().GetComponents<LightSpell>();

            if (fistSpell[0] != null)
            {
                _refernceToFirstCast = fistSpell[0];
                if (!fistSpell[0].Thrown) { fistSpell[0].UpdateFirstInstanceState(_castCount); }
                //Each instance has its own castCount that is why it must be passed this way. Instance shouldnt be updated when it was thrown
            }
            else { Debug.LogWarning("There is no first istance:  " + fistSpell[0]); }
        }
        public void UpdateFirstInstanceState(int currentCount)
        {
            _castCount = currentCount;

            if (currentCount == 2)
            {
                this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                Blinds = true;  //TODO implement when enemies are created 

            }
            if (currentCount == 3)
            {
                this.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                DealDamage = true;
            }
            ExtendSphereLiveSpan(5f);
        }
        private void DealDamageToTargat(IDamagable target) //TODO later on consider adding area of effect
        {
            target.TakeDamage(_fireballDamage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_castCount == 3 && collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) //TODO magic string 
            {
                if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable component))
                {
                    DealDamageToTargat(component);

                    _spellCaster.RemoveFromActiveSpellsList(this);
                    Destroy(gameObject);
                }
            }
        }



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



    }
}