/*****************************************************************
 * 코 드 명 : Objective2.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.30
 * 설    명 : 4번쨰 목표가 클리어 되도록 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 플레이어가 box를 지나면 이벤트가 실행한다.
*****************************************************************/
public class Objective2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //퀘스트 클리어
            ObjectivesComplete.occurrence.GetObjectives(true, true, true, true, false);

            Destroy(gameObject, 2f);
        }
    }
}
