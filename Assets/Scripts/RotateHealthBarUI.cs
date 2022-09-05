/*****************************************************************
 * 코 드 명 : RotateHealthBarUI.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.28
 * 설    명 : 좀비의 체력바 UI를 회전 시키도록 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 체력바 UI를 회전 시키도록 구현한다.
*****************************************************************/
public class RotateHealthBarUI : MonoBehaviour
{
    public Transform MainCamera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + MainCamera.forward);
    }
}
