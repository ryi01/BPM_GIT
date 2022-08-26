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
