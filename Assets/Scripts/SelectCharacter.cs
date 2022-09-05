/*****************************************************************
 * 코 드 명 : SelectCharacter.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.29
 * 설    명 : 캐릭터를 선택한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************************************************
 * 설명 : 캐릭터를 선택하여 다음 씬으로 넘어갈 수 있도록 한다.
*****************************************************************/
public class SelectCharacter : MonoBehaviour
{

    public GameObject selectCharacter;
    public GameObject mainMenu;
    public void OnBackButton()
    {
        selectCharacter.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnCharacter1()
    {
        SceneManager.LoadScene("ZombieLand 1");
    }
    public void OnCharacter2()
    {
        SceneManager.LoadScene("ZombieLand 2");
    }
    public void OnCharacter3()
    {
        SceneManager.LoadScene("ZombieLand 3");
    }
}
