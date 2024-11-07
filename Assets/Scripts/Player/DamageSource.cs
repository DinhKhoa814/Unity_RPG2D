using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    int damageAmount;
    private void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);// ?. Cho phép bạn truy cập các thành phần của một obj mà có thể là null mà không gây ra lỗi NullReferenceException => ktra 1 obj xem co the la null khong
    }
}
//currentActiveWeapon as IWeapon:phép ép kiểu (type cast) trong C# để chuyển đổi CurrentActiveWeapon thành kiểu IWeapon