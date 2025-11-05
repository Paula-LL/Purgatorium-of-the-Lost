using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomGenerator : MonoBehaviour
{
    
    UnityEvent puertaEncontrada;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")==true){
            Teleport();
            puertaEncontrada.Invoke();
        }
    }
    private void Teleport()
    {
        
    }
}
