using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour,IEnemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletMoveSpeed;
    [SerializeField] float burstCount;//so dot ban
    [SerializeField] int projectilesPerBurst;//so vien dan tung dot
    [SerializeField][Range(0, 359)] float angleSpread;//do lan cua goc ban
    [SerializeField] float startingDistance = 0.1f;
    [SerializeField] float timeBetweenBursts;//time giua cac dot ban
    [SerializeField] float restTime = 1f;// cooldown
    [SerializeField] bool stagger;//tao do tre giua cac vien dan trong 1 dot ban
    [Tooltip("Stagger must be enabled for oscillate to function properly")]
    [SerializeField] bool oscillate;//dao dong trong goc ban

    bool isShooting = false;//dang ban

    private void OnValidate()//setting mac dinh de hop le
    {
        if (oscillate) { stagger = true; }
        if (!oscillate) { stagger = false; }
        if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistance < 0.1f) { startingDistance = 0.1f; }
        if (angleSpread == 0) { projectilesPerBurst = 1; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
    }
    public void Attack()
    {
        if (!isShooting) 
        {
            StartCoroutine(ShootRoutine());
        }
    }
    IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep , endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep,out endAngle);
        if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst; }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle,out currentAngle,out angleStep,out endAngle);
            }
            if (oscillate && i % 2 != 1) 
            {
                TargetConeOfInfluence(out startAngle,out currentAngle,out angleStep,out endAngle);
            }
            else if(oscillate) 
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);
                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;//huong vien dan ve phia ngui choi
                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }
                currentAngle += angleStep;
                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }  
            }
            currentAngle = startAngle;
            if (!stagger) { yield return new WaitForSeconds(timeBetweenBursts); }
        }
        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }
    void TargetConeOfInfluence (out float startAngle, out float currentAngle, out float angleStep,out float endAngle)//ban theo hinh non
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;//goc hop boi vector va truc x
        startAngle = targetAngle;//60
        endAngle = targetAngle;//60
        currentAngle = targetAngle;//60
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0) 
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);//tinh khoang cach giua cac vien dan n - 1
            halfAngleSpread = angleSpread / 2f;//chia doi goc ban 45
            startAngle = targetAngle - halfAngleSpread;// 15
            endAngle = targetAngle + halfAngleSpread;//105
            currentAngle = startAngle;

        }
    }
    Vector2 FindBulletSpawnPos(float currentAngle)//tinh toan vi tri cua vien dan 
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);// kề
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);// đối
        Vector2 pos = new Vector2(x, y);
        return pos;//cầng gần trung tâm hình nón x,y cang nhỏ
    }
}
/*
TryGetComponent là một phương thức trong Unity giúp bạn kiểm tra xem một GameObject có thành phần (Component) cụ thể hay không và trả về thành phần đó nếu có
TryGetComponent(out Projectile projectile) kiểm tra và lấy thành phần Projectile từ newBullet. Nếu Projectile tồn tại, phương thức UpdateMoveSpeed sẽ được gọi để cập nhật tốc độ di chuyển của viên đạn.
*/