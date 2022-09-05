/*****************************************************************
 * 코 드 명 : PlayerPunch.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.23
 * 설    명 : 플레이어가 무기가 없을 때 펀치를 하도록 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 플레이어의 펀치를 구현한다.
*****************************************************************/
public class PlayerPunch : MonoBehaviour
{
    [Header("플레이어의 펀치")]
    public Camera cam;
    public float punchDamage = 10f;
    public float punchRange = 0.5f;

/****************************************************************
 * 설명 : 총 hit와 마찬가지로 주먹으로 공격한다.
*****************************************************************/
    public void Punch()
    {
        RaycastHit hitInfo;     //맞은놈
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchRange)) //공격범위 내에 레이케스트 hit되는 것들의 정보
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(punchDamage);

            }
            else if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(punchDamage);

            }
            else if (zombie2 != null)
            {
                zombie2.ZombieHitDamage(punchDamage);
            }

        }
    }
}
