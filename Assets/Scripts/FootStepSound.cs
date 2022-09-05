/*****************************************************************
 * �� �� �� : FootStepSound.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.07.01
 * ��    �� : �߼Ҹ��� event�� �°� ������ �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : �߼Ҹ��� �����Ѵ�.
*****************************************************************/
public class FootStepSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("�߼Ҹ� �ҽ�")]
    public AudioClip[] footstepsSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private AudioClip GetRandomFootStep()
    {
        return footstepsSound[Random.Range(0, footstepsSound.Length)];
    }
    private void Step()
    {
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }
}
