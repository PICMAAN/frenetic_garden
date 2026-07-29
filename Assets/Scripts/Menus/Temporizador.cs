using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEditor;


public class Temporizador : MonoBehaviour
{
    [Header("Tiempo")] 
    public float tiempoInicial;
    private bool TransicionIniciada = false;
    private float tiempoRestante;

    [Header("UI")]
    public TextMeshProUGUI textoTiempo;

    [Header("Sonido")]
    //public AudioSource audioSource;
    //public AudioClip sonido10Segundos;
    //private bool sonidoReproducido = false;

    [Header("PanelGameOver")]
    public GameObject panelGameOver;
    
    private Animator animator;
    //[SerializeField] private AnimationClip animacionFadeInOut;
    [SerializeField] private AnimationClip animacionFadeIn;
    [SerializeField] private AnimationClip animacionFadeOut;

    public MenuPausa music;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        music =  FindFirstObjectByType<MenuPausa>();
    }
    
    void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    IEnumerator Transiciones()
    {
        music.musica.Pause();
        Time.timeScale = 0f;
        Debug.Log("FadeOut");
        animator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(animacionFadeOut.length);
        
        Debug.Log("PanelGameOver");
        panelGameOver.SetActive(true);
        
        animator.SetTrigger("FadeIn");
        yield return new WaitForSecondsRealtime(animacionFadeIn.length);
        Debug.Log("FadeIn");
    }

            void Update()
            {
                if (tiempoRestante > 0)
                {
                    tiempoRestante -= Time.deltaTime;

                    int minutos = Mathf.FloorToInt(tiempoRestante / 60);
                    int segundos = Mathf.FloorToInt(tiempoRestante % 60);

                    textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);

                    //if (tiempoRestante <= 10f && !sonidoReproducido)
                    //{
                    //    Debug.Log("Sonido");
                    //    audioSource.PlayOneShot(sonido10Segundos);
                    //    sonidoReproducido = true;
                    //}
                }
                else if (!TransicionIniciada)
                {
                    textoTiempo.text = "00:00";
                    TransicionIniciada = true;
                    StartCoroutine(Transiciones());
                }
            }

        }
    