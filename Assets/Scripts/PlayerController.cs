using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("움직임")]
    public float playerSpeed = 1.9f;                //플레이어 속도

    [Header("카메라")]
    public Transform playerCamera;               //카메라 회전

    [Header("회전")]
    public float turnCalmTime = 0.1f;               //회전속도
    float turnCalmVelocity;

    [Header("땅 체크")]
    public bool isGround = true;
    private CapsuleCollider capsuleCollider;
    private Rigidbody myRigid;

    public float jumpForce;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        IsGround();
        Move();
        TryJump();
    }


    // 지면 체크
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y);
    }


    // 움직임 실행
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");       //수평
        float v = Input.GetAxisRaw("Vertical");         //수직

        Vector3 direction = new Vector3(h, 0f, v);
        direction.Normalize(); //정규화
        


        if (direction.magnitude >= 0.1f) //이동이 있으면
        {

            //회전
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime); //미끄러지듯 스무스한 회전
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //이동
            myRigid.MovePosition(transform.position + moveDirection * playerSpeed * Time.deltaTime);
        }
    }

    // 점프시도
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&isGround)
        {
            Jump();
        }
    }
    void Jump()
    {
        Debug.Log("space Key");
        Debug.Log(isGround);
        myRigid.velocity = transform.up * jumpForce;
    }
}
