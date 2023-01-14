using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    [SerializeField]
    private int currentHealth;
    public int Health
    {
        get
        {
            return currentHealth;
        }
    }
    public bool flashActive;
    public float flashLength;
    private float flashCounter;

    public int expWhenDefeated;

    private SpriteRenderer _characterRenderer;

    private QuestEnemy quest;
    private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        UpdateMaxHealth(maxHealth);

        quest = GetComponent<QuestEnemy>();
        questManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashLength * 0.66f < flashCounter)
            {
                toggleColor(false);
            } else if (flashLength * 0.33f < flashCounter)
            {
                toggleColor(true);
            } else if (0 < flashCounter)
            {
                toggleColor(false);
            } else
            {
                toggleColor(true);
                GetComponent<BoxCollider2D>().enabled = true;
                if(GetComponent<PlayerController>() != null)
                {
                    GetComponent<PlayerController>().canMove = true;
                }
                
                flashActive = false;
            }
        }
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            if (gameObject.tag.Equals("Enemy"))
            {
                GameObject.Find("Player").GetComponent<CharacterStats>()
                                         .AddExperience(expWhenDefeated);
                questManager.enemyKilled = quest;
            }
            gameObject.SetActive(false);
        }

        if (0 < flashLength)
        {
            flashActive = true;
            GetComponent<BoxCollider2D>().enabled = false;
            if (GetComponent<PlayerController>() != null)
            {
                GetComponent<PlayerController>().canMove = false;
            }
            flashCounter = flashLength;
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    private void toggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,
                                              _characterRenderer.color.g,
                                              _characterRenderer.color.b,
                                              (visible? 1:0));
    }
}
