/*****************************************************************
 * 코 드 명 : FootStepSound.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.07.01
 * 설    명 : 발소리를 event에 맞게 나도록 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 발소리를 구현한다.
*****************************************************************/
public class FootStepSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("발소리 소스")]
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
