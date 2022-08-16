using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bpm : MonoBehaviour
{
    public static Bpm instance;
    public AudioSource bgm;
    public AudioSource eft;
    float currTime;
    
    public float oneBit;
    public float bpm;
    bool canFire = true;
    public float offsetTime;

    int count = 0;

    public GameObject nodeFactory;
    public float nodeSpeed;

    private void Awake()
    {
        instance = this;
        oneBit = 60 / bpm;
        //속력 = 거리 / 시간
        nodeSpeed = 2 / oneBit;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            bgm.Stop();
            bgm.Play();
            oneBit = 60 / bpm;
            currTime = 0;
            CreateNode();
        }

        if(bgm.isPlaying)
        {
            currTime += Time.deltaTime;
            
            if(currTime >= oneBit)
            {
                count++;
                print(count + ", " + currTime + ", " +  bgm.time);
                currTime -= oneBit;
                CreateNode();
            }            
        }
    }

    public void Shot()
    {
        eft.PlayOneShot(eft.clip);
    }

    public void CreateNode()
    {
        GameObject node = Instantiate(nodeFactory, new Vector3(2, 0, 0), Quaternion.identity);
        node.GetComponent<Node>().dir = -1;

        node = Instantiate(nodeFactory, new Vector3(-2, 0, 0), Quaternion.identity);
        node.GetComponent<Node>().dir = 1;
    }
}
