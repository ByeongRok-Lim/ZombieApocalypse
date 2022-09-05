/*****************************************************************
 * 코 드 명 : Zombie2.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.24
 * 설    명 : 좀비2의 상태 및 좀비의 애니매이션을 관리한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/****************************************************************
 * 설명 : 좀비를 컨트롤한다.
*****************************************************************/
public class Zombie2 : MonoBehaviour
{
    [Header("좀비 체력 및 데미지")]
    public float zombieHealth = 100f;
    private float currentZombieHealth;
    public float damage = 5f;
    public HealthBar healthBar;

    [Header("좀비")]
    public NavMeshAgent zombieAgent;                                //좀비AI
    public Transform LookPoint;                                     //트랜스폼 참조
    public Camera AttackRayCastArea;
    public Transform player;
    public LayerMask playerLayer;                                   //플레이어 레이어마스크

    [Header("좀비 서있는 포인트")]
    public float zombieSpeed;                                       //좀비의 속도

    [Header("좀비 어택 정보")]
    public float timeBtwAttack;                                     //때리는 쿨타임
    private bool isAttack;                                          //때리는 중인지?

    [Header("좀비 애니메이션")]
    public Animator animator;

    [Header("좀비 상태")]
    public float visionRadius;                                      //좀비 시야(플레이어가 여기로 들어오면 좀비가 회전)
    public float attackRadius;                                      //좀비 공격 반경
    public bool playerInvisionRadius;                               //플레이어가 좀비 시야 반경안으로 들어왔는지 아닌지.
    public bool playerInAttackRadius;                               //플레이어가 좀비 공격 반경안으로 들어왔는지 아닌지.

    private void Awake()
    {
        //초기화
        currentZombieHealth = zombieHealth;
        healthBar.GiveFullHealth(zombieHealth);
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);  //시야반경안에 있니?
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);  //공격반경안에 있니?

        if (!playerInvisionRadius && !playerInAttackRadius)
        {
            Idle();
        }
        if (playerInvisionRadius && !playerInAttackRadius)
        {
            Pursueplayer();
        }
        if (playerInvisionRadius && playerInAttackRadius)
        {
            AttackPlayer();
        }

    }
    /****************************************************************
     * 설명 : 좀비의 가만히 서있는 IDLE 상태를 구현한다.
    *****************************************************************/
    private void Idle()
    {
        zombieAgent.SetDestination(transform.position);
        animator.SetBool("Idle", true);
        animator.SetBool("Running", false);
    }
    /****************************************************************
     * 설명 : 좀비가 플레이어를 추적하도록 구현한다.
    *****************************************************************/
    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(player.position))
        {
            //애니메이션
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
            animator.SetBool("Attacking", false);

        }

        zombieAgent.SetDestination(player.position);
    }
    /****************************************************************
     * 설명 : 플레이어를 공격하는 기능을 구현한다.
    *****************************************************************/
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!isAttack)
        {
            //Debug.Log("아니 왜 안돼?");
            RaycastHit hit;
            if (Physics.Raycast(AttackRayCastArea.transform.position, AttackRayCastArea.transform.forward, out hit, attackRadius))
            {
                //Debug.Log("여기가 안타");
                //Debug.Log("Attack" + hit.transform.name);

                PlayerScript playerBody = hit.transform.GetComponent<PlayerScript>();

                if (playerBody != null)
                {
                    playerBody.PlayerHitDamage(damage);
                }
                //애니메이션
                animator.SetBool("Attacking", true);
                animator.SetBool("Running", false);

            }

            isAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);             //??
        }
    }
    /****************************************************************
     * 설명 : 공격상태를 변경하도록 구현한다.
    *****************************************************************/
    private void ActiveAttacking()
    {
        isAttack = false;

    }

    /****************************************************************
     * 설명 : 좀비가 데미지를 입는다.
    *****************************************************************/
    public void ZombieHitDamage(float damage)
    {
        currentZombieHealth -= damage;

        healthBar.SetHealth(currentZombieHealth);

        if (currentZombieHealth <= 0)
        {
            
            animator.SetBool("Die", true);

            ZombieDie();
        }
    }
    /****************************************************************
     * 설명 : 좀비의 죽을 떄의 동작을 구현한다.
    *****************************************************************/
    private void ZombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackRadius = 0f;
        visionRadius = 0f;
        playerInAttackRadius = false;
        playerInvisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
