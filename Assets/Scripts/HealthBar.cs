/*****************************************************************
 * �� �� �� : HealthBar.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.28
 * ��    �� : �÷��̾��� ü�� �ٸ� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/****************************************************************
 * ���� : �÷��̾��� ü�� �� UI�� �����Ѵ�.
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
