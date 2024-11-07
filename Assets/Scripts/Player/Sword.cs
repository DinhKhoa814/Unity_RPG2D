using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject slashPrefab; // Prefab cho hieu ung chem
    [SerializeField] Transform slashTransform; // Vi tri tao hieu ung chem
    [SerializeField] float swordAttackCD = 0.5f; // Thoi gian hoi chieu giua cac don tan cong
    [SerializeField] WeaponInfo weaponInfo;

    private Transform weaponCollider;
    Animator anim; // Animator de dieu khien hoat anh
    GameObject slashAnim; // Bien chua hieu ung chem

    private void Awake()
    {
        // Khoi tao cac thanh phan
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashTransform = GameObject.Find("SlashSpawnPoint").transform;
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        // Kich hoat hoat anh tan cong
        anim.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true); // Kich hoat collider de xu ly va cham
        slashAnim = Instantiate(slashPrefab, slashTransform.transform.position, Quaternion.identity); // Tao hieu ung chem
        slashAnim.transform.parent = this.transform.parent; // Gan cha cho hieu ung
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void DoneAttackingAnimEvent()
    {
        // Tat collider sau khi hoan thanh hoat anh tan cong
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        // Xoay hieu ung chem khi chem len
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        //if (PlayerController.Instance.FacingLeft)
        //{
        if (PlayerController.Instance.FacingLeft) { 
            // Lat hieu ung chem neu nhan vat dang quay sang trai
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        // Xoay hieu ung chem khi chem xuong
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //if (PlayerController.Instance.FacingLeft)
        //{
        if (PlayerController.Instance.FacingLeft) { 
            // Lat hieu ung chem neu nhan vat dang quay sang trai
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    void MouseFollowWithOffset()
    {
        // Cập nhật hướng của vũ khí dựa trên vị trí chuột
        Vector3 mousePos = Input.mousePosition; // Lấy vị trí chuột
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint( PlayerController.Instance.transform.position); // Chuyển đổi vị trí nhân vật sang không gian màn hình

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; // Tính góc từ vị trí chuột

        // Kiểm tra hướng chuột để xoay vũ khí
        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}


