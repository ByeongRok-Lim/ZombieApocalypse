/*****************************************************************
 * �� �� �� : Objective2.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.30
 * ��    �� : 4���� ��ǥ�� Ŭ���� �ǵ��� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : �÷��̾ box�� ������ �̺�Ʈ�� �����Ѵ�.
*****************************************************************/
public class Objective2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //����Ʈ Ŭ����
            ObjectivesComplete.occurrence.GetObjectives(true, true, true, true, false);

            Destroy(gameObject, 2f);
        }
    }
}
