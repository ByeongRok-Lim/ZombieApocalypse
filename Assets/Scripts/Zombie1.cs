/*****************************************************************
 * 코 드 명 : Zombie1.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.23
 * 설    명 : 좀비1의 상태 및 좀비의 애니매이션을 관리한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/****************************************************************
 * 설명 : 좀비를 컨트롤한다.
*****************************************************************/
public class Zombie1 : MonoBehaviour
{
    [Header("좀비 체력 및 데미지")]
    private float zombieHealth = 100f;
    private float currentZombieHealth;
    public float damage = 5f;
    public HealthBar healthBar;


    [Header("좀비")]
    public NavMeshAgent zombieAgent;                                //좀비AI
    public Transform LookPoint;                                     //트랜스폼 참조
    public Camera AttackRayCastArea;
    public Transform player;
    public LayerMask playerLayer;                                   //플레이어 레이어마스크

    [Header("좀비 walkPoints")]
    public GameObject[] walkPoints;                                 //걸어다닐 walkPoints
    int currentZombiePos = 0;                                       //현재 좀비 위치
    public float zombieSpeed;                                       //좀비의 속도
    float walkingPointRadius = 2;                                   //좀비가 걷는 반경

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
            Guard();
        }
        if(playerInvisionRadius && !playerInAttackRadius)
        {
            Pursueplayer();
        }
        if (playerInvisionRadius && playerInAttackRadius)
        {
            AttackPlayer();
        }

    }
/****************************************************************
 * 설명 : 좀비가 주변을 탐색하고 행동을 취한다.
*****************************************************************/
    private void Guard()
    {
        //워킹포인트의 반경보다 워크포인트와 좀비의 거리가 작으면
        if(Vector3.Distance(walkPoints[currentZombiePos].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePos = Random.Range(0, walkPoints.Length);  //좀비의 위치를 랜덤한 walkPoint로
            if(currentZombiePos >= walkPoints.Length)
            {
                currentZombiePos = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePos].transform.position, Time.deltaTime * zombieSpeed);


        //좀비 회전
        transform.LookAt(walkPoints[currentZombiePos].transform.position);
    }
/****************************************************************
 * 설명 : 좀비가 플레이어를 추적하도록 구현한다.
*****************************************************************/
    private void Pursueplayer()
    {
        if(zombieAgent.SetDestination(player.position))
        {
            //애니메이션
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Attacking", false);
            animator.SetBool("Die", false);

        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Attacking", false);
            animator.SetBool("Die", true);
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
        if(!isAttack)
        {
            Debug.Log("아니 왜 안돼?");
            RaycastHit hit;
            if(Physics.Raycast(AttackRayCastArea.transform.position, AttackRayCastArea.transform.forward, out hit, attackRadius))
            {
                Debug.Log("여기가 안타");
                //Debug.Log("Attack" + hit.transform.name);

                PlayerScript playerBody = hit.transform.GetComponent<PlayerScript>();

                if(playerBody != null)
                {
                    playerBody.PlayerHitDamage(damage);
                }
                //애니메이션
                animator.SetBool("Walking", false);
                animator.SetBool("Running", false);
                animator.SetBool("Attacking", true);
                animator.SetBool("Die", false);

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
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Attacking", false);
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
