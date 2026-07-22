using System;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;
//public enum Habitacion {Patio, Cocina, Mesas}

public class CambioDeCamara : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D limitesMapa;
    CinemachineConfiner2D confiner;
    //[SerializeField] private Collider2D coll;

    private void Awake()
    {
        //coll = GetComponent<Collider2D>();
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //coll.enabled = false;
            Debug.Log("Entro al paso de habitacion");
            confiner.BoundingShape2D = limitesMapa;
            
        }
    }
}
