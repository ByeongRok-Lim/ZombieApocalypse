/*****************************************************************
 * �� �� �� : PlayerPunch.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.23
 * ��    �� : �÷��̾ ���Ⱑ ���� �� ��ġ�� �ϵ��� �����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : �÷��̾��� ��ġ�� �����Ѵ�.
*****************************************************************/
public class PlayerPunch : MonoBehaviour
{
    [Header("�÷��̾��� ��ġ")]
    public Camera cam;
    public float punchDamage = 10f;
    public float punchRange = 0.5f;

/****************************************************************
 * ���� : �� hit�� ���������� �ָ����� �����Ѵ�.
*****************************************************************/
    public void Punch()
    {
        RaycastHit hitInfo;     //������
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchRange)) //���ݹ��� ���� �����ɽ�Ʈ hit�Ǵ� �͵��� ����
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(punchDamage);

            }
            else if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(punchDamage);

            }
            else if (zombie2 != null)
            {
                zombie2.ZombieHitDamage(punchDamage);
            }

        }
    }
}
