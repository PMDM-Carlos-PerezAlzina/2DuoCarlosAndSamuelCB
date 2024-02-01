using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    [SerializeField] private float m_speed = 4.0f;
    [SerializeField] private float m_jumpForce = 7.5f;
    [SerializeField] private float m_rollForce = 6.0f;
    [SerializeField] private bool m_noBlood = false;
    [SerializeField] private GameObject m_slideDust;
    [SerializeField] private Vector2 reboundSpeed;
    [SerializeField] private int life = 100;
    [SerializeField] private int damageAttack = 20;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    public GameObject knightColliderContainer;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    private float sanity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimers();

        CheckGroundStatus();

        HandleInputAndMovement();

        HandleAnimations();
    }

    private void InitializeComponents()
    {
        // Obtener componentes necesarios al inicio
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    private void HandleTimers()
    {
        // Incrementar temporizador que controla el combo de ataque
        m_timeSinceAttack += Time.deltaTime;

        // Incrementar temporizador que verifica la duración del rollo
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Desactivar el rollo si el temporizador excede la duración
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;
    }

    private void CheckGroundStatus()
    {
        // Verificar si el personaje acaba de aterrizar en el suelo
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Verificar si el personaje acaba de empezar a caer
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }
    }

    private void HandleInputAndMovement()
    {
        float inputX = Input.GetAxis("Horizontal");

        // Cambiar dirección del sprite según la dirección de movimiento
        if (inputX > 0)
        {
            FlipCharacter(false);
        }
        else if (inputX < 0)
        {
            FlipCharacter(true);
        }

        // Mover el Collider junto con el personaje
        MoveColliderWithCharacter(inputX);

        // Mover
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // Establecer AirSpeed en el animador
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);
    }

    private void HandleAnimations()
    {
        float inputX = Input.GetAxis("Horizontal");
        // Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        // Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            TriggerDeathAnimation();
        }

        // Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
        {
            m_animator.SetTrigger("Hurt");
        }

        // Ataque
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            PerformAttack();
            HandleAttack();
        }

        // Bloquear
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            TriggerBlockAnimation();
        }

        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
        }

        // Rollo
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            PerformRoll();
        }

        // Saltar
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            PerformJump();
        }

        // Correr
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reiniciar temporizador
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

       // Inactividad
       else
       {
           // Evitar transiciones parpadeantes a la inactividad
           m_delayToIdle -= Time.deltaTime;
           if (m_delayToIdle < 0)
           {
               m_animator.SetInteger("AnimState", 0);
           }
       }
    }

    private void MoveColliderWithCharacter(float inputX)
    {
        if (knightColliderContainer != null)
        {
            float newXPosition = m_facingDirection == -1 ? -1f : 0f;
            knightColliderContainer.transform.localPosition = new Vector3(newXPosition, 0, 0);
        }
    }

    private void FlipCharacter(bool flipX)
    {
        GetComponent<SpriteRenderer>().flipX = flipX;
        m_facingDirection = flipX ? -1 : 1;
    }

    private void TriggerDeathAnimation()
    {
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
    }

    private void PerformAttack()
    {
        m_currentAttack++;

        // Volver a uno después del tercer ataque
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reiniciar el combo de ataque si el tiempo desde el último ataque es demasiado grande
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;

        // Llamar a una de las tres animaciones de ataque "Attack1", "Attack2", "Attack3"
        m_animator.SetTrigger("Attack" + m_currentAttack);

        // Reiniciar temporizador
        m_timeSinceAttack = 0.0f;
    }
    private void HandleAttack()
    {
        // Obtener el tamaño del BoxCollider2D del enemigo
        Vector2 boxSize = new Vector2(1.5f, 1.5f);

        // Lógica para verificar y dañar enemigos
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyInterface enemy = hitCollider.GetComponent<EnemyInterface>();

                if (enemy != null)
                {
                    enemy.TakeDamage(); // Llama al método de la interfaz
                }
            }
        }
    }

    private void TriggerBlockAnimation()
    {
        m_animator.SetTrigger("Block");
        m_animator.SetBool("IdleBlock", true);
    }

    private void PerformRoll()
    {
        m_rolling = true;
        m_animator.SetTrigger("Roll");
        m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
    }

    private void PerformJump()
    {
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    // Función de evento para la animación de deslizamiento de polvo
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Establecer la posición de spawn correcta
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Girar el polvo en la dirección correcta
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}
