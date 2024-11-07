using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponInfo weaponInfo;//Scriptable obj
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }
    public void Attack()
    {
        anim.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position , ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
    }
    public WeaponInfo GetWeaponInfo()//lay thong tin cua vu khi
    {
        return weaponInfo;
    }
}
//Animator.StringToHash: Chuyển đổi chuỗi (ví dụ: "Fire") thành giá trị hash (integer) để sử dụng trong Animator tối ưu hiệu suất
//readonly: sử dụng để chỉ định rằng một biến chỉ có thể được gán giá trị một lần trong quá trình khởi tạo,không thể thay đổi trong suốt thời gian sống của đối tượng