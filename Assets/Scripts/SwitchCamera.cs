/****************************************************************************************
 * �� �� �� : SwitchCamera.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.22
 * ��    �� : �����ʸ��콺 Ŭ�� �� �ó׸ӽ� ī�޶� on/off
*****************************************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : ī�޶� ��ü�Ѵ�.
*****************************************************************/
public class SwitchCamera : MonoBehaviour
{
    [Header("��ǥ ī�޶�")]
    public GameObject AimCamera;                    //��ǥ ī�޶�
    public GameObject AimCanvas;                    //��ǥ ĵ����
    public GameObject TPSCamera;                    //3��Ī ī�޶�
    public GameObject TPSCanvas;                    //3��Ī ĵ����

    [Header("ī�޶� �ִϸ�����")]
    public Animator animator;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", true);
            animator.SetBool("Walk", true);

            TPSCamera.SetActive(false);
            TPSCanvas.SetActive(false);
            AimCamera.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else if(Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("Walk", false);

            TPSCamera.SetActive(false);
            TPSCanvas.SetActive(false);
            AimCamera.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("IdleAim", false);
            animator.SetBool("RifleWalk", false);

            TPSCamera.SetActive(true);
            TPSCanvas.SetActive(true);
            AimCamera.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }

}
