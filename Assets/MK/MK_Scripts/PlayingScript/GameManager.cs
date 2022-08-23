using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �Ŵ��� : �������� �ý��� ���� 
// 1. Ready 2. Play 3. Stop(��� �Ѿ�� ��)
public class GameManager : MonoBehaviour
{
    // �̱��� 
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    // �Ͻ�������
    bool isPause = false;
    // UI ����
    public GameObject pauseUI;
    // UI
    public GameObject playingUI;
    public GameObject noteUI;
    public GameObject readyUI;
    public GameObject bossUI;
    // enum
    public enum GameState
    {
        Ready,
        Playing,
        Stop,
        Pause
    }
    // �ʹݿ��� ready���·�
    public GameState m_state = GameState.Ready;

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case GameState.Ready:
                ReadyState();
                break;
            case GameState.Playing:
                PlayingState();
                break;
            case GameState.Stop:
                StopState();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_state = GameState.Pause;
            PauseState();
        }
    }

    // Play ��ư ����, ��� ����ٰ� Playing���� �Ѿ��
    // �ʿ�Ӽ� : ��� �ð�, ���ߴ� �ð�
    [SerializeField]
    public float readyTime = 3;
    float currentTime;
    private void ReadyState()
    {
        // �ð��� �帣��
        currentTime += Time.deltaTime;
        readyUI.SetActive(true);
        playingUI.SetActive(false);
        noteUI.SetActive(false);
        // ���� �ð��� �帣�� �Ǹ�
        if (currentTime > readyTime)
        {
            // ���¸� �÷������� �ٲ�
            currentTime = 0;
            m_state = GameState.Playing;
        }
    }

    private void PlayingState()
    {
        readyUI.SetActive(false);
        playingUI.SetActive(true);
        noteUI.SetActive(true);
    }

    // �÷��̾ ��Ҹ� �Ѿ ��, ����
    // �÷��̾ �ڷ���Ʈ�� �� ��, 3�� �ִٰ� ������
    // �ʿ�Ӽ� : ��� �ð�
    [SerializeField]
    public float stopTime = 4;

    private void StopState()
    {
        bossUI.SetActive(true);
        playingUI.SetActive(false);
        // �ð��� �帣��
        currentTime += Time.deltaTime;
        
        // ���� �ð��� �帣�� �Ǹ�
        if (currentTime > stopTime)
        {
            bossUI.SetActive(false);
            playingUI.SetActive(true);
            // ���¸� �÷������� �ٲ�
            currentTime = 0;
            m_state = GameState.Playing;
        }
    }

    // �Ͻ�����
    private void PauseState()
    {
        if(isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
            pauseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        if(isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            m_state = GameState.Playing;
            pauseUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
    }

}
