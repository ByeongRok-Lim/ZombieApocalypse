/*****************************************************************
 * �� �� �� : PlayerScript.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.22
 * ��    �� : �÷��̾��� �̵� �� �߷��� �����Ѵ�. 13
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : �÷��̾ �����Ѵ�.
*****************************************************************/
public class PlayerScript : MonoBehaviour
{
    [Header("������")]  
    public float playerSpeed = 1.9f;                        //�÷��̾� �ӵ�
    public float playerDash = 3f;                           //�÷��̾� �뽬

    [Header("�÷��̾� ü��")]
    private float playerHealth = 100f;                       //�÷��̾� ü��
    public float currentHealth;
    public GameObject playerDamage;
    public HealthBar healthBar;


    [Header("ī�޶�")]
    public Transform playerCamera;                          //ī�޶� ȸ��
    public GameObject EndGameMenuUI;                          //endgame


    [Header("�÷��̾� �ִϸ����� �� �߷�")]
    public CharacterController characterController;         //��Ʈ�ѷ�
    public float gravity = -9.81f;
    public Animator animator;

    [Header("���� �� ȸ��")]         
    public float turnCalmTime = 0.1f;                       //ȸ���ӵ�
    private float turnCalmVelocity;
    public float jumpRange = 1f;                            //���� ����
    Vector3 velocity;                                       //���� �ӵ�

    [Header("�� üũ")]
    public Transform groundCheck;                           //�� üũ�ϴ� �� ������Ʈ
    private bool onGround;                                  //���ΰ�?
    public float groundDistance = 0.4f;                     //���� �Ÿ�
    public LayerMask groundMask;                            //�� ���̾� ����ũ

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;               //Ŀ�� ��� ��
        currentHealth = playerHealth;
        healthBar.GiveFullHealth(playerHealth);
    }

    private void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0)     //�� ���� �ְ�, �ӵ��� 0���� ������ 
        {
            velocity.y = -2f;

        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        playerMove();
        Jump();
        Dash();
    }


/****************************************************************
 * ���� : �÷��̾��� �̵��� �����Ѵ�.(�⺻ �̵�)
*****************************************************************/
    void playerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");       //����
        float v = Input.GetAxisRaw("Vertical");         //����

        Vector3 direction = new Vector3(h, 0f, v);
        direction.Normalize(); //����ȭ

        
        if(direction.magnitude >= 0.1f) //�̵��� ������
        {

            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("IdleAim", false);
            
            //ȸ��
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime); //�̲������� �������� ȸ��
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //�̵�
            characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }

    }

/****************************************************************
 * ���� : �÷��̾��� ������ �����Ѵ�.
*****************************************************************/
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");


            Debug.Log("�����Ѵ�.");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);      //����
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

/****************************************************************
 * ���� : �÷��̾��� �뽬�� �����Ѵ�.
*****************************************************************/
    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onGround)
        {
            float h = Input.GetAxisRaw("Horizontal");       //����
            float v = Input.GetAxisRaw("Vertical");         //����

            Vector3 direction = new Vector3(h, 0f, v);
            direction.Normalize(); //����ȭ

            if (direction.magnitude >= 0.1f) //�̵��� ������
            {
                //�ִϸ��̼�
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true); 

                //ȸ��
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime); //�̲������� �������� ȸ��
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                //�̵�
                characterController.Move(moveDirection * playerDash * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);
            }
        }
    }

/****************************************************************
 * ���� : �÷��̾ �ǰ� ���� ���� �����Ѵ�.
*****************************************************************/
    public void PlayerHitDamage(float ZombieDamage)
    {
        currentHealth -= ZombieDamage;
        StartCoroutine(PlayerDamamge());

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            PlayerDie();
        }
    }

/****************************************************************
 * ���� : �÷��̾��� ���� ���� �����Ѵ�.
*****************************************************************/
    private void PlayerDie()
    {
        EndGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;             //Ŀ����� ����
        Object.Destroy(gameObject, 1.0f);
    }

/****************************************************************
 * ���� : �÷��̾ �ǰ� ���� �� UI �̹����� �ڷ�ź���� �����Ѵ�.
*****************************************************************/
    IEnumerator PlayerDamamge()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);
    }


}
