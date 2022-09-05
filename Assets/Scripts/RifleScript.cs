/*****************************************************************
 * �� �� �� : RafleScript.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.22
 * ��    �� : �Ѱ� �Ѿ��� �����ϱ� ���� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : ���� �����Ѵ�.
*****************************************************************/
public class RifleScript : MonoBehaviour
{
    [Header("�� ����")]
    public Camera cam;

    public float damage = 10f;                  //������
    public float shootingRange = 100f;          //���ݹ���
    public float fireCharge = 15f;              
    [SerializeField]
    private float nextTimeShoot = 0f;           //���� �߻���� �ɸ��� �ð�

    public Animator animator;

    public PlayerScript player;                 //�÷��̾� ����
    public Transform hand;
    public GameObject rifleUI;


    [Header("�Ѿ� �� �߻�")]
    private int maxBullet = 40;                 //�� źâ�� �ִ� �Ѿ� ����
    public int magazine = 20;                   //źâ
    private int currentBullet;                  //���� �Ѿ� ����
    public float reloadingTime = 1.5f;          //������ �ɸ��� �ð�
    private bool setReloading = false;          //����������?


    [Header("�� ����Ʈ")]
    public ParticleSystem muzzleSpark;          //�ǰݽ���ũ
    public GameObject woodedEffect;             //������� ������ ����Ʈ
    public GameObject bloodEffect;              //������ ������ �� ����Ʈ


    [Header("�� ���� �� UI")]
    public GameObject AmmoOutUi;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public AudioSource audioSource;

    private void Awake()
    {
        transform.SetParent(hand);
        rifleUI.SetActive(true);
        currentBullet = maxBullet;
    }

    private void Update()
    {
        if(setReloading)
        {
            return;
        }

        if(currentBullet <= 0)
        {
            StartCoroutine(Reload());               //�Ѿ��� 0������ �� ������ �ڷ�ƾ ȣ��
            return;
        }

        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);

            nextTimeShoot = Time.time + 1f/fireCharge;
            Shoot();
        }
        else if(Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if(Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);

        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
        }
    }

/****************************************************************
 * ���� : �Ѿ� �߻縦 �����Ѵ�.
*****************************************************************/
    private void Shoot()
    {
        //�Ű����� 1�� �̻����� ȭ��
        if(magazine == 0)
        {
            //�Ѿ� ���� �ؽ��� ǥ��
            StartCoroutine(ShowAmmoOut());
            return;
        }
        currentBullet--;
        if(currentBullet == 0)
        {
            magazine--;
        }

        //ui
        AmmoCount.occurrence.UpdateAmmoText(currentBullet);
        AmmoCount.occurrence.UpdateMagText(magazine);


        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;     //������
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange)) //���ݹ��� ���� �����ɽ�Ʈ hit�Ǵ� �͵��� ����
        {
            Debug.Log(hitInfo.transform.name);          //��ġ�� position.

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(damage);
                GameObject woodGo = Instantiate(woodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(woodGo, 1f);
            }

            else if(zombie1 != null)
            {
                zombie1.ZombieHitDamage(damage);
                GameObject bloodEffectGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodEffectGo, 1f);
            }
            else if(zombie2 != null)
            {
                zombie2.ZombieHitDamage(damage);
                GameObject bloodEffectGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodEffectGo, 1f);
            }
        }
    }

/****************************************************************
 * ���� : �Ѿ��� �����ϴ� �ڷ�ƾ
*****************************************************************/
    //���̹� ��α� ����
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerDash = 0f;
        setReloading = true;
        if(magazine <= 0)
        {
            Debug.Log("źâ�� ������! ���� ����!!!!!!");
        }
        else
        {
            Debug.Log("�������֟T������");
        }


        //�ִϸ��̼� �÷���
        animator.SetBool("Reloading", true);

        //����..
        audioSource.PlayOneShot(reloadingSound);

        yield return new WaitForSeconds(reloadingTime);

        animator.SetBool("Reloading", false);
        currentBullet = maxBullet;
        player.playerSpeed = 1.9f;
        player.playerDash = 3f;
        setReloading = false;
        
    }

    IEnumerator ShowAmmoOut()
    {
        AmmoOutUi.SetActive(true);
        yield return new WaitForSeconds(5f);
        AmmoOutUi.SetActive(false);
    }

}
