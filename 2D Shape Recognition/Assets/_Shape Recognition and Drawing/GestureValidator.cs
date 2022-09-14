using System.Collections.Generic;
using UnityEngine;
using Recognizer.Dollar;
using WobbrockLib;
using UnityEditor;
using System.IO;
using MyUtilty;

namespace ValhalaProject
{
    public class GestureValidator : MonoBehaviour
    {
        [Header("Gestures")]
        [SerializeField] private float _gestureScoreThreshold;
        [SerializeField] private GameEvent _gestureRecognizedEvent;

        [Header("Debug")]
        [SerializeField] private BoolVariable _recordingModeEnabled;

        [Header("Input")]
        [SerializeField] private InputManagerSO _inputManager;

        public List<TimePointF> ValidatorPoints { get; set; }

        Recognizer.Dollar.Recognizer _recognizer;

        void Start()
        {
            SetupRecognizer();
            ValidatorPoints = new List<TimePointF>();
        }

        private void SetupRecognizer()
        {
            _recognizer = new Recognizer.Dollar.Recognizer();
            string[] files = Directory.GetFiles(@"C:\Users\karol\Documents\Programing\My Unity Projects\Valhala_Project\2D Shape Recognition\Assets\_Gestures\", "*.xml");
            foreach (var file in files) { _recognizer.LoadGesture(file); }
        }

        public void CreateTimePoint(float x, float y, double time)
        {
            ValidatorPoints.Add(new TimePointF(x, y, time * 1000));
        }

        public void CompareOrRecordShape()
        {
            if (!_recordingModeEnabled.Value)
            {
                NBestList result = _recognizer.Recognize(ValidatorPoints, true);

                if (result.Name != null && result.Score > _gestureScoreThreshold)
                {
                    Debug.Log(result.Name + " -- " + result.Score);
                    _gestureRecognizedEvent.Raise(new GameEventArgs { text = result.Name });
                }
                else
                {
                    _inputManager.EnableLookInputAction(true);
                    _inputManager.EnableMovmentInputAction(true);

                }
                ValidatorPoints.Clear();
            }
            else
            {
#if (UNITY_EDITOR)

                string path = EditorUtility.SaveFilePanel("Save Gesture", string.Empty, "test", "xml");
                path = path.Replace(@"/", @"\");
                _recognizer.SaveGesture(@path, ValidatorPoints);
#endif

                ValidatorPoints.Clear();
            }


        }
    }
}