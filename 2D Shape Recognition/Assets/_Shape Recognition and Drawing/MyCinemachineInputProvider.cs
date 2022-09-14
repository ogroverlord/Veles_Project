using UnityEngine;
using Cinemachine;
using ValhalaProject;

public class MyCinemachineInputProvider : CinemachineInputProvider
{
    [SerializeField] private InputManagerSO _inputManager;

    public override float GetAxisValue(int axis)
    {
        var action = ResolveForPlayer(axis, axis == 2 ? ZAxis : XYAxis);
        if (action != null)
        {
            switch (axis)
            {
                case 0: return _inputManager.GetMouseDelta().x;
                case 1: return _inputManager.GetMouseDelta().y;
                case 2: return action.ReadValue<float>();
            }
        }
        return 0;
    }
}
