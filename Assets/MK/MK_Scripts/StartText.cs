using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� Ŀ���ٰ� �۾����鼭 ���İ� �پ���� => 250 -> 280
public class StartText : MonoBehaviour
{
    // �ؽ�Ʈ
    public Text startText;
    // Ŀ���� �ӵ�
    public int speed = 3;
    // ũ��
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
