/*****************************************************************
 * 코 드 명 : ZombieSpawn.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.25
 * 설    명 : 좀비들이 일정구역을 지나면 스폰된다. 45
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 좀비들이 스폰된다.
*****************************************************************/
public class BossSpawner : MonoBehaviour
{
    public GameObject colliders;
    public GameObject Boss;

    [Header("사운드")]
    public AudioClip BossZoneSound;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider col)
    {
        colliders.SetActive(true);
        Boss.SetActive(true);
        audioSource.PlayOneShot(BossZoneSound);
        Debug.Log("벽활성화");
        Destroy(gameObject, 1f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    //[Header("보스 스폰 지역 변수")]
    //public GameObject bossPrefab;                     //좀비
    //public Transform bossSpawnPosition;               //위치
    ////public GameObject dangerZone;
    //private float repeatTime = 1f;

    //private void OnTriggerEnter(Collider col)             //콜라이더 박스 지나가면
    //{
    //    if (col.gameObject.tag == "Player")                //부딪힌 태그가 "플레이어"면
    //    {
    //        Debug.Log("플레이어 지나감");
    //        InvokeRepeating("Boss", 1f, repeatTime);
    //        Destroy(gameObject, 1f);
    //        gameObject.GetComponent<BoxCollider>().enabled = false;
    //    }


    //}

    //void Boss()
    //{
    //    Instantiate(bossPrefab, bossSpawnPosition.position, bossSpawnPosition.rotation);
    //}
}