using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager questManager;
    private ItemsManager itemManager;
    public string itemName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            questManager = FindObjectOfType<QuestManager>();
            itemManager = FindObjectOfType<ItemsManager>();

            Quest q = questManager.QuestWidthID(questID);
            if(q == null)
            {
                Debug.LogErrorFormat("La mision con id {0} no existe", questID);
                return;
            }

            if (q.gameObject.activeInHierarchy && !q.questCompleted)
            {
                questManager.itemCollected = this;
                itemManager.AddQuestItem(this.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
