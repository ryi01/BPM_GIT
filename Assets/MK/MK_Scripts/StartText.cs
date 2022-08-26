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
    int normalSize = 250;

    // Start is called before the first frame update
    void Start()
    {
        startText.fontSize = normalSize;
    }

    // Update is called once per frame
    void Update()
    {
        SizeUp();
    }

    void SizeUp()
    {
        StartCoroutine(BigSize());
    }
    int size = 0;
    IEnumerator BigSize()
    {
        while(startText.fontSize <= maxSize)
        {
            size += speed;
            startText.fontSize = size;
            yield return null;
        }
    }
}
