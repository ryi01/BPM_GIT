using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 점점 커지다가 작아지면서 알파값 줄어들음 => 250 -> 280
public class StartText : MonoBehaviour
{
    // 텍스트
    public Text startText;
    // 커지는 속도
    public int speed = 3;
    // 크기
    public int maxSize = 280;
    int normalSize = 200;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.m_state == GameManager.GameState.Ready)
        {
            startText.fontSize = normalSize;
            SizeUp();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SizeUp()
    {
        StartCoroutine(BigSize());
    }
    float currentTime = 0;
    IEnumerator BigSize()
    {
        // 250
        int size = normalSize;

        Color alpha = startText.color;
        float b = 1;

        while (b > 0)
        {
            size += speed;
            startText.fontSize = size;
            b -= 0.005f;
            alpha.a = b;
            startText.color = alpha;
            yield return null;
        }
        
    }    
}
