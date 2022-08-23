using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ��ŸƮ���� �������� �� �� ������ �ε���
public class LoadingSceneManager : MonoBehaviour
{
    // ���ξ�
    public static string nextScene;
    [SerializeField]
    Image uiBar;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }
    public static void LoadScene(string name)
    {
        nextScene = name;
        SceneManager.LoadScene("Loading");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if(op.progress < 0.9f)
            {
                uiBar.fillAmount = Mathf.Lerp(uiBar.fillAmount, op.progress, timer);
                if(uiBar.fillAmount >= op.progress)
                {
                    timer = 0;
                }
            }
            else
            {
                uiBar.fillAmount = Mathf.Lerp(uiBar.fillAmount, 1, timer);
                if(uiBar.fillAmount == 1)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
