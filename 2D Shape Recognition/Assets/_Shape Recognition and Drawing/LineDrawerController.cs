using UnityEngine;
using UnityEngine.VFX;

namespace ValhalaProject
{
    public class LineDrawerController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputManagerSO _inputManager;

        public int LineRenderPointsCount
        {
            get { return _lineRenderer.positionCount; }
            private set { SetPointCount(value); }
        }

        private LineRenderer _lineRenderer;
        private GestureValidator _gestureValidator;

        private VisualEffect _drawingSpark;

        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _gestureValidator = FindObjectOfType<GestureValidator>();
            _drawingSpark = GetComponentInChildren<VisualEffect>();
            _drawingSpark.enabled = false;
        }

        public void DrawLineInWorld()
        {
            _drawingSpark.enabled = true;
            _lineRenderer.positionCount++;
            Vector3 mousePosition = new Vector3(_inputManager.GetMousePosition().x, _inputManager.GetMousePosition().y, 2f);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _drawingSpark.gameObject.transform.position = worldPosition;


            _gestureValidator.CreateTimePoint(mousePosition.x, mousePosition.y, (double)Time.time);
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, worldPosition);
        }

        public void SetPointCount(int value)
        {
            _lineRenderer.positionCount = value;
            if (value == 0) { _drawingSpark.enabled = false; } // For now it is a good way to disable spark effect
        }
    }
}