using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caution : MonoBehaviour
{
    public GameObject butt;
    // Start is called before the first frame update
    void Start()
    {
        // �ʹݿ� ��������
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            butt.gameObject.SetActive(true);
        }
    }
}
