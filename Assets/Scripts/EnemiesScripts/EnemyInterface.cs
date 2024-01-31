using UnityEngine;

public class EnemyInterface : MonoBehaviour
{
    private int life = 100;
    private int damage = 10;
    // Método para el comportamiento de ataque
    public virtual void Attack()
    {
        Debug.Log("EnemyInterface Attack");
    }

    // Método para el comportamiento de recibir daño
    public virtual void TakeDamage(int damageAmount)
    {
        Debug.Log("EnemyInterface TakeDamage: " + damageAmount + " damage");
    }

    public virtual void Die() {

    }

    public virtual void Move() {

    }
}
