using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Image avatarImage;
    public bool dialogueActive;

    public string[] dialogueLines;
    public int currentDialogueLine;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueLine++;
            if (dialogueLines.Length <= currentDialogueLine)
            {
                dialogueActive = false;
                avatarImage.enabled = false;
                dialogueBox.SetActive(false);
                playerController.isTalking = false;
            } else
            {
                dialogueText.text = dialogueLines[currentDialogueLine];
            }
        }
    }

    public void ShowDialogue(string[] line)
    {
        currentDialogueLine = 0;
        dialogueLines = line;
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[currentDialogueLine];
        playerController.isTalking = true;
    }

    public void ShowDialogue(string[] line, Sprite sprite)
    {
        ShowDialogue(line);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
    }


}
