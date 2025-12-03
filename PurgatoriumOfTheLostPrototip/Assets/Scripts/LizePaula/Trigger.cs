using UnityEngine;

public class Trigger : MonoBehaviour
{
    [Header("Referencia a la puerta")]
    public PuertaFSM puerta;  // Arrastra aquí el GameObject de la puerta en el Inspector

    [Header("Tag del jugador")]
    public string tagJugador = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            if (puerta != null)
            {
                puerta.AbrirPuerta();  // Notifica a la puerta para que se abra
            }
        }
    }
}
