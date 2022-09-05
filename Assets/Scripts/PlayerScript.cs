/*****************************************************************
 * 코 드 명 : PlayerScript.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.22
 * 설    명 : 플레이어의 이동 및 중력을 구현한다. 13
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 플레이어를 제어한다.
*****************************************************************/
public class PlayerScript : MonoBehaviour
{
    [Header("움직임")]  
    public float playerSpeed = 1.9f;                        //플레이어 속도
    public float playerDash = 3f;                           //플레이어 대쉬

    [Header("플레이어 체력")]
    private float playerHealth = 100f;                       //플레이어 체력
    public float currentHealth;
    public GameObject playerDamage;
    public HealthBar healthBar;


    [Header("카메라")]
    public Transform playerCamera;                          //카메라 회전
    public GameObject EndGameMenuUI;                          //endgame


    [Header("플레이어 애니메이터 및 중력")]
    public CharacterController characterController;         //컨트롤러
    public float gravity = -9.81f;
    public Animator animator;

    [Header("점프 및 회전")]         
    public float turnCalmTime = 0.1f;                       //회전속도
    private float turnCalmVelocity;
    public float jumpRange = 1f;                            //점프 높이
    Vector3 velocity;                                       //점프 속도

    [Header("땅 체크")]
    public Transform groundCheck;                           //땅 체크하는 빈 오브젝트
    private bool onGround;                                  //땅인가?
    public float groundDistance = 0.4f;                     //점프 거리
    public LayerMask groundMask;                            //땅 레이어 마스크

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;               //커서 잠굼 ㅋ
        currentHealth = playerHealth;
        healthBar.GiveFullHealth(playerHealth);
    }

    private void Update()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0)     //땅 위에 있고, 속도가 0보다 작으면 
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
 * 설명 : 플레이어의 이동을 구현한다.(기본 이동)
*****************************************************************/
    void playerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");       //수평
        float v = Input.GetAxisRaw("Vertical");         //수직

        Vector3 direction = new Vector3(h, 0f, v);
        direction.Normalize(); //정규화

        
        if(direction.magnitude >= 0.1f) //이동이 있으면
        {

            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("IdleAim", false);
            
            //회전
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime); //미끄러지듯 스무스한 회전
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //이동
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
 * 설명 : 플레이어의 점프를 구현한다.
*****************************************************************/
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");


            Debug.Log("점프한다.");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);      //점프
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

/****************************************************************
 * 설명 : 플레이어의 대쉬를 구현한다.
*****************************************************************/
    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onGround)
        {
            float h = Input.GetAxisRaw("Horizontal");       //수평
            float v = Input.GetAxisRaw("Vertical");         //수직

            Vector3 direction = new Vector3(h, 0f, v);
            direction.Normalize(); //정규화

            if (direction.magnitude >= 0.1f) //이동이 있으면
            {
                //애니메이션
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true); 

                //회전
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime); //미끄러지듯 스무스한 회전
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                //이동
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
 * 설명 : 플레이어가 피격 당할 때를 구현한다.
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
 * 설명 : 플레이어의 죽을 떄를 구현한다.
*****************************************************************/
    private void PlayerDie()
    {
        EndGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;             //커서잠금 해제
        Object.Destroy(gameObject, 1.0f);
    }

/****************************************************************
 * 설명 : 플레이어가 피격 당할 때 UI 이미지를 코루탄으로 구현한다.
*****************************************************************/
    IEnumerator PlayerDamamge()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        playerDamage.SetActive(false);
    }


}
