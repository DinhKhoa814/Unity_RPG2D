using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float roamChangeDirFloat = 2f;//time doi huong tan cong
    [SerializeField] float attackRange = 0f;
    [SerializeField] MonoBehaviour enemyType;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] bool stopMovingWhileAttacking = false;

    SpriteRenderer spriteRenderer;
    bool canAttack = true;
    private enum State
    {
        Roaming,
        Attacking
    }
    Vector2 roamPos;
    float timeRoam = 0f;

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPos = GetRoamingPosition();
    }
    private void Update()
    {
        MovementStatControl();
    }
    private void MovementStatControl()
    {
        switch (state) 
        {
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }
    private void Roaming()
    {
        timeRoam += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPos);
        if(Vector2.Distance(transform.position,PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        if(timeRoam > roamChangeDirFloat)
        {
            roamPos = GetRoamingPosition();
        }

    }
    private void Attacking()
    {
        if(Vector2.Distance(transform.position,PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();//ep kieu
            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPos);
            }
            StartCoroutine(AttackCooldownRoutine());
        }
    }
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private Vector2 GetRoamingPosition()
    {
        timeRoam = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}
