using UnityEngine;

public class PuertaFSM : MonoBehaviour
{
    public enum EstatPorta
    {
        Oberta,
        Tancada,
        Obrint,
        Tancant
    }

    public EstatPorta estatActual = EstatPorta.Tancada;

    [Header("Configuración de movimiento")]
    public Transform portaTransform;
    public float distanciaApertura = 3f;
    public float velocidadMovimiento = 2f;

    [Header("Auto-cierre")]
    public float tiempoAutoCierre = 5f;

    [Header("Audio")]
    public AudioClip sonidoPuerta; 
    private AudioSource audioSource;

    private Vector3 posicionInicial;
    private float temporizador = 0f;

    void Start()
    {
        posicionInicial = portaTransform.localPosition;

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        EnterState(estatActual);
    }

    void Update()
    {
        UpdateState(estatActual);
    }

    public void AbrirPuerta()
    {
        if (estatActual == EstatPorta.Tancada)
        {
            ExitState(estatActual);
            estatActual = EstatPorta.Obrint;
            EnterState(estatActual);
        }
    }

    private void EnterState(EstatPorta estat)
    {
        switch (estat)
        {
            case EstatPorta.Oberta:
                portaTransform.localPosition = posicionInicial + Vector3.up * distanciaApertura;
                temporizador = 0f;
                break;

            case EstatPorta.Tancada:
                portaTransform.localPosition = posicionInicial;
                break;

            case EstatPorta.Obrint:
            case EstatPorta.Tancant:
                ReproducirSonido();
                break;
        }
    }

    private void UpdateState(EstatPorta estat)
    {
        switch (estat)
        {
            case EstatPorta.Oberta:
                temporizador += Time.deltaTime;
                if (temporizador >= tiempoAutoCierre)
                {
                    ExitState(estatActual);
                    estatActual = EstatPorta.Tancant;
                    EnterState(estatActual);
                }
                break;

            case EstatPorta.Tancada:
                break;

            case EstatPorta.Obrint:
                MoverPuerta(arriba: true);
                break;

            case EstatPorta.Tancant:
                MoverPuerta(arriba: false);
                break;
        }
    }

    private void MoverPuerta(bool arriba)
    {
        Vector3 destino = arriba
            ? posicionInicial + Vector3.up * distanciaApertura
            : posicionInicial;

        portaTransform.localPosition = Vector3.MoveTowards(
            portaTransform.localPosition,
            destino,
            velocidadMovimiento * Time.deltaTime
        );

        if (Vector3.Distance(portaTransform.localPosition, destino) < 0.01f)
        {
            ExitState(estatActual);
            estatActual = arriba ? EstatPorta.Oberta : EstatPorta.Tancada;
            EnterState(estatActual);
        }
    }

    private void ExitState(EstatPorta estat)
    {
       
    }

    private void ReproducirSonido()
    {
        if (sonidoPuerta != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoPuerta);
        }
    }
}
