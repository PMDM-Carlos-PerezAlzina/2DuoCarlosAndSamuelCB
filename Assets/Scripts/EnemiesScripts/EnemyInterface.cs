using UnityEngine;
using System.Collections;

public abstract class EnemyInterface : MonoBehaviour
{
    protected int life = 100;
    protected int damage = 10;
    // Método para el comportamiento de ataque
    public virtual void Attack()
    {
        Debug.Log("EnemyInterface Attack");
    }

    // Método para el comportamiento de recibir daño
    public virtual void TakeDamage()
    {
        life -= GameManager.damagePlayer;
        Debug.Log("Life is: " + life);
    }

    public virtual void Die(GameObject gameObject, GameObject deathObject)
    {
        // Guardar las coordenadas del GameObject actual
        Vector3 position = gameObject.transform.position;

        // Finalmente, destruir el GameObject original
        Destroy(gameObject);

        // Crear y configurar el nuevo GameObject (DeathAnimation) en las coordenadas guardadas
        GameObject deathAnimation = Instantiate(deathObject);

        deathAnimation.transform.position = position;

        // Obtener el Animator del nuevo GameObject
        Animator deathAnimator = deathAnimation.GetComponent<Animator>();
    }
}
