using UnityEngine;

public class ConfettiExplosion : MonoBehaviour
{
    public ParticleSystem confettiExplosion; // Asegúrate de asignar tu sistema de partículas en el Inspector de Unity

    void Start()
    {
        confettiExplosion.Play(); // Inicia la reproducción del sistema de partículas
    }

    void Update()
    {
        if (!confettiExplosion.isPlaying)
        {
            confettiExplosion.Play(); // Si el sistema de partículas no está reproduciéndose, inicia la reproducción
        }
    }
}
