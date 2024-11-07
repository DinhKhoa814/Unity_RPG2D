using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : Singleton<ActiveInventory>
{
    int activeSlotIndexNum = 0; // Chi so o hien tai

    PlayerControls playerControls; // Luu tru dieu khien nguoi choi

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls(); // Tao doi tuong luu cac thao tac tu ban phim
    }

    private void Start()
    {
        // performed: kich hoat khi hanh dong thuc hien thanh cong
        // started: kich hoat khi bat dau thuc hien hanh dong
        // canceled: kich hoat khi hanh dong bi huy
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>()); // Luu gia tri input vao ctx, ep kieu thanh int
    }

    private void OnEnable()
    {
        playerControls.Enable(); // Bat cac dieu khien
    }
    public void EquipStartingWeapon()
    {
        ToggleActiveHighLight(0);
    }
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1); // Chuyen den o duoc chon
    }

    void ToggleActiveHighLight(int indexNum)
    {
        activeSlotIndexNum = indexNum; // Cap nhat chi so o hien tai
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false); // Tat tat ca cac o trong inventory
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true); // Bat o duoc chon theo index
        ChangeActiveWeapon(); // Thay doi vu khi
    }

    void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject); // Xoa vu khi cu
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponToSpawn == null) 
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform); // Tao vu khi moi

        //ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        //newWeapon.transform.parent = ActiveWeapon.Instance.transform; // Gan cha cho vu khi moi

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>()); // Cap nhat vu khi moi
    }
}
