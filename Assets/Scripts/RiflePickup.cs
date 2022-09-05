/*****************************************************************
 * �� �� �� : RiflePickup.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.23
 * ��    �� : ���� �ݴ´�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : �÷��̾ �ݰ�ȿ� ���ͼ� FŰ�� ������ ���� �ݴ´�.
*****************************************************************/
public class RiflePickup : MonoBehaviour
{
    [Header("���� ����")]
    public GameObject PlayerRifle;                  //�÷��̾� ��
    public GameObject PickupRifle;                  //�Ⱦ��� ��
    public PlayerPunch playerPunch;                 //���� ���� �ָ��϶���
    public GameObject rifleUI;


    [Header("�Ⱦ��ϱ� ���� ����")]
    public PlayerScript player;                     //�÷��̾� ����
    private float radius = 2.5f;                    //�ݰ�
    public Animator animator;

    private float nextTimePunch = 0f;               //��ġ ������
    public float punchCharge = 15f;

    private void Awake()
    {
        Debug.Log(PlayerRifle, gameObject);
        PlayerRifle.SetActive(false);
        rifleUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimePunch)
        {
            animator.SetBool("Punch", true);
            animator.SetBool("Idle", false);
            nextTimePunch = Time.time + 1f/punchCharge;

            playerPunch.Punch();
        }
        else
        {
            animator.SetBool("Punch", false);
            animator.SetBool("Idle", true);
        }

        //�Ѱ� �÷��̾��� �Ÿ��� radius���� ª����
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                PlayerRifle.SetActive(true);
                PickupRifle.SetActive(false);
                //����

                //����Ʈ Ŭ����
                ObjectivesComplete.occurrence.GetObjectives(true, true, true, false, false);
            }
        }
    }
}
