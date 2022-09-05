/*****************************************************************
 * 코 드 명 : Objective3.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.30
 * 설    명 : 2번쨰 목표가 클리어 되도록 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other2)
    {
        if (other2.gameObject.tag == "Player")
        {
            Debug.Log(ObjectivesComplete.occurrence);
            //퀘스트 클리어
            ObjectivesComplete.occurrence.GetObjectives(true, true, false, false, false);
            
            Destroy(gameObject, 2f);
           
        }
    }
}
