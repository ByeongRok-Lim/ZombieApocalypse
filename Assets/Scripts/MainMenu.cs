/*****************************************************************
 * 코 드 명 : MainMenu.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.29
 * 설    명 : 메인 메뉴 UI를 담당한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************************************************
 * 설명 : 메인 메뉴를 관리한다.
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
        Debug.Log("게임 나가는 중~");
        Application.Quit();
    }
}
