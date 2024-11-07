using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    Rigidbody2D rb;
    Vector2 moveDir;

    KnockBack knockBack;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (knockBack.GettingKnockedBack) { return; } // dang bi day lui k di chuyen
        rb.MovePosition(rb.position + moveDir * ( moveSpeed * Time.fixedDeltaTime ));   

        if (moveDir.x < 0)//kiem tra huong di chuyen de lat hinh
        {
            spriteRenderer.flipX = true;
        }
        else if(moveDir.x > 0) 
        {
            spriteRenderer.flipX = false;
        }
    }
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }

}
