using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{

    private QuestManager questManager;
    public int questID;
    public bool startPoint;
    public bool endPoint;
    private bool playerInZone;
    public bool automaticCatch;

    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")){
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone)
        {
            if (automaticCatch || (!automaticCatch && Input.GetKeyDown(KeyCode.T)))
            {
                Quest q = questManager.QuestWidthID(questID);

                if(q == null)
                {
                    Debug.LogErrorFormat("La misión con ID {0} no existe", questID);
                    return;
                }

                // No ha completado la mision actual
                if (!q.questCompleted)
                {
                    // Estoy en la zona que empieza la mision
                    if (startPoint)
                    {
                        if (!q.gameObject.activeInHierarchy)
                        {
                            q.gameObject.SetActive(true);
                            q.StartQuest();
                        }
                    }
                    
                    // Estoy en la zona que termina la mision
                    if (endPoint)
                    {
                        if (q.gameObject.activeInHierarchy)
                        {
                            q.gameObject.SetActive(false);
                            q.CompleteQuest();
                        }
                    }
                }
            }
        }
    }
}
