/*****************************************************************
 * �� �� �� : MainMenu.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.29
 * ��    �� : ���� �޴� UI�� ����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************************************************
 * ���� : ���� �޴��� �����Ѵ�.
*****************************************************************/
public class MainMenu : MonoBehaviour
{
    public GameObject selectCharacter;
    public GameObject mainMenu;

    public void OnSelectCharacter()
    {
        selectCharacter.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("ZombieLand 1");
    }

    public void OnQuitButton()
    {
        Debug.Log("���� ������ ��~");
        Application.Quit();
    }
}
