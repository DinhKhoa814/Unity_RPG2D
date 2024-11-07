using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    PlayerControls playerControls;// Biến chứa các điều khiển của người chơi
    float timeBetweenAttacks;

    bool attackButtonDown,isAttacking = false; // Trạng thái nhấn nút tấn công và đang tấn công hay không

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        // Kích hoạt các điều khiển khi đối tượng được kích hoạt
        playerControls.Enable();
    }
    private void Start()
    {
        // Đăng ký sự kiện cho việc nhấn và nhả nút tấn công
        //+= Toán tử dùng để thêm một phương thức vào danh sách các phương thức sẽ được gọi khi sự kiện xảy ra
        // _ => Một biểu thức lambda. Dấu _ ở đây là một tham số ẩn danh mà bạn không sử dụng trong hàm
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }
    private void Update()
    {
        Attack();
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }
    void StartAttacking()
    {
        // Bắt đầu tấn công khi nút tấn công được nhấn
        attackButtonDown = true;
    }

    void StopAttacking()
    {
        // Dừng tấn công khi nút tấn công được nhả
        attackButtonDown = false;
    }
    void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)//co dang nhan nut ,dang tan cong va co vu khi k
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();//goi ham attack cua vu khi
        }
    }
    void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();//dung tat ca coroutine dang chay trong script hen tai tranh gay xung dot
        StartCoroutine(TimeBetweenAttackRoutine());
    }
    IEnumerator TimeBetweenAttackRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }
}
