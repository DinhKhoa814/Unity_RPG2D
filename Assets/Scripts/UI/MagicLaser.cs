using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] float laserGrowTime = 2f;//time max

    bool isGrowing = true;//ktra dang keo dai cua tia laser
    float laserRange;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider2D;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        LaserFaceMouse();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Indestructible>() && !collision.isTrigger){//va cham vs indes vs k phai istrigger
            isGrowing = false;
        }
    }
    public void UpdateLaserRange(float laserRange)//cap nhat range theo range vu khi
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }
    IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;//time da qua
        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            //sprite
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f,laserRange, linearT), 1f);

            //collider
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f,laserRange,linearT))/2,capsuleCollider2D.offset.y);  
            
            yield return null;
        }
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }
    void LaserFaceMouse()//keo dai theo huong chuot
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = transform.position - mousePos;
        transform.right = - direction;
    }
}
