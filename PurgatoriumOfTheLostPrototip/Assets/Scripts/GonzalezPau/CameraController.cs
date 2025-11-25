using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
     public Transform player;       // Referencia al objeto Player
    public Vector3 offset = new Vector3(0, 5, -10); // Distancia relativa
    public float smoothSpeed = 0.125f;              // Velocidad de suavizado

    void LateUpdate()
    {
        if (player == null) return;

        // Posición deseada = posición del jugador + offset
        Vector3 desiredPosition = player.position + offset;

        // Movimiento suave hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualizar posición de la cámara
        transform.position = smoothedPosition;

        transform.LookAt(player);
    }
}
