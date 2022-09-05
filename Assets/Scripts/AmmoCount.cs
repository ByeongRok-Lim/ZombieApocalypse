/*****************************************************************
 * 코 드 명 : AmmoCount.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.29
 * 설    명 : UI의 총알과 탄창을 담당한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/****************************************************************
 * 설명 : 총알과 탄창의 갯수를 구한다.
*****************************************************************/
public class AmmoCount : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI magText;

    public static AmmoCount occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public void UpdateAmmoText(int currnetAmmo)
    {
        ammoText.text = "Ammo. " + currnetAmmo;
    }

    public void UpdateMagText(int mag)
    {
        magText.text = "Magines. " + mag;
    }
}
