using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quets;
    private DialogueManager dialogueManager;

    public QuestItem itemCollected;
    public QuestEnemy enemyKilled;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        //foreach (Transform t in transform)
        //{
        //    quets.Add(t.gameObject.GetComponent<Quest>());
        //}
        //foreach (Quest temp in quets)
        //{

        //    Debug.Log(temp);
        //}
        //Debug.Log(quets.Count);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestText(string quesText)
    {
        dialogueManager.ShowDialogue(new string[] { quesText });
    }

    public Quest QuestWidthID(int questID)
    {
        Quest q = null;
        foreach(Quest temp in quets)
        {
            
            if(temp.questID == questID)
            {
                q = temp;
            }
        }
        return q;
    }
}
