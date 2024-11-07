using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour,IWeapon
{
    [SerializeField] WeaponInfo weaponInfo;
    [SerializeField] GameObject magicLaser;
    [SerializeField] Transform magicLaserSpawnPoint;

    Animator anim;
    readonly int AttackHash = Animator.StringToHash("Attack");
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }
    public void Attack()
    {
        anim.SetTrigger(AttackHash);
    }
    void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(magicLaser,magicLaserSpawnPoint.position,Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);//thong thong tin khoang cach tan cong cua laser
    }
    public WeaponInfo GetWeaponInfo ()
    {
        return weaponInfo;
    }
    void MouseFollowWithOffset()
    {
        // Cập nhật hướng của vũ khí dựa trên vị trí chuột
        Vector3 mousePos = Input.mousePosition; // Lấy vị trí chuột
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position); // Chuyển đổi vị trí nhân vật sang không gian màn hình

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; // Tính góc từ vị trí chuột

        // Kiểm tra hướng chuột để xoay vũ khí
        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
