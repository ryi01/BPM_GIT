using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 버튼 관리용 스크립트
public class PauseButton : MonoBehaviour
{
    public GameObject pauseUI;
    // continue
    public void Continue()
    {
        Time.timeScale = 1;
        GameManager.Instance.m_state = GameManager.GameState.Playing;
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
    }
    // restart
    public void Restart()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        SceneManager.LoadScene("TotalMap");
    }
    // mainmenu
    public void MainMenu()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        SceneManager.LoadScene("Main");
    }
}
