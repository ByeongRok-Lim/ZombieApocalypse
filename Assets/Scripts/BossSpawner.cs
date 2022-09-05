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
public class BossSpawner : MonoBehaviour
{
    public GameObject colliders;
    public GameObject Boss;

    [Header("����")]
    public AudioClip BossZoneSound;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider col)
    {
        colliders.SetActive(true);
        Boss.SetActive(true);
        audioSource.PlayOneShot(BossZoneSound);
        Debug.Log("��Ȱ��ȭ");
        Destroy(gameObject, 1f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    //[Header("���� ���� ���� ����")]
    //public GameObject bossPrefab;                     //����
    //public Transform bossSpawnPosition;               //��ġ
    ////public GameObject dangerZone;
    //private float repeatTime = 1f;

    //private void OnTriggerEnter(Collider col)             //�ݶ��̴� �ڽ� ��������
    //{
    //    if (col.gameObject.tag == "Player")                //�ε��� �±װ� "�÷��̾�"��
    //    {
    //        Debug.Log("�÷��̾� ������");
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