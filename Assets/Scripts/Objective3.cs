/*****************************************************************
 * �� �� �� : Objective3.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.30
 * ��    �� : 2���� ��ǥ�� Ŭ���� �ǵ��� �����Ѵ�.
******************************************************************/
/*include�� ���*/
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
            //����Ʈ Ŭ����
            ObjectivesComplete.occurrence.GetObjectives(true, true, false, false, false);
            
            Destroy(gameObject, 2f);
           
        }
    }
}
