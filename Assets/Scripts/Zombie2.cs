/*****************************************************************
 * �� �� �� : Zombie2.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.24
 * ��    �� : ����2�� ���� �� ������ �ִϸ��̼��� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/****************************************************************
 * ���� : ���� ��Ʈ���Ѵ�.
*****************************************************************/
public class Zombie2 : MonoBehaviour
{
    [Header("���� ü�� �� ������")]
    public float zombieHealth = 100f;
    private float currentZombieHealth;
    public float damage = 5f;
    public HealthBar healthBar;

    [Header("����")]
    public NavMeshAgent zombieAgent;                                //����AI
    public Transform LookPoint;                                     //Ʈ������ ����
    public Camera AttackRayCastArea;
    public Transform player;
    public LayerMask playerLayer;                                   //�÷��̾� ���̾��ũ

    [Header("���� ���ִ� ����Ʈ")]
    public float zombieSpeed;                                       //������ �ӵ�

    [Header("���� ���� ����")]
    public float timeBtwAttack;                                     //������ ��Ÿ��
    private bool isAttack;                                          //������ ������?

    [Header("���� �ִϸ��̼�")]
    public Animator animator;

    [Header("���� ����")]
    public float visionRadius;                                      //���� �þ�(�÷��̾ ����� ������ ���� ȸ��)
    public float attackRadius;                                      //���� ���� �ݰ�
    public bool playerInvisionRadius;                               //�÷��̾ ���� �þ� �ݰ������ ���Դ��� �ƴ���.
    public bool playerInAttackRadius;                               //�÷��̾ ���� ���� �ݰ������ ���Դ��� �ƴ���.

    private void Awake()
    {
        //�ʱ�ȭ
        currentZombieHealth = zombieHealth;
        healthBar.GiveFullHealth(zombieHealth);
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);  //�þ߹ݰ�ȿ� �ִ�?
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);  //���ݹݰ�ȿ� �ִ�?

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
     * ���� : ������ ������ ���ִ� IDLE ���¸� �����Ѵ�.
    *****************************************************************/
    private void Idle()
    {
        zombieAgent.SetDestination(transform.position);
        animator.SetBool("Idle", true);
        animator.SetBool("Running", false);
    }
    /****************************************************************
     * ���� : ���� �÷��̾ �����ϵ��� �����Ѵ�.
    *****************************************************************/
    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(player.position))
        {
            //�ִϸ��̼�
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
            animator.SetBool("Attacking", false);

        }

        zombieAgent.SetDestination(player.position);
    }
    /****************************************************************
     * ���� : �÷��̾ �����ϴ� ����� �����Ѵ�.
    *****************************************************************/
    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!isAttack)
        {
            //Debug.Log("�ƴ� �� �ȵ�?");
            RaycastHit hit;
            if (Physics.Raycast(AttackRayCastArea.transform.position, AttackRayCastArea.transform.forward, out hit, attackRadius))
            {
                //Debug.Log("���Ⱑ ��Ÿ");
                //Debug.Log("Attack" + hit.transform.name);

                PlayerScript playerBody = hit.transform.GetComponent<PlayerScript>();

                if (playerBody != null)
                {
                    playerBody.PlayerHitDamage(damage);
                }
                //�ִϸ��̼�
                animator.SetBool("Attacking", true);
                animator.SetBool("Running", false);

            }

            isAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);             //??
        }
    }
    /****************************************************************
     * ���� : ���ݻ��¸� �����ϵ��� �����Ѵ�.
    *****************************************************************/
    private void ActiveAttacking()
    {
        isAttack = false;

    }

    /****************************************************************
     * ���� : ���� �������� �Դ´�.
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
     * ���� : ������ ���� ���� ������ �����Ѵ�.
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
