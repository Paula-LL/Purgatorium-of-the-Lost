using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class VirtualCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera camaraDeSala;
    public Camera mainCamera;
    public Collider[] salaColliders;
    private void Update()
    {
        comprovarChoqueConPlayer();
    }
    public void cambiarCamara()
    {
        camaraDeSala.enabled = mainCamera == camaraDeSala;
    }
    public void comprovarChoqueConPlayer()
    {
        for (int i = 0; i < salaColliders.Length; i++)
        {
            if (salaColliders[i].CompareTag("Player")){
                Debug.Log("PlayerDetectado cambiar Camara");
                cambiarCamara();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cambiarCamara();
        }
    }
}
