using UnityEngine;
using TMPro;
using MyUtilty;

namespace ValhalaProject
{
    public class DebugUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recordingModeText;
        [SerializeField] private BoolVariable _recordingEnabled;

        [SerializeField] private InputManagerSO _inputManager;

        void Start()
        {
            _recordingModeText.transform.localScale = new Vector3(0f, 0f, 0f);
            _recordingEnabled.SetValue(false);
        }

        void Update()
        {
#if (UNITY_EDITOR)

            if (_inputManager.EnableDisableRecordingMode()) { SetRecording(); }

#endif
        }

        public void SetRecording()
        {
            if (!_recordingEnabled.Value)
            {
                _recordingEnabled.SetValue(true);
                _recordingModeText.transform.localScale = new Vector3(1f, 1f, 1f);

            }
            else
            {
                _recordingEnabled.SetValue(false);
                _recordingModeText.transform.localScale = new Vector3(0f, 0f, 0f);
            }
        }
    }
}