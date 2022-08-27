using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SR_EndingManager : MonoBehaviour
{
    public Image bpm, victory;
    float currentTime = 0;

    private void Start()
    {
        bpm.gameObject.SetActive(false);
        victory.gameObject.SetActive(false);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 1.0f)
        {
            StartCoroutine(Show());
        }
        if (victory.gameObject.activeSelf == true)
        {
            if (Input.anyKey) SceneManager.LoadScene("Main");
        }
    }



    IEnumerator Show()
    {
        bpm.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Destroy(bpm);
        victory.gameObject.SetActive(true);
    }
}
