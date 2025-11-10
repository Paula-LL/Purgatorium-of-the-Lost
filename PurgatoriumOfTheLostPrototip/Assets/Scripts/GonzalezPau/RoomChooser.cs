using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class RoomChooser : MonoBehaviour
{
    enum salida { DERECHA, IZQUIERDA, ARRIBA, ABAJO }
    salida puertaSalida = salida.DERECHA;
    [SerializeField] GameObject salaInicial;
    GameObject salaActual;
    [SerializeField] GameObject sala2PuertasLineal;
    [SerializeField] Collider sala2PuertasLineal1;
    [SerializeField] Collider sala2PuertasLineal2;
    [SerializeField] GameObject sala2PuertasGrados;
    [SerializeField] Collider sala2PuertasGrados1;
    [SerializeField] Collider sala2PuertasGrados2;
    [SerializeField] GameObject sala3Puertas;
    [SerializeField] Collider sala3Puertas1;
    [SerializeField] Collider sala3Puertas2;
    [SerializeField] Collider sala3Puertas3;
    [SerializeField] GameObject sala4Puertas;
    [SerializeField] Collider sala4Puertas1;
    [SerializeField] Collider sala4Puertas2;
    [SerializeField] Collider sala4Puertas3;
    [SerializeField] Collider sala4Puertas4;
    [SerializeField] GameObject Player;

    int randomSala;
    int contadorSalasVisitadas = 1;

    public GameObject[] salasRef;

    private static RoomChooser roomChooser;

    public static RoomChooser instance
    {
        get
        {
            return RequestInstance();
        }
    }
    private static RoomChooser RequestInstance()
    {
        if (roomChooser == null)
        {
            roomChooser = FindObjectOfType<RoomChooser>();
            if (roomChooser == null)
            {
                GameObject roomChooserObject = new GameObject("RoomChooser");
                roomChooser = roomChooserObject.AddComponent<RoomChooser>();
            }
        }
        return roomChooser;
    }
    private void Awake()
    {
        if (roomChooser == null)
        {
            roomChooser = this;
        }
        else if (roomChooser != this)
        {
            Destroy(roomChooser.gameObject);
        }
    }
    public void Start()
    {
        foreach (GameObject go in salasRef)
        {
            go.SetActive(false);
        }
        salaActual = salaInicial;
    }
    public void cargarSiguienteSala()
    {
        switch (puertaSalida)
        {
            case salida.DERECHA:
                salaActual.SetActive(false);
                randomSala = Mathf.RoundToInt(Random.Range(1, 4));
                if (randomSala == 1)
                {
                    Instantiate(sala2PuertasLineal, salaActual.transform.position, Quaternion.identity);
                    salaActual = sala2PuertasLineal;
                    sala2PuertasLineal.SetActive(true);
                    sala2PuertasLineal1.gameObject.SetActive(false);
                    Player.transform.position = sala2PuertasLineal1.transform.position + new Vector3(4f, 0, 0);

                }
                else if (randomSala == 2)
                {
                    Instantiate(sala2PuertasGrados, salaActual.transform.position, Quaternion.identity);
                    salaActual = sala2PuertasGrados;
                    sala2PuertasGrados.SetActive(true);
                    sala2PuertasGrados1.gameObject.SetActive(false);
                    Player.transform.position = sala2PuertasGrados1.transform.position + new Vector3(5f, 0, 0);
                }
                else if (randomSala == 3)
                {
                    Instantiate(sala3Puertas, salaActual.transform.position, Quaternion.identity);
                    salaActual = sala3Puertas;
                    sala3Puertas.SetActive(true);
                    sala3Puertas1.gameObject.SetActive(false);  
                    Player.transform.position = sala3Puertas1.transform.position + new Vector3(2f, 0, 0);
                }
                else if (randomSala == 4)
                {
                    Instantiate(sala4Puertas, salaActual.transform.position, Quaternion.identity);
                    salaActual = sala4Puertas;
                    sala4Puertas.SetActive(true);
                    sala4Puertas1.gameObject.SetActive (false);
                    Player.transform.position = sala4Puertas1.transform.position + new Vector3(2f, 0, 0);
                }
                contadorSalasVisitadas++;
                break;
            case salida.IZQUIERDA:
                break;
            case salida.ARRIBA:
                break;
            case salida.ABAJO:
                break;
        }
    }
}
