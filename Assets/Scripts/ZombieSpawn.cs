/*****************************************************************
 * �� �� �� : ZombieSpawn.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.25
 * ��    �� : ������� ���������� ������ �����ȴ�. 45
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : ������� �����ȴ�.
*****************************************************************/
public class ZombieSpawn : MonoBehaviour
{
    [Header("���� ���� ���� ����")]
    public GameObject zombiePrefab;                     //����
    public Transform zombieSpawnPosition;               //��ġ
    public GameObject dangerZone1;
    private float repeatCycle = 1f;

    [Header("����")]
    public AudioClip DangerZoneSound;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider col)             //�ݶ��̴� �ڽ� ��������
    {
        if(col.gameObject.tag == "Player")                //�ε��� �±װ� "�÷��̾�"��
        {
            Debug.Log("�÷��̾� ������");
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
