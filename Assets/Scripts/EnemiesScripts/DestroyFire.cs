using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float tiempoParaDestruccion = 0.68333f;
        Destroy(gameObject, tiempoParaDestruccion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
