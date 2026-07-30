using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Componentes")]
    [SerializeField] private AudioSource audioSource;

    [Header("Pistas de Música")]
    [SerializeField] private AudioClip musicaMenu;
    [SerializeField] private AudioClip musicaJuego;

    private void Awake()
    {
        // 1. Patrón Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(transform.root.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        // Si no asignaste el AudioSource en el Inspector, intenta obtenerlo
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Nos suscribimos al evento que avisa cuándo se carga una nueva escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Nos desuscribimos al destruirlo para evitar errores
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Se ejecuta automáticamente CADA VEZ que se carga cualquier escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Evaluamos en qué escena estamos
        if (scene.name == "MenuPrincipal" || scene.name == "Configuracion")
        {
            // Reproducir música de menú (si no estaba ya sonando)
            CambiarMusica(musicaMenu);
        }
        else if (scene.name == "EmmanuelPruebas") // O el nombre de tu escena de Gameplay
        {
            // Reproducir música del Gameplay
            CambiarMusica(musicaJuego);
        }
    }

    private void CambiarMusica(AudioClip nuevaMusica)
    {
        // Si la canción elegida ya se está reproduciendo, NO la reiniciamos
        if (audioSource.clip == nuevaMusica && audioSource.isPlaying) 
            return;

        audioSource.Stop();
        audioSource.clip = nuevaMusica;
        audioSource.Play();
    }
    
    // Dentro de tu script MusicManager.cs

    public void PausarMusica()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void ReanudarMusica()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
}