using UnityEngine;
using UnityEngine.UI;

public class WizardController : EnemyInterface
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
    [SerializeField] private Image wizardImage;

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            base.Die(gameObject, deathObject);
        }
        rb2d.velocity = new Vector3(moveSpeed, rb2d.velocity.y);

        frontInformation = Physics2D.Raycast(frontController.position, transform.right, distanceFront, front);
        downInformation = Physics2D.Raycast(downController.position, transform.up * -1, distanceDown, down);

        // Si no está atacando, realiza la lógica de voltear
        if (frontInformation || !downInformation)
        {
            Flip();
        }
    }

    private void Flip() {
        isLookingRight = !isLookingRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        moveSpeed = moveSpeed * -1;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(downController.transform.position, downController.transform.position + transform.up * -1 * distanceDown);
        Gizmos.DrawLine(frontController.transform.position, frontController.transform.position + transform.right * distanceFront);
    }

    public void UpdateHealtBar(float maxHealth, float currentHealth) {
        wizardImage.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage()
    {
        life -= GameManager.damagePlayer;
        UpdateHealtBar(100, life);
        Debug.Log("Life is: " + life);
    }
}
