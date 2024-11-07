using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] GameObject deathVFXPrefab; 
    [SerializeField] float knockBackThrust = 15f; 

    int currentHealth;

    KnockBack knockBack; // Tham chiếu đến lớp KnockBack để xử lý đẩy lùi
    Flash flash; // Tham chiếu đến lớp Flash để tạo hiệu ứng flash khi nhận sát thương

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        // - health hiện tại và xử lý các hiệu ứng khi nhận sát thương
        currentHealth -= damage;
        knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }
    IEnumerator CheckDetectDeathRoutine()
    {
        // Đợi cho đến khi hiệu ứng flash kết thúc trước khi kiểm tra health
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}
