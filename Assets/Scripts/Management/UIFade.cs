using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Lớp UIFade kế thừa từ Singleton để đảm bảo chỉ có một instance duy nhất
public class UIFade : Singleton<UIFade>
{
    [SerializeField] Image fadeScreen; // Đối tượng Image để hiển thị hiệu ứng mờ
    [SerializeField] float fadeSpeed; // Tốc độ mờ (fade)

    IEnumerator fadeRoutine; // Biến để lưu trữ coroutine hiện tại

    // Phương thức để mờ màn hình sang màu đen
    public void FadeToBlack()
    {
        // Nếu có một quy trình fade đang chạy, dừng nó
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        // Thiết lập quy trình fade mới đến alpha = 1 (đen)
        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine); // Bắt đầu quy trình fade
    }

    // Phương thức để làm rõ màn hình
    public void FadeToClear()
    {
        // Nếu có một quy trình fade đang chạy, dừng nó
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        // Thiết lập quy trình fade mới đến alpha = 0 (trong suốt)
        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine); // Bắt đầu quy trình fade
    }

    // Coroutine để thực hiện quá trình fade đến giá trị alpha mục tiêu
    IEnumerator FadeRoutine(float targetAlpha)
    {
        // Vòng lặp cho đến khi alpha hiện tại khớp với targetAlpha
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))// Approximately: ktra xem 2 gi tri co xap xi bang nhau khong
        {
            // Tính toán giá trị alpha mới
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);// tang gia tri tu a -> b
            // Cập nhật màu sắc của fadeScreen với giá trị alpha mới
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null; // Đợi cho đến khung hình tiếp theo
        }
    }
}
