using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
    [SerializeField] private float m_speed = 4.0f;
    [SerializeField] private float m_jumpForce = 7.5f;
    [SerializeField] private float m_rollForce = 6.0f;
    [SerializeField] private bool m_noBlood = false;
    [SerializeField] private GameObject m_slideDust;
    [SerializeField] private Vector2 reboundSpeed;
    public int torchQuantity = 3;
    public int lifePotionQuantity = 1;
    public int sanityPotionQuantity = 1;

    public float life = 100f;
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    public float sanity = 100f;
    private float lastDamageTime;
    public float damageCooldown = 5f;
    public float damageRadius = 5f;
    public GameObject torch;
    private bool isInsideLight = false;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public Canvas canvas;
    public Button objectButton;
    private bool isLeftButtonPressed = false;
    private bool isRightButtonPressed = false;
    private float inputX;
    public Sprite[] sprites;
    public Image imageButton;


    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
        InvokeRepeating("DecreaseSanityEverySecond", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimers();

        CheckGroundStatus();

        if (isLeftButtonPressed)
        {
            inputX = -1f;
            FlipCharacter(true);
        }
        else if (isRightButtonPressed)
        {
            inputX = 1f;
            FlipCharacter(false);
        }
        else
        {
            inputX = 0f;
        }

        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Correr
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
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

        if (life <= 0)
        {
            TriggerDeathAnimation();
            SceneManager.LoadScene("DeathScene");
        }

        lastDamageTime = -damageCooldown;
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
        float inputX = Input.touchCount > 0 ? Mathf.Sign(Input.GetTouch(0).position.x - Screen.width / 2) : 0;

        // Cambiar dirección del sprite según la dirección de movimiento
        if (inputX > 0)
        {
            FlipCharacter(false);
        }
        else if (inputX < 0)
        {
            FlipCharacter(true);
        }

        // Move the collider with the player
        MoveColliderWithCharacter();

        // Move
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

        // Ataque
        //if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        //{
            //PerformAttack();
          //  HandleAttack();
        //}

        // Bloquear
        if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            Text[] texts = canvas.GetComponentsInChildren<Text>();
            if (torchQuantity > 0) {
                torchQuantity--;
                Instantiate(torch, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            }
        }

        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
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

    private void MoveColliderWithCharacter()
    {
        //if (knightColliderContainer != null)
        //{
            
            //knightColliderContainer.transform.localPosition = new Vector3(newXPosition, 0, 0);
        //}
        float newXPosition = m_facingDirection == -1 ? -1f : 0f;
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
        // Posición del jugador
        Vector3 playerPosition = transform.position;

        // Obtener los colliders de los objetos dentro del área de daño
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, damageRadius);
        
        foreach (Collider2D collider in colliders)
        {
            // Aplicar daño a los objetos en el radio del ataque
            if (collider.CompareTag("Enemy"))
            {
                EnemyInterface enemy = collider.GetComponent<EnemyInterface>();

                if (enemy != null)
                {
                    enemy.TakeDamage(); // Llama al método de la interfaz
                }
            }
        }
    }

    private void PerformJump()
    {
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    private IEnumerator ResetWizardAnimationToIdle(Animator wizardAnimator)
    {
        yield return new WaitForSeconds(1f);
        wizardAnimator.SetTrigger("Idle");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light")) {
            isInsideLight = true;
        }
        if (other.CompareTag("Enemy") || other.CompareTag("Trap"))
        {
            if (other.GetComponent<WizardController>() != null)
            {
                // Obtener el Animator del Wizard
                Animator wizardAnimator = other.GetComponent<WizardController>().GetComponent<Animator>();

                // Activar la animación de ataque del Wizard
                wizardAnimator.SetTrigger("Attack");

                // Esperar un segundo y luego restablecer la animación del Wizard al estado Idle
                StartCoroutine(ResetWizardAnimationToIdle(wizardAnimator));
            }

            // Realizar la animación de daño del jugador y aplicar el daño
            m_animator.SetTrigger("Hurt");
            TakeDamage();
        }
        if (other.CompareTag("EndFirstLevel")) {
            SceneManager.LoadScene("SecondLevel");
        }
        if (other.CompareTag("EndSecondLevel")) {
            SceneManager.LoadScene("WinLevelScene");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light")) {
            isInsideLight = false;
        }
    }

    private void SpawnRandomEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Seleccionar aleatoriamente entre los tres prefabs
            GameObject selectedPrefab = GetRandomEnemyPrefab();

            // Calcular la posición en el eje X entre 0 y 191
            float randomX = Random.Range(0f, 192f);

            // Fijar la posición en el eje Y a una altura constante de 2
            float fixedY = 2f;

            // Hacer spawn del enemigo en la posición aleatoria
            Instantiate(selectedPrefab, new Vector3(randomX, fixedY, 0f), Quaternion.identity);
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                return enemyPrefab1;
            case 1:
                return enemyPrefab2;
            case 2:
                return enemyPrefab3;
            default:
                return enemyPrefab1;
        }
    }

    private void DecreaseSanityEverySecond()
    {
        if (!isInsideLight)
        {
            if (sanity > 0)
            {
                sanity -= 1f;
            }

            if (sanity <= 0)
            {
                // Hacer spawn de 20 enemigos de manera aleatoria
                SpawnRandomEnemies(10);

                // Comenzar a disminuir la vida cada segundo
                InvokeRepeating("DecreaseLifeEverySecond", 0f, 7.5f);
            }
        }
    }

    private void DecreaseLifeEverySecond()
    {
        life -= 1;
    }

    public void TakeDamage()
    {
        life -= GameManager.damagePlayer;

        Vector2 knockbackForce = new Vector2(-5f * m_facingDirection, 5f);
        m_body2d.velocity = Vector2.zero; 
        m_body2d.AddForce(knockbackForce, ForceMode2D.Impulse);
    }

    public void IncreasePotionSanity() {
        sanityPotionQuantity++;
    }

    public void IncreasePotionLife() {
        lifePotionQuantity++;
    }

    public void IncreaseTorch() {
        torchQuantity++;
    }

    public void HandleLeftButtonClick()
    {
        isLeftButtonPressed = true;
    }

    public void HandleRightButtonClick()
    {
        isRightButtonPressed = true;
    }
    
    public void HandleLeftButtonRelease()
    {
        isLeftButtonPressed = false;
    }

    public void HandleRightButtonRelease()
    {
        isRightButtonPressed = false;
    }

    public void HandleAttackButtonClick()
    {
        if (m_timeSinceAttack > 0.25f && !m_rolling)
        {
            PerformAttack();
            HandleAttack();
        }
    }

    public void HandleJumpButtonClick()
    {
        if (m_grounded && !m_rolling)
        {
            PerformJump();
        }
    }

    public void OnClickSelectItemTorch(){
        imageButton.sprite = sprites[2];
    }

    public void OnClickSelectItemHeal(){
        imageButton.sprite = sprites[0];
    }

    public void OnClickSelectItemSanity(){
        imageButton.sprite = sprites[1];
    }

    public void OnClickObjectButton(){
        if (imageButton.sprite == sprites[0]){
            if(lifePotionQuantity > 0){
                lifePotionQuantity--;
                life += 20;
            }
        } else if (imageButton.sprite == sprites[1]){
                if(sanityPotionQuantity > 0){
                sanityPotionQuantity--;
                sanity += 20;
            }
        } else if(imageButton.sprite == sprites[2]){
                if (torchQuantity > 0){
                    torchQuantity--;
                    Instantiate(torch, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                }
        }
    }
}