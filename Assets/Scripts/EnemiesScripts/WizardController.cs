using UnityEngine;

public class WizardController : EnemyInterface
{
    private Animator wizardAnimator;
    public GameObject deathObject;

    // Start is called before the first frame update
    void Start()
    {
        wizardAnimator = GetComponent<Animator>();

        // Invoca el método AttackAnimation cada 5 segundos, empezando después de 0 segundos
        InvokeRepeating("AttackAnimation", 0f, 5f);
    }

    // Método para la animación de ataque
    void AttackAnimation()
    {
        // Asegúrate de que el componente Animator esté adjunto al objeto
        if (wizardAnimator != null)
        {
            // Reproduce la animación "wizard_attack"
            wizardAnimator.Play("wizard_attack");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (base.life <= 0) {
            base.Die(gameObject, deathObject);
        }
    }

    public override void Move(){
        
    }
}
