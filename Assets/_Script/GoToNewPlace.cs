using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New Scene Name Here!!!";
    public bool needsClick = false;

    public string uuid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Teleport(collision.gameObject.tag);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Teleport(collision.gameObject.name);
    }

    private void Teleport(string objname)
    {
        
        if(objname == "Player")
        {
            if (!needsClick || (needsClick && Input.GetKeyDown("k")))
            {
                FindObjectOfType<PlayerController>().nextUuid = uuid;
                Debug.LogFormat("Cambiar de la escena \"{0}\" a \"{1}\"",
                                SceneManager.GetActiveScene().name, newPlaceName);
                SceneManager.LoadScene(newPlaceName);
            }
        }
        
        
    }
}
