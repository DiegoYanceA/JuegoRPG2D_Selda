using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public int questID;
    public bool questCompleted;
    private QuestManager questManager;

    public string title;
    public string startText;
    public string completeText;

    public bool needsItem;
    public List<QuestItem> itemsNeeded;

    public bool killsEnemy;
    public List<QuestEnemy> enemies;
    public List<int> numberOfEnemies;

    public Quest nextQuest;

    public void StartQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + startText);

        if (needsItem)
        {
            ActivateItems();
        }

        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (needsItem)
        {
            ActivateItems();
        }

        if (killsEnemy)
        {
            ActivateEnemies();
        }
    }
    private void ActivateItems()
    {
        Object[] items = Resources.FindObjectsOfTypeAll<QuestItem>();
        foreach (QuestItem q in items)
        {
            if (q.questID == questID)
            {
                q.gameObject.SetActive(true);
            }
        }
    }

    private void ActivateEnemies()
    {
        Object[] enemies = Resources.FindObjectsOfTypeAll<QuestEnemy>();
        foreach (QuestEnemy q in enemies)
        {
            if (q.questID == questID)
            {
                Debug.Log("TEST");
                q.gameObject.SetActive(true);
            }
        }
    }
    
    public void CompleteQuest()
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + completeText);
        questCompleted = true;
        if(nextQuest != null)
        {
            Invoke("ActivateNextQuest",  5.0f);
        }
        gameObject.SetActive(false);
    }

    private void ActivateNextQuest()
    {
        nextQuest.gameObject.SetActive(true);
        nextQuest.StartQuest();
    }

    private void Update()
    {
        if(needsItem && questManager.itemCollected != null)
        {
            for(int i = 0; i < itemsNeeded.Count; i++)
            {
                if(itemsNeeded[i].itemName == questManager.itemCollected.itemName)
                {
                    itemsNeeded.RemoveAt(i);
                    questManager.itemCollected = null;
                    break;
                }
            }

            if(itemsNeeded.Count == 0)
            {
                
                CompleteQuest();
            }
        }

        if(killsEnemy && questManager.enemyKilled != null)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].enemyName == questManager.enemyKilled.enemyName)
                {
                    numberOfEnemies[i]--;
                    questManager.enemyKilled = null;
                    if (numberOfEnemies[i] <= 0)
                    {
                        enemies.RemoveAt(i);
                        numberOfEnemies.RemoveAt(i);
                        
                    }
                    break;
                }
            }

            if (enemies.Count == 0)
            {
                questManager.itemCollected = null;
                CompleteQuest();
            }
        }
    }
}
