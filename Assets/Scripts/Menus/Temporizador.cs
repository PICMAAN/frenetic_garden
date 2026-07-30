using UnityEngine;
using System.Collections;
using TMPro;

public class Temporizador : MonoBehaviour
{
    [Header("Tiempo")] 
    public float tiempoInicial = 60f;
    private float tiempoRestante;
    private bool transicionIniciada = false;

    [Header("UI")]
    public TextMeshProUGUI textoTiempo;

    [Header("Panel GameOver")]
    public GameObject panelGameOver;
    
    [Header("Animaciones")]
    private Animator animator;
    [SerializeField] private AnimationClip animacionFadeIn;
    [SerializeField] private AnimationClip animacionFadeOut;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        tiempoRestante = tiempoInicial;

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }
    }

    private void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);

            if (textoTiempo != null)
            {
                textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            }
        }
        else if (!transicionIniciada)
        {
            if (textoTiempo != null) textoTiempo.text = "00:00";
            
            transicionIniciada = true;
            StartCoroutine(TransicionGameOverRoutine());
        }
    }

    private IEnumerator TransicionGameOverRoutine()
    {
        // 1. Pausamos la música usando el MusicManager centralizado
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PausarMusica();
        }

        // 2. Transición FadeOut de salida
        if (animator != null && animacionFadeOut != null)
        {
            animator.SetTrigger("FadeOut");
            yield return new WaitForSecondsRealtime(animacionFadeOut.length);
        }
        
        // 3. Activamos el panel de GameOver
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
        
        // 4. Pausamos el tiempo del juego (físicas, movimiento de enemigos, etc.)
        Time.timeScale = 0f;

        // 5. Animación FadeIn de entrada al GameOver (si aplica)
        if (animator != null && animacionFadeIn != null)
        {
            animator.SetTrigger("FadeIn");
            yield return new WaitForSecondsRealtime(animacionFadeIn.length);
        }
    }
}