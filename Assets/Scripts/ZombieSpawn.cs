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
public class ZombieSpawn : MonoBehaviour
{
    [Header("좀비 스폰 지역 변수")]
    public GameObject zombiePrefab;                     //좀비
    public Transform zombieSpawnPosition;               //위치
    public GameObject dangerZone1;
    private float repeatCycle = 1f;

    [Header("사운드")]
    public AudioClip DangerZoneSound;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider col)             //콜라이더 박스 지나가면
    {
        if(col.gameObject.tag == "Player")                //부딪힌 태그가 "플레이어"면
        {
            Debug.Log("플레이어 지나감");
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            audioSource.PlayOneShot(DangerZoneSound);
            StartCoroutine(DangerZoneTimer());
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }

    IEnumerator DangerZoneTimer()
    {
        dangerZone1.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerZone1.SetActive(false);
    }
}
