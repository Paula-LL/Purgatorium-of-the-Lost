using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportRoomsController : MonoBehaviour
{
    [SerializeField] GameObject Sala1;
    [SerializeField] GameObject Sala2;
    [SerializeField] GameObject Sala3;
    [SerializeField] GameObject Sala4;
    [SerializeField] GameObject Sala5;
    [SerializeField] GameObject Sala6;
    [SerializeField] GameObject Sala7_1;
    [SerializeField] GameObject Sala7_2;
    [SerializeField] GameObject Player;
    private int numSalaAnt;
    public enum SalaActual
    {
        SALA1, SALA2, SALA3, SALA4, SALA5, SALA6, SALA7, SALA7_2
    }
    SalaActual state;
    private void Start()
    {
        numSalaAnt = 1;
        state = SalaActual.SALA1;
    }
    private void Update()
    {
        ComprovarSala();
    }
    void ComprovarSala()
    {
        Collider salaAnt;
        Collider salaDest;
        switch (state)
        {
            case SalaActual.SALA1:
                salaAnt = Sala1.GetComponentInChildren<Collider>();
                salaDest = Sala2.GetComponentInChildren<Collider>();
                state = SalaActual.SALA2;
                break;
            case SalaActual.SALA2:
                break; 
            case SalaActual.SALA3:
                break; 
            case SalaActual.SALA4:
                break; 
            case SalaActual.SALA5:
                break;
            case SalaActual.SALA6:
                break; 
            case SalaActual.SALA7:
                break; 
            case SalaActual.SALA7_2:
                break;
        }
    }
}
