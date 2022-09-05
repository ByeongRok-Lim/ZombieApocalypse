/*****************************************************************
 * �� �� �� : Menu.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.29
 * ��    �� : �޴� UI�� ����Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************************************************
 * ���� : ��ü���� �޴��� �����ϵ��� �����Ѵ�.
*****************************************************************/
public class Menu : MonoBehaviour
{
    [Header("�޴�")]
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
 * ���� : ��ǥ UI�� �����ش�.
*****************************************************************/
    public void ShowObjectives()
    {
        objectiveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsStopped = true;
    }

/****************************************************************
 * ���� : ��ǥ UI�� �����.
*****************************************************************/
    public void RemoveObjectives()
    {
        objectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsStopped = false;
    }

/****************************************************************
 * ���� : ������ ����Ѵ�.
*****************************************************************/
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsStopped = false;
    }
/****************************************************************
 * ���� : ������ ������Ѵ�.
*****************************************************************/
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }

/****************************************************************
 * ���� : �޴��� �ε��Ѵ�.
*****************************************************************/
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

/****************************************************************
 * ���� : ���ӿ��� ������.
*****************************************************************/
    public void QuitGame()
    {
        Debug.Log("���� ������ ��~");
        Application.Quit();
    }

/****************************************************************
 * ���� : ������ �Ͻ������Ѵ�.
*****************************************************************/
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsStopped = true;
    }

}
