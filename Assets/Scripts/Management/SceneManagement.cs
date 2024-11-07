using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp MySceneManagement kế thừa từ lớp Singleton
public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; } // Thuộc tính để lưu trữ tên của cảnh muốn chuyển

    // Phương thức để thiết lập tên chuyển tiếp
    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName; // Gán tên chuyển tiếp
    }
}
