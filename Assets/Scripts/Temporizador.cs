using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    [Header("Tiempo")]
    public float tiempoInicial = 120f; // 2 minutos
    private float tiempoRestante;

    [Header("UI")]
    public Text textoTiempo;

    [Header("Sonido")]
    public AudioSource audioSource;
    public AudioClip sonido10Segundos;
    private bool sonidoReproducido = false;

    [Header("Escena")]
    public string nombreEscena;

    void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);

            textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);

            if (tiempoRestante <= 10f && !sonidoReproducido)
            {
                audioSource.PlayOneShot(sonido10Segundos);
                sonidoReproducido = true;
            }
        }
        else
        {
            textoTiempo.text = "00:00";
            SceneManager.LoadScene(nombreEscena);
        }
    }
}