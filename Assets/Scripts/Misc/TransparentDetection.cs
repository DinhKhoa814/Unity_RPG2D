using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float transparencyAmount = 0.8f; // Mức độ trong suốt khi làm mờ
    [SerializeField] float fadeTime = 0.4f; // Thời gian để hoàn tất hiệu ứng mờ

    SpriteRenderer spriteRenderer;
    Tilemap tilemap;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là "Player"
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                // Bắt đầu hiệu ứng mờ cho SpriteRenderer
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            }
            else if (tilemap)
            {
                // Bắt đầu hiệu ứng mờ cho Tilemap
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng rời va chạm là "Player"
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                // Hiệu ứng mờ trở lại bình thường cho SpriteRenderer
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime,spriteRenderer.color.a, 1f));
            }
            else if (tilemap)
            {
                // Hiệu ứng mờ trở lại bình thường cho Tilemap
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }
    //Lerp(a,b,t):chuyen doi gtri tu a->b theo he so thoi gian t
    IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime , float startValue, float targetTransparency)
    {
        float elapsedTime = 0;//time da troi qua
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            // Tạo giá trị alpha mới
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
    IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            // Tạo giá trị alpha mới
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
