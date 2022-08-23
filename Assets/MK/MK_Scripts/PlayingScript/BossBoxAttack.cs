using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 닿으면 플레이어의 HP를 깍고 싶다
public class BossBoxAttack : MonoBehaviour
{
    SR_PlayerHP player;
    public bool isAttack = false;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<SR_PlayerHP>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            isAttack = true;
        }
    }
}
