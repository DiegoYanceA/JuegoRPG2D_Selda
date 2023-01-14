using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private PlayerController player;
    private CameraFollow thecamera;
    public Vector2 facingDirection = Vector2.zero;

    public string uuid;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (!player.nextUuid.Equals(uuid))
        {
            return;
        }

        thecamera = FindObjectOfType<CameraFollow>();

        // Se asigna la posicion del jugador a la del start point
        player.transform.position = transform.position;

        // El eje de las z se mantiene para no estar en el mismo nivel que el jugador
        thecamera.transform.position = new Vector3(transform.position.x,
                                                transform.position.y,
                                                thecamera.transform.position.z);

        player.lastMovement = facingDirection;
    }
}
