using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class Temporizador : MonoBehaviour
{
    [Header("Tiempo")]
    public float tiempoInicial = 120f; // 2 minutos
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


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    IEnumerator Transiciones()
    {
        Debug.Log("FadeOut");
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(animacionFadeOut.length);
        
        Debug.Log("PanelGameOver");
        panelGameOver.SetActive(true);
        
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(animacionFadeIn.length);
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
    