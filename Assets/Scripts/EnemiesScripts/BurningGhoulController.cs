using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGhoulController : EnemyInterface
{
    public GameObject deathObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (base.life <= 0) {
            base.Die(gameObject, deathObject);
        }
    }
}
