/*****************************************************************
 * 코 드 명 : AmmoBoost.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.07.01
 * 설    명 : 탄창을 획득할 수 있도록 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    [Header("탄약 부스터")]
    public RifleScript rifle;
    private int magToGive = 15;
    private float radius = 2.5f;

    [Header("사운드")]
    public AudioClip AmmoBoostSound;
    public AudioSource audioSource;

    [Header("헬스박스 애니메이터")]
    public Animator animator;

    private void Update()
    {
        if (Vector3.Distance(transform.position, rifle.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                rifle.magazine = magToGive;

                //사운드
                audioSource.PlayOneShot(AmmoBoostSound);

                Object.Destroy(gameObject, 1.5f);
            }
        }
    }
}
