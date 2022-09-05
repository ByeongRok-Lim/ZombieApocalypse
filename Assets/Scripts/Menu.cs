/*****************************************************************
 * 코 드 명 : Menu.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.29
 * 설    명 : 메뉴 UI를 담당한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************************************************
 * 설명 : 전체적인 메뉴를 관리하도록 구현한다.
*****************************************************************/
public class Menu : MonoBehaviour
{
    [Header("메뉴")]
    public GameObject pauseMenuUI;
    public GameObject endGameMenuUI;
    public GameObject objectiveMenuUI;

    public static bool gameIsStopped = false;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;

            }
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            if(gameIsStopped)
            {
                RemoveObjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                ShowObjectives();
                Cursor.lockState = CursorLockMode.None;

            }
        }

    }


/****************************************************************
 * 설명 : 목표 UI를 보여준다.
*****************************************************************/
    public void ShowObjectives()
    {
        objectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsStopped = true;
    }

/****************************************************************
 * 설명 : 목표 UI를 지운다.
*****************************************************************/
    public void RemoveObjectives()
    {
        objectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsStopped = false;
    }

/****************************************************************
 * 설명 : 게임을 계속한다.
*****************************************************************/
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsStopped = false;
    }
/****************************************************************
 * 설명 : 게임음 재시작한다.
*****************************************************************/
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }

/****************************************************************
 * 설명 : 메뉴를 로드한다.
*****************************************************************/
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

/****************************************************************
 * 설명 : 게임에서 나간다.
*****************************************************************/
    public void QuitGame()
    {
        Debug.Log("게임 나가는 중~");
        Application.Quit();
    }

/****************************************************************
 * 설명 : 게임을 일시정지한다.
*****************************************************************/
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsStopped = true;
    }

}
