using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CambioEscenas : MonoBehaviour
{
    //VARIABLES
    private Animator animator;
    [SerializeField] private AnimationClip animacionFadeOut;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void EventosCambiarEscena(string nombreEscena)
    {
        Debug.Log("Entrando al activador de corrutina");
        StartCoroutine(CambiarEscena(nombreEscena));
    }
    IEnumerator CambiarEscena(string nombreEscena)
    {
        Debug.Log("Dentro de la corrutina");

        if (nombreEscena == "exit")
        {
            Debug.Log("Saliendo");
            Application.Quit();
        }
        else
        {
            animator.SetTrigger("CambioIniciado");
            yield return new WaitForSecondsRealtime(animacionFadeOut.length);
            SceneManager.LoadScene(nombreEscena);
            //Time.timeScale = 1f;
        }
    }
    


}

