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

    // enum
    public enum GameState
    {
        Ready,
        Playing,
        Stop
    }
    // �ʹݿ��� ready���·�
    public GameState m_state = GameState.Ready;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        throw new NotImplementedException();
    }

    // �÷��̾ ��Ҹ� �Ѿ ��, ����
    // �÷��̾ �ڷ���Ʈ�� �� ��, 3�� �ִٰ� ������
    // �ʿ�Ӽ� : ��� �ð�
    [SerializeField]
    public float stopTime = 4;

    private void StopState()
    {
        // �ð��� �帣��
        currentTime += Time.deltaTime;
        // ���� �ð��� �帣�� �Ǹ�
        if (currentTime > stopTime)
        {
            // ���¸� �÷������� �ٲ�
            currentTime = 0;
            m_state = GameState.Playing;
        }
    }
}
