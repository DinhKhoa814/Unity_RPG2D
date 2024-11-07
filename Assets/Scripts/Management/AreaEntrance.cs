using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] string transitionName; // Tên chuyển tiếp để kiểm tra điều kiện

    private void Start()
    {
        // Kiểm tra xem ten nhan dien cua canh hien tai có khớp với ten dc gan trong scenemanagement không
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            // Nếu khớp, đặt vị trí của PlayerController tại vị trí của AreaEntrance
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
    }
}
