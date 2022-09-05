/*****************************************************************
 * �� �� �� : AmmoCount.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.29
 * ��    �� : UI�� �Ѿ˰� źâ�� ����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/****************************************************************
 * ���� : �Ѿ˰� źâ�� ������ ���Ѵ�.
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
