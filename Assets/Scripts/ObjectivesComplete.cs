/*****************************************************************
 * �� �� �� : ObjectivesComplete.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.30
 * ��    �� : ��ǥ�� Ŭ�����ϱ����� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/****************************************************************
 * ���� : ��ǥ�� Ŭ�����ϵ��� �����Ѵ�.
*****************************************************************/
public class ObjectivesComplete : MonoBehaviour
{
    [Header("�Ϸ��ؾ��� ��ǥ")]
    public Text Objective1;
    public Text Objective2;
    public Text Objective3;
    public Text Objective4;
    public Text Objective5;

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public void GetObjectives(bool obj1, bool obj2, bool obj3, bool obj4, bool obj5)
    {
        //����Ʈ 1
        if(obj1 == true)
        {
            Objective1.text = "01. Mission Completed";
            Objective1.color = Color.green;
        }
        else
        {
            Objective1.text = "01. Ride a car in front of you";
            Objective1.color = Color.white;
        }
        //����Ʈ 2
        if (obj2 == true)
        {
            Objective2.text = "02. Mission Completed";
            Objective2.color = Color.green;

        }
        else
        {
            Objective2.text = "02. Find the city entry";
            Objective2.color = Color.white;
        }
        //����Ʈ 3
        if (obj3 == true)
        {
            Objective3.text = "03. Mission Completed";
            Objective3.color = Color.green;

        }
        else
        {
            Objective3.text = "03. find the rifle in the villa";
            Objective3.color = Color.white;
        }
        //����Ʈ 4
        if (obj4 == true)
        {
            Objective4.text = "04. Mission Completed";
            Objective4.color = Color.green;

        }
        else
        {
            Objective4.text = "04. find and save five hostages";
            Objective4.color = Color.white;
        }
        //����Ʈ 5
        if (obj5 == true)
        {
            Objective5.text = "05. Completed";
            Objective5.color = Color.green;

        }
        else
        {
            Objective5.text = "05. Kill A Boss Zombie a.k.a MuTant";
            Objective5.color = Color.white;
        }
    }
}
