/*****************************************************************
 * �� �� �� : AmmoBoost.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.07.01
 * ��    �� : źâ�� ȹ���� �� �ֵ��� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    [Header("ź�� �ν���")]
    public RifleScript rifle;
    private int magToGive = 15;
    private float radius = 2.5f;

    [Header("����")]
    public AudioClip AmmoBoostSound;
    public AudioSource audioSource;

    [Header("�ｺ�ڽ� �ִϸ�����")]
    public Animator animator;

    private void Update()
    {
        if (Vector3.Distance(transform.position, rifle.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                rifle.magazine = magToGive;

                //����
                audioSource.PlayOneShot(AmmoBoostSound);

                Object.Destroy(gameObject, 1.5f);
            }
        }
    }
}
