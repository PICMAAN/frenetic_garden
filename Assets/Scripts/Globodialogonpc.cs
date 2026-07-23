using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// Va arriba de la cabeza de cada NPC (Canvas en World Space).
// Muestra la imagen del platillo pedido y una barra que se vacia
// durante el tiempo de paciencia. Si se vacia por completo, avisa
// al ComensalBase para que se vaya insatisfecho.
public class Globodialogonpc : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject contenedorGlobo;
    public Image imagenPlatilloPedido;
    public Image barraDePaciencia;

    private Coroutine temporizadorActivo;

    private void Awake()
    {
        if (contenedorGlobo != null)
        {
            contenedorGlobo.SetActive(false);
        }
    }

    // Prende el globo, pone el icono del platillo y arranca la cuenta regresiva
    public void MostrarPedido(Receta receta, float duracionSegundos, Action alAgotarseElTiempo)
    {
        if (contenedorGlobo != null)
        {
            contenedorGlobo.SetActive(true);
        }

        if (imagenPlatilloPedido != null && receta != null)
        {
            imagenPlatilloPedido.sprite = receta.iconoPlatillo;
        }

        if (barraDePaciencia != null)
        {
            barraDePaciencia.fillAmount = 1f;
        }

        if (temporizadorActivo != null)
        {
            StopCoroutine(temporizadorActivo);
        }

        temporizadorActivo = StartCoroutine(CorrerTemporizador(duracionSegundos, alAgotarseElTiempo));
    }

    private IEnumerator CorrerTemporizador(float duracionSegundos, Action alAgotarseElTiempo)
    {
        float tiempoRestante = duracionSegundos;

        while (tiempoRestante > 0f)
        {
            tiempoRestante -= Time.deltaTime;

            if (barraDePaciencia != null)
            {
                barraDePaciencia.fillAmount = Mathf.Clamp01(tiempoRestante / duracionSegundos);
            }

            yield return null;
        }

        OcultarGlobo();
        alAgotarseElTiempo?.Invoke();
    }

    // Apaga el globo, se llama tanto si se le entrego a tiempo como si se agoto la paciencia
    public void OcultarGlobo()
    {
        if (temporizadorActivo != null)
        {
            StopCoroutine(temporizadorActivo);
            temporizadorActivo = null;
        }

        if (contenedorGlobo != null)
        {
            contenedorGlobo.SetActive(false);
        }
    }
}