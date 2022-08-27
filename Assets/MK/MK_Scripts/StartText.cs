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
        startText.fontSize = normalSize;
        SizeUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SizeUp()
    {
        StartCoroutine(BigSize());
    }
    IEnumerator BigSize()
    {
        // 250
        int size = normalSize;
        // 280
        int downsize = maxSize;
        Color alpha = startText.color;
        float b = 1;
        while (startText.fontSize < maxSize)
        {
            size += speed;
            startText.fontSize = size;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        while (startText.fontSize > 190 && alpha.a > 0)
        {
            downsize -= 1;
            b -= 0.01f;
            startText.fontSize = downsize;
            alpha.a = b;
            startText.color = alpha;
            yield return null;
        }
    }    
}
