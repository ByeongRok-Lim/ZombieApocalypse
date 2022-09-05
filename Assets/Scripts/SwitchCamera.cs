/****************************************************************************************
 * 코 드 명 : SwitchCamera.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.22
 * 설    명 : 오른쪽마우스 클릭 시 시네머신 카메라 on/off
*****************************************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 카메라를 교체한다.
*****************************************************************/
public class SwitchCamera : MonoBehaviour
{
    [Header("목표 카메라")]
    public GameObject AimCamera;                    //목표 카메라
    public GameObject AimCanvas;                    //목표 캔버스
    public GameObject TPSCamera;                    //3인칭 카메라
    public GameObject TPSCanvas;                    //3인칭 캔버스

    [Header("카메라 애니메이터")]
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
