using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomGenerator : MonoBehaviour
{
    
    public UnityEvent puertaEncontrada;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")==true){
            puertaEncontrada.Invoke();
        }
    }
}
