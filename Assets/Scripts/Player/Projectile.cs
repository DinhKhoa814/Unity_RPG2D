using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] GameObject particleOnHitPrefabVFX;
    [SerializeField] bool isEnemyProjectile = false;//ktra dan cua ke thu
    [SerializeField] float projectileRange = 10f;

    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; 
    }
    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }
    public void UpdateProjectileRange(float projectileRange)//lay tt vk hien tai
    {
        this.projectileRange = projectileRange;
    }
    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (!collision.isTrigger && (enemyHealth || indestructible || player)) // ktra va cham vs cac vat k co istrigger && ...
        {
            if((player && isEnemyProjectile)||(enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (!collision.isTrigger && indestructible)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            //Instantiate(particleOnHitPrefabVFX,transform.position,transform.rotation);
            //Destroy(gameObject);
        }
    }

    void MoveProjectile()//move
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
    void DetectFireDistance()//ss khoang cach
    {
        if(Vector3.Distance(transform.position,startPos) > projectileRange)
        {
            Destroy(gameObject);     
        }
    }
}
//projectile : viên đạn