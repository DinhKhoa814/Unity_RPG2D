using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName="Weapons/New Weapon")]
public class WeaponInfo : ScriptableObject 
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public int weaponDamage;
    public float weaponRange;
}
/*
 - CreateAssetMenu là một thuộc tính trong Unity dùng để cho phép người dùng tạo các đối tượng (assets) 
tùy chỉnh từ menu trong Unity Editor. Khi bạn sử dụng thuộc tính này, bạn có thể dễ dàng tạo các tài nguyên mới 
mà không cần phải tạo một script riêng để khởi tạo các giá trị mặc định.
*/