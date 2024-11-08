using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera virtualCamera;
    private void Start()
    {
        SetPlayerCameraFollow();
    }
    public void SetPlayerCameraFollow()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = PlayerController.Instance.transform;
    }
}
