using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjeto : MonoBehaviour
{
    public float distancia = 10.0f; // La distancia que quieres que se mueva el objeto
    private float posicionInicial;

    void Start()
    {
        posicionInicial = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector2(posicionInicial + Mathf.PingPong(Time.time, distancia), transform.position.y);
    }
}
