using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SR_BPM : MonoBehaviour
{
    public static SR_BPM instance;
    public AudioSource bgm;
    public AudioSource eft;
    float curTime;
    bool canFire = true;

    int cnt = 0;

    public float oneBit;
    public float bpm;
    public float offsetTime;

    public GameObject nodeFactory_L,nodeFactory_R;
    public GameObject s_nodeFactory_L, s_nodeFactory_R;

    public float nodeSpeed;

    public Image centerImage;
    
    private void Start()
    {
        centerImage.enabled = false;
    }

    private void Awake()
    {
        instance = this;
        oneBit = 60 / bpm;
        nodeSpeed = 100 / oneBit;
    }

    private void Update()
    {
        centerImage.enabled = false;

        if (bgm.isPlaying)
        {
            curTime += Time.deltaTime;

            if (curTime >= oneBit && cnt == 0)
            {
                curTime -= oneBit;
                centerImage.enabled = true;
                CreateNode();
                cnt++;
                if (cnt >= 2) cnt = 0;
            }
            if (curTime >= oneBit && cnt == 1)
            {
                curTime -= oneBit;
                centerImage.enabled = true;
                CreateSNode();
                cnt++;
                if (cnt >= 2) cnt = 0;
            }
        }
    }

    public void Shot()
    {
        eft.PlayOneShot(eft.clip);
    }
    public void CreateNode()
    {
        Transform centre = GameObject.Find("Center").transform;

        GameObject node_R = Instantiate(nodeFactory_R, centre.position + new Vector3(172,0,0), Quaternion.identity, GameObject.Find("Center").transform);
        node_R.GetComponent<SR_Node_R>().dir = -1;
        GameObject node_L = Instantiate(nodeFactory_L, centre.position - new Vector3(172,0,0), Quaternion.identity, GameObject.Find("Center").transform);
        node_L.GetComponent<SR_Node_L>().dir = 1;
        
    }
    public void CreateSNode()
    {
        Transform centre = GameObject.Find("Center").transform;

        GameObject s_node_R = Instantiate(s_nodeFactory_R, centre.position + new Vector3(172, 0, 0), Quaternion.identity, GameObject.Find("Center").transform);
        s_node_R.GetComponent<SR_Node_R>().dir = -1;
        GameObject s_node_L = Instantiate(s_nodeFactory_L, centre.position - new Vector3(172, 0, 0), Quaternion.identity, GameObject.Find("Center").transform);
        s_node_L.GetComponent<SR_Node_L>().dir = 1;

    }
    
}
