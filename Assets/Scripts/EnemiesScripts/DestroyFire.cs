using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFire : MonoBehaviour
{

    public delegate void EnemigoDerrotado();
    public static event EnemigoDerrotado OnEnemigoDerrotado;
    public GameObject player;
    public 

    void Start()
    {
        OnEnemigoDerrotado?.Invoke();
        float tiempoParaDestruccion = 0.68333f;
        Destroy(gameObject, tiempoParaDestruccion);
    }
}
