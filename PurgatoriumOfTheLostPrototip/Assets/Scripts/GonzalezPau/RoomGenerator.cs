using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomGenerator : MonoBehaviour
{
    
    public UnityEvent puertaEncontrada;
    public RoomChooser roomChooser;

    private void OnEnable()
    {
        puertaEncontrada.AddListener(UsaElSingleton);
    }

    private void OnDisable()
    {
        puertaEncontrada.RemoveListener(UsaElSingleton);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")==true){
            puertaEncontrada.Invoke();
        }
    }

    void UsaElSingleton()
    {
        //Hace llamada a RoomChoose.SiguienteSala 
        RoomChooser.instance.cargarSiguienteSala();
    }


}
