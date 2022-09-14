using UnityEngine;
using MyUtilty;

namespace ValhalaProject
{
    public class PointCaster : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private float _readDelay;
        [SerializeField] private float _inputDelay;
        [SerializeField] private InputManagerSO _inputManager;

        [Header("Events")]
        [SerializeField] private GameEvent _gestureFinished;

        private LineDrawerController _lineDrawerController;
        private float _lastTime = 0f;


        void Start()
        {
            _lineDrawerController = FindObjectOfType<LineDrawerController>();
            _lineDrawerController.SetPointCount(0);
        }

        void Update()
        {
            if (_inputManager.DrawGesture())
            {
                if (_lastTime + _readDelay < Time.time)
                {
                    _inputManager.EnableLookInputAction(false);
                    _inputManager.EnableMovmentInputAction(false);
                    _lineDrawerController.DrawLineInWorld();
                    _lastTime = Time.time;
                }
            }
            if (!_inputManager.DrawGesture() && _lineDrawerController.LineRenderPointsCount != 0)
            {
                _lineDrawerController.SetPointCount(0);
                _lastTime = Time.time + _inputDelay;

                _gestureFinished.Raise(new GameEventArgs { });
            }
        }
    }
}