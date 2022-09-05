/*****************************************************************
 * �� �� �� : HealthBoost.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.07.01
 * ��    �� : ü���� ȸ���ϴ� ŰƮ�� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : ü���� ȸ���ϴ� ������ �����Ѵ�.
*****************************************************************/
public class HealthBoost : MonoBehaviour
{
    [Header("health �ν���")]
    public PlayerScript player;
    private float healthTogive = 120f;
    private float radius = 2.5f;

    [Header("����")]
    public AudioClip HealthBoostSound;
    public AudioSource audioSource;

    [Header("�ｺ�ڽ� �ִϸ�����")]
    public Animator animator;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)< radius)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                player.currentHealth = healthTogive;

                //����
                audioSource.PlayOneShot(HealthBoostSound);

                Object.Destroy(gameObject, 1.5f);
            }
        }
    }
}
