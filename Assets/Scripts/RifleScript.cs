/*****************************************************************
 * 코 드 명 : RafleScript.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.22
 * 설    명 : 총과 총알을 제어하기 위해 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 총을 제어한다.
*****************************************************************/
public class RifleScript : MonoBehaviour
{
    [Header("총 정보")]
    public Camera cam;

    public float damage = 10f;                  //데미지
    public float shootingRange = 100f;          //공격범위
    public float fireCharge = 15f;              
    [SerializeField]
    private float nextTimeShoot = 0f;           //다음 발사까지 걸리는 시간

    public Animator animator;

    public PlayerScript player;                 //플레이어 참조
    public Transform hand;
    public GameObject rifleUI;


    [Header("총알 및 발사")]
    private int maxBullet = 40;                 //한 탄창의 최대 총알 갯수
    public int magazine = 20;                   //탄창
    private int currentBullet;                  //현재 총알 갯수
    public float reloadingTime = 1.5f;          //장전에 걸리는 시간
    private bool setReloading = false;          //장전중인지?


    [Header("총 이펙트")]
    public ParticleSystem muzzleSpark;          //피격스파크
    public GameObject woodedEffect;             //나무쏘면 나오는 이펙트
    public GameObject bloodEffect;              //좀비쏘면 나오는 피 이펙트


    [Header("총 사운드 및 UI")]
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
            StartCoroutine(Reload());               //총알이 0이하일 때 재장전 코루틴 호출
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
 * 설명 : 총알 발사를 구현한다.
*****************************************************************/
    private void Shoot()
    {
        //매거진이 1개 이상인지 화긴
        if(magazine == 0)
        {
            //총알 없는 텍스쳐 표시
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
        RaycastHit hitInfo;     //맞은놈
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange)) //공격범위 내에 레이케스트 hit되는 것들의 정보
        {
            Debug.Log(hitInfo.transform.name);          //위치는 position.

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
 * 설명 : 총알을 장전하는 코루틴
*****************************************************************/
    //네이버 블로그 참조
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerDash = 0f;
        setReloading = true;
        if(magazine <= 0)
        {
            Debug.Log("탄창이 없다잇! 장전 실패!!!!!!");
        }
        else
        {
            Debug.Log("장전중주웆우중중");
        }


        //애니메이션 플레이
        animator.SetBool("Reloading", true);

        //사운드..
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
