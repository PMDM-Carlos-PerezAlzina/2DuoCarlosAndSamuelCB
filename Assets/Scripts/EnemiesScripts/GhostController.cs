using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : EnemyInterface
{
    public GameObject deathObject;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float stepDistance = 2.0f; // Ajusta el valor en el editor
    [SerializeField] private float flipSpeed = 5.0f; // Ajusta el valor en el editor para controlar la velocidad del giro
    private Vector3 initialPosition;
    private int m_facingDirection = -1;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (base.life <= 0)
        {
            base.Die(gameObject, deathObject);
        }

        CheckDirectionChange();
        Move();
        RotateGhost(); // Nueva función para manejar la rotación del sprite
    }

    private void CheckDirectionChange()
    {
        float step = Mathf.PingPong(Time.time * moveSpeed, stepDistance * 2) - stepDistance;

        // Verificar si es necesario cambiar de dirección
        if ((step < 0 && m_facingDirection > 0) || (step >= 0 && m_facingDirection < 0))
        {
            // Cambiar de dirección
            m_facingDirection *= -1;
        }
    }

    private void RotateGhost()
    {
        // Gira gradualmente hacia la nueva dirección
        transform.localScale = new Vector3(m_facingDirection, 1, 1);
    }

    public override void Move()
    {
        float step = Mathf.PingPong(Time.time * moveSpeed, stepDistance * 2) - stepDistance;
        transform.position = new Vector3(initialPosition.x + step, transform.position.y, transform.position.z);
    }
}