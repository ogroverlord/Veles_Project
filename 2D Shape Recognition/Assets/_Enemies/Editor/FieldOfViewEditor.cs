using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ValhalaProject
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            FieldOfView fov = (FieldOfView)target;

            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360f, fov.Radius);

            Vector3 viewAngleDirection01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2);
            Vector3 viewAnglDirectione02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleDirection01 * fov.Radius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAnglDirectione02 * fov.Radius);

            if (fov.CanSeeTarget)
            {
                Handles.color = Color.green;
                Handles.DrawLine(fov.transform.position, fov.Target.transform.position);
            }
        }


        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}