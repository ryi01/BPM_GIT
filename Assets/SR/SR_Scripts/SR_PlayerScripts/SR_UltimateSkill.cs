using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SR_UltimateSkill : MonoBehaviour
{
    // Celestial Drumroll
    // explosion effect and damage to every enemy during player clearing three rooms.

    float currentTime;
    public GameObject explosionFactory;
    int drumrollDamage = 2;
    int cnt=0;

    // 스킬 이미지
    public Image skill;

    private void Start()
    {
        skill.gameObject.SetActive(false);
        currentTime = 5.4f;
    }
    private void FixedUpdate()
    {

        cnt = GetComponent<SR_PlayerInventory>().numberOfScrolls;
        print(cnt);


        currentTime += Time.deltaTime;
            if (currentTime > 5.4f) currentTime = 0;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (cnt > 0)
        {
            skill.gameObject.SetActive(true);
            if (other.gameObject.name.Contains("Larva") && currentTime == 0)
            {
                other.GetComponent<LarvaHP>().AddDamage(drumrollDamage);
                GameObject explosion = Instantiate(explosionFactory, other.transform);
            }
            if (other.gameObject.name.Contains("Spider") && currentTime == 0)
            {
                other.GetComponent<SpiderHP>().AddDamage(drumrollDamage);
                GameObject explosion = Instantiate(explosionFactory, other.transform);
            }
            if (other.gameObject.name.Contains("Boss") && currentTime == 0)
            {
                other.GetComponent<BossHP>().AddDamage(drumrollDamage, new Vector3(0, 0, 0));
                GameObject explosion = Instantiate(explosionFactory, other.transform);
            }
            if (other.gameObject.name.Contains("Bat") && currentTime == 0)
            {
                other.GetComponent<BatHP>().AddDamage(drumrollDamage);
                GameObject explosion = Instantiate(explosionFactory, other.transform);
            }
        }
    }
}
