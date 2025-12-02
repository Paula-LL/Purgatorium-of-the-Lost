using UnityEngine;
using UnityEngine.SceneManagement;

public class CondiciondeVictoria : MonoBehaviour
{
    // Nombre de la escena a cargar
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
