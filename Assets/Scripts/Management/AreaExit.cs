using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Lớp AreaExit quản lý việc chuyển đổi giữa các cảnh
public class AreaExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;          // Tên của cảnh sẽ được tải (ten map)
    [SerializeField] string sceneTransitionName;   // Tên de nhan dien giua cac canh

    float waitToloadTime = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName); //lay ten nhan dien cua canh tiep theo va gan 
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }
    IEnumerator LoadSceneRoutine()
    {
        while(waitToloadTime >= 0f)
        {
            waitToloadTime -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);//tai canh
    }
}
