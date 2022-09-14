using UnityEngine;
using MyUtilty;
using Cinemachine;


namespace ValhalaProject
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControler : MonoBehaviour
    {

        [Header("Movment")]
        [SerializeField] private FloatVariable _playerSpeed;
        [SerializeField] private FloatVariable _jumpHeight;
        [SerializeField] private FloatVariable _gravityValue;
        [SerializeField] private float _crouchSpeedModifier;
        [SerializeField] private BoolVariable _playerCrouching;


        [Header("Input")]
        [SerializeField] private InputManagerSO _inputManager;

        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamer;
        [SerializeField] private Transform _eyeTransfrom; //TODO in future animations should take care of this


        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private Transform _camerTransform;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _camerTransform = Camera.main.transform;
            _playerCrouching.SetValue(false);
        }

        void Update()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0) { _playerVelocity.y = 0f; }

            Vector3 movment = _inputManager.GetPlayerMovment();
            Vector3 move = new Vector3(movment.x, 0f, movment.y);
            move = _camerTransform.forward * move.z + _camerTransform.right * move.x;
            move.y = 0;
            _controller.Move(move * Time.deltaTime * _playerSpeed.Value);

            if (_inputManager.PlayerJumped() && _groundedPlayer) { _playerVelocity.y += Mathf.Sqrt(_jumpHeight.Value * -3.0f * _gravityValue.Value); }

            _playerVelocity.y += _gravityValue.Value * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);

            if (_inputManager.Aim())
            {
                _inputManager.EnablDraw(false);
                _virtualCamer.m_Lens.FieldOfView = 50f;  //TODO find a way to provide a smooth fov transition 
            }

            if (_inputManager.Crouch() && _groundedPlayer && !_playerCrouching.Value)
            {
                _eyeTransfrom.localPosition = new Vector3(0f, _eyeTransfrom.localPosition.y - 0.4f, 0f);   //TODO for the love of god get rid of magic numbers
                _playerCrouching.SetValue(true);
                _playerSpeed.ModifyValueBy(-_crouchSpeedModifier); //TODO in future think about percentage change of speed
            }
            else if (_inputManager.Crouch() && _groundedPlayer && _playerCrouching.Value)
            {
                _eyeTransfrom.localPosition = new Vector3(0f, _eyeTransfrom.localPosition.y + 0.4f, 0f);
                _playerCrouching.SetValue(false);
                _playerSpeed.ModifyValueBy(_crouchSpeedModifier);
            }

            else if(!_inputManager.Aim() && _virtualCamer.m_Lens.FieldOfView == 50)
            {
                _inputManager.EnablDraw(true);
                _virtualCamer.m_Lens.FieldOfView = 60f;
            }

        }
    }
}