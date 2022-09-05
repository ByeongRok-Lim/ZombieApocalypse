/*****************************************************************
 * 코 드 명 : RiflePickup.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.23
 * 설    명 : 총을 줍는다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 플레이어가 반경안에 들어와서 F키를 누르면 총을 줍는다.
*****************************************************************/
public class RiflePickup : MonoBehaviour
{
    [Header("총의 정보")]
    public GameObject PlayerRifle;                  //플레이어 총
    public GameObject PickupRifle;                  //픽업할 총
    public PlayerPunch playerPunch;                 //총이 없고 주먹일때만
    public GameObject rifleUI;


    [Header("픽업하기 위한 정보")]
    public PlayerScript player;                     //플레이어 참조
    private float radius = 2.5f;                    //반경
    public Animator animator;

    private float nextTimePunch = 0f;               //펀치 딜레이
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

        //총과 플레이어의 거리가 radius보다 짧으면
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                PlayerRifle.SetActive(true);
                PickupRifle.SetActive(false);
                //사운드

                //퀘스트 클리어
                ObjectivesComplete.occurrence.GetObjectives(true, true, true, false, false);
            }
        }
    }
}
