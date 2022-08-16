using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 매니져 : 전반적인 시스템 관리 
// 1. Ready 2. Play 3. Stop(장소 넘어가는 용)
public class GameManager : MonoBehaviour
{
    // 싱글톤 
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    // 일시정지용
    bool isPause = false;
    // enum
    public enum GameState
    {
        Ready,
        Playing,
        Stop,
        Pause
    }
    // 초반에는 ready상태로
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

    // Play 버튼 이후, 잠깐 멈췄다가 Playing으로 넘어가기
    // 필요속성 : 경과 시간, 멈추는 시간
    [SerializeField]
    public float readyTime = 3;
    float currentTime;
    private void ReadyState()
    {
        // 시간이 흐르고
        currentTime += Time.deltaTime;
        // 일정 시간이 흐르게 되면
        if (currentTime > readyTime)
        {
            // 상태를 플레잉으로 바뀜
            currentTime = 0;
            m_state = GameState.Playing;
        }
    }

    private void PlayingState()
    {
        
    }

    // 플레이어가 장소를 넘어갈 때, 사용됨
    // 플레이어가 텔레포트를 한 후, 3초 있다가 움직임
    // 필요속성 : 경과 시간
    [SerializeField]
    public float stopTime = 4;

    private void StopState()
    {
        // 시간이 흐르고
        currentTime += Time.deltaTime;
        // 일정 시간이 흐르게 되면
        if (currentTime > stopTime)
        {
            // 상태를 플레잉으로 바뀜
            currentTime = 0;
            m_state = GameState.Playing;
        }
    }

    // 일시정지
    private void PauseState()
    {
        if(isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
            return;
        }
        if(isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            m_state = GameState.Playing;
            return;
        }
    }

}
