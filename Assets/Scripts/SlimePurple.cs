using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePurple : MonoBehaviour,IEnemy
{
    [SerializeField] GameObject grapeProjectilePrefab;

    Animator anim;
    SpriteRenderer spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Attack()
    {
        anim.SetTrigger(ATTACK_HASH);
        if((transform.position.x - PlayerController.Instance.transform.position.x) < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    public void SpawnProjectileAnimEvent()
    {
        Instantiate(grapeProjectilePrefab,transform.position, Quaternion.identity);
    } 
}
