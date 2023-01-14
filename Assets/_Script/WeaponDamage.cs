using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Cantidad de daño que hará la espada, cuchillo o hacha")]
    public int damage;

    public GameObject bloodAnim;
    private GameObject hitPoint;
    private GameObject currentAnim;

    public GameObject canvasDamage;

    private CharacterStats stats;

    private void Start()
    {
        if(transform.Find("Hit Point"))
        {
            hitPoint = transform.Find("Hit Point").gameObject;
            stats = GameObject.Find("Player").GetComponent<CharacterStats>();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            CharacterStats enemyStats = collision.gameObject.GetComponent<CharacterStats>();
            float plaFac = (1 + stats.strengthLevels[stats.level] / CharacterStats.MAX_STAT_VAL);
            float eneFac = (1 - enemyStats.defenseLevels[enemyStats.level] / CharacterStats.MAX_STAT_VAL); 

            int totalDamage = (int) (damage * plaFac * eneFac);

            if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < stats.accuracyLevels[stats.level])
            {
                if (Random.Range(0, CharacterStats.MAX_STAT_VAL) < enemyStats.luckLevels[enemyStats.level])
                {
                    totalDamage = 0;
                } else
                {
                    totalDamage *= 5;
                }
                
            }

            if (bloodAnim != null && hitPoint != null)
            {
                currentAnim = Instantiate(bloodAnim, hitPoint.transform.position, hitPoint.transform.rotation);
                Destroy(currentAnim, 0.5f);
            }

            var clone = (GameObject)Instantiate(canvasDamage,
                        hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;

            collision.gameObject.GetComponent<HealthManager>()
                                .DamageCharacter(damage);
        }
    }
}
