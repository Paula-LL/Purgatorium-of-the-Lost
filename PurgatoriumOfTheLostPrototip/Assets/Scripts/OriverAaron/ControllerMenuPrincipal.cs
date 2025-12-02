using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerMenuPrincipal : MonoBehaviour
{
    [Header("Configuración de Botones")]
    [SerializeField] private Button botonInicio;
    [SerializeField] private Button botonAjustes;
    [SerializeField] private Button botonSalir;

    [Header("Configuración de Escena")]
    [SerializeField] private string nombreEscena = "Mapa";

    [Header("Configuración de Canvas")]
    [SerializeField] private Canvas canvasActivar;
    [SerializeField] private Canvas canvasDesactivar;

    void Start()
    {
        // Asignar los métodos a los botones
        if (botonInicio != null)
            botonInicio.onClick.AddListener(CambiarEscena);

        if (botonAjustes != null)
            botonAjustes.onClick.AddListener(AlternarCanvas);

        if (botonSalir != null)
            botonSalir.onClick.AddListener(SalirDelJuego);
    }

    // Método para cambiar de escena
    public void CambiarEscena()
    {
       SceneManager.LoadScene(nombreEscena);
    }

    // Método para alternar entre canvas
    public void AlternarCanvas()
    {
        if (canvasActivar != null)
        {
            canvasActivar.gameObject.SetActive(true);
        }

        if (canvasDesactivar != null)
        {
            canvasDesactivar.gameObject.SetActive(false);
        }
    }

    // Método para salir del juego
    public void SalirDelJuego()
    {

            Application.Quit();

    }
}