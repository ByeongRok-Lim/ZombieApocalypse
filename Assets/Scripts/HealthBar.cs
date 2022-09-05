/*****************************************************************
 * 코 드 명 : HealthBar.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.28
 * 설    명 : 플레이어의 체력 바를 구현한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/****************************************************************
 * 설명 : 플레이어의 체력 바 UI를 구현한다.
*****************************************************************/
public class HealthBar : MonoBehaviour
{
    public Slider healthBarSilder;

    public void GiveFullHealth(float health)
    {
        healthBarSilder.maxValue = health;
        healthBarSilder.value = health;
    }

    public void SetHealth(float health)
    {
        healthBarSilder.value = health;
    }
}
