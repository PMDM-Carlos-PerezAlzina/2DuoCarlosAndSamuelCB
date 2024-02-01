using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : EnemyInterface
{
    public GameObject deathObject;
    public Rigidbody2D rb2d;
    [SerializeField] private float moveSpeed = 2.0f;
    public LayerMask down;
    public LayerMask front;
    public float distanceDown;
    public float distanceFront;
    public Transform downController;
    public Transform frontController;
    public bool downInformation;
    public bool frontInformation;
    private bool isLookingRight = true;

    public float followDistance = 5.0f;  // Definir la distancia a la que el ghost empezará a seguir al jugador
    public Transform player;  // Referencia al objeto del jugador

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            base.Die(gameObject, deathObject);
        }

        // Calcular la distancia entre el ghost y el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si la distancia es menor que la distancia de seguimiento, seguir al jugador
        if (distanceToPlayer < followDistance)
        {
            // Calcular la dirección hacia el jugador
            float direction = Mathf.Sign(player.position.x - transform.position.x);

            // Mover el ghost hacia el jugador
            rb2d.velocity = new Vector2(moveSpeed * direction, rb2d.velocity.y);

            // Voltear el ghost según la dirección hacia el jugador
            if ((direction > 0 && !isLookingRight) || (direction < 0 && isLookingRight))
            {
                Flip();
            }
        }
        else
        {
            // Si el jugador está fuera de la distancia de seguimiento, continuar moviéndose en la dirección predeterminada
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

            frontInformation = Physics2D.Raycast(frontController.position, transform.right, distanceFront, front);
            downInformation = Physics2D.Raycast(downController.position, transform.up * -1, distanceDown, down);

            if (frontInformation || !downInformation)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isLookingRight = !isLookingRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        moveSpeed = moveSpeed * -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(downController.transform.position, downController.transform.position + transform.up * -1 * distanceDown);
        Gizmos.DrawLine(frontController.transform.position, frontController.transform.position + transform.right * distanceFront);
    }
}
