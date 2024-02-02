using UnityEngine;

public class ConfettiExplosion : MonoBehaviour
{
    public ParticleSystem confettiExplosion; // Aseg�rate de asignar tu sistema de part�culas en el Inspector de Unity

    void Start()
    {
        confettiExplosion.Play(); // Inicia la reproducci�n del sistema de part�culas
    }

    void Update()
    {
        if (!confettiExplosion.isPlaying)
        {
            confettiExplosion.Play(); // Si el sistema de part�culas no est� reproduci�ndose, inicia la reproducci�n
        }
    }
}
