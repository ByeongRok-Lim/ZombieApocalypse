/*****************************************************************
 * �� �� �� : Objective4.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.30
 * ��    �� : 2���� ��ǥ�� Ŭ���� �ǵ��� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
    public GameObject col;
    public GameObject Boss;
    private void OnTriggerEnter(Collider other2)
    {
        if(!col && !Boss)
        {
            if (other2.gameObject.tag == "Player")
            {
                Debug.Log(ObjectivesComplete.occurrence);
                //����Ʈ Ŭ����
                ObjectivesComplete.occurrence.GetObjectives(true, true, true, true, true);

                Destroy(gameObject, 2f);

                SceneManager.LoadScene("MainMenu");
            }
        }
        
    }
}
