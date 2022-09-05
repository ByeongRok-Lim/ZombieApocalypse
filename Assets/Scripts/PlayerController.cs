using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("������")]
    public float playerSpeed = 1.9f;                //�÷��̾� �ӵ�

    [Header("ī�޶�")]
    public Transform playerCamera;               //ī�޶� ȸ��

    [Header("ȸ��")]
    public float turnCalmTime = 0.1f;               //ȸ���ӵ�
    float turnCalmVelocity;

    [Header("�� üũ")]
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


    // ���� üũ
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y);
    }


    // ������ ����
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");       //����
        float v = Input.GetAxisRaw("Vertical");         //����

        Vector3 direction = new Vector3(h, 0f, v);
        direction.Normalize(); //����ȭ
        


        if (direction.magnitude >= 0.1f) //�̵��� ������
        {

            //ȸ��
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime); //�̲������� �������� ȸ��
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //�̵�
            myRigid.MovePosition(transform.position + moveDirection * playerSpeed * Time.deltaTime);
        }
    }

    // �����õ�
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
