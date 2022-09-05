/*****************************************************************
 * 코 드 명 : HealthBoost.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.07.01
 * 설    명 : 체력을 회복하는 키트를 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 체력을 회복하는 로직을 구현한다.
*****************************************************************/
public class HealthBoost : MonoBehaviour
{
    [Header("health 부스터")]
    public PlayerScript player;
    private float healthTogive = 120f;
    private float radius = 2.5f;

    [Header("사운드")]
    public AudioClip HealthBoostSound;
    public AudioSource audioSource;

    [Header("헬스박스 애니메이터")]
    public Animator animator;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)< radius)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                player.currentHealth = healthTogive;

                //사운드
                audioSource.PlayOneShot(HealthBoostSound);

                Object.Destroy(gameObject, 1.5f);
            }
        }
    }
}
