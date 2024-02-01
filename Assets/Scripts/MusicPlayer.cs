using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance = null; // Instancia estática del MusicPlayer

    public AudioSource audioSource;
    private bool isMuted = false; // Variable para controlar si la música está silenciada

    void Awake()
    {
        // Si ya hay una instancia de MusicPlayer y no es esta, destruye este objeto
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Si no hay una instancia de MusicPlayer, esta se convierte en la instancia
        instance = this;

        // Hace que este objeto no se destruya al cargar nuevas escenas
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource.loop = true; // Habilita la reproducción en bucle
        audioSource.Play(); // Comienza a reproducir la canción
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            // Si la música está silenciada, la reanuda
            audioSource.UnPause();
        }
        else
        {
            // Si la música está sonando, la silencia
            audioSource.Pause();
        }
    }

    public bool IsMutedMusic()
    {
        return isMuted;
    }
}
