using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    [SerializeField] private CinemachineVirtualCamera tpsCamera;

    private void Start()
    {
        tpsCamera.Priority = 1;
        fpsCamera.Priority = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // V from word VIEW
        {
            SwitchPriority();
        }
    }
    
    private void SwitchPriority()
    {
        (tpsCamera.Priority, fpsCamera.Priority) = (fpsCamera.Priority, tpsCamera.Priority);
    }
}
