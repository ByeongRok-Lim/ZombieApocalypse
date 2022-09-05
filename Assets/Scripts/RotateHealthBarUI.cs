/*****************************************************************
 * �� �� �� : RotateHealthBarUI.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.28
 * ��    �� : ������ ü�¹� UI�� ȸ�� ��Ű���� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : ü�¹� UI�� ȸ�� ��Ű���� �����Ѵ�.
*****************************************************************/
public class RotateHealthBarUI : MonoBehaviour
{
    public Transform MainCamera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + MainCamera.forward);
    }
}
