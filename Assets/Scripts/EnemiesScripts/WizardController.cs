using UnityEngine;

public class WizardController : EnemyInterface
{
    private Animator wizardAnimator;

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
        // Aquí puedes poner lógica adicional si es necesario
    }
}
