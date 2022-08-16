using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_StartToStore : MonoBehaviour
{
    public Image black;
    Transform player;
    public Transform newPos;
    float transparent;
    

    private void Update()
    {
        player = GameObject.Find("Player").transform;
        float distance = (gameObject.transform.position - player.position).magnitude;

        if(distance<5.0f)
        {
            StartCoroutine(FadeInOut());
        }
    }

    IEnumerator FadeInOut()
    {
        for (transparent = 0; transparent <= 1; transparent += 0.01f)
        {
            black.canvasRenderer.SetAlpha(transparent);
            print(transparent);
            
        }
        yield return new WaitForSeconds(0.5f);
        player = newPos;
        for (transparent = 1; transparent >= 0; transparent -= Time.deltaTime) black.canvasRenderer.SetAlpha(transparent);
    }
}
