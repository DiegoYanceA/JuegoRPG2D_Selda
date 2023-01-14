using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    /*
    [Tooltip("Tiempo en que tarda el jugador en revivir")]
    public float timeToRevivePlayer;
    private float timeRevivalCounter;
    private bool playerReviving;
    */

    public int damage;

    //private GameObject thePlayer;
    public GameObject canvasDamage;
    private CharacterStats playerStats;
    private CharacterStats _stats;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        _stats = GetComponent<CharacterStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            float strFac = 1 + _stats.strengthLevels[_stats.level] / CharacterStats.MAX_STAT_VAL;
            float plaFac = 1 - playerStats.defenseLevels[playerStats.level] / CharacterStats.MAX_STAT_VAL;

            int totalDamage = (int)(damage * strFac * plaFac);
            totalDamage = Mathf.Clamp(totalDamage, 1, CharacterStats.MAX_HEALTH);

            float missProb = playerStats.luckLevels[playerStats.level];
            if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < missProb)
            {
                if (_stats.accuracyLevels[_stats.level] < Random.Range(0, CharacterStats.MAX_STAT_VAL))
                {
                    totalDamage = 0;
                }
            }

            var clone = (GameObject)Instantiate(canvasDamage,
                        collision.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;

            collision.gameObject.GetComponent<HealthManager>()
                                .DamageCharacter(damage);
            //collision.gameObject.SetActive(false);

            //playerReviving = true;
            //timeRevivalCounter = timeToRevivePlayer;
            //thePlayer = collision.gameObject;
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (playerReviving)
        {
            timeRevivalCounter -= Time.deltaTime;
            if(timeRevivalCounter < 0)
            {
                playerReviving = false;
                thePlayer.SetActive(true);
            }
        }
    }*/
}
