using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance = null; // Instancia est�tica del MusicPlayer

    public AudioSource audioSource;
    private bool isMuted = false; // Variable para controlar si la m�sica est� silenciada

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
        audioSource.loop = true; // Habilita la reproducci�n en bucle
        audioSource.Play(); // Comienza a reproducir la canci�n
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            // Si la m�sica est� silenciada, la reanuda
            audioSource.UnPause();
        }
        else
        {
            // Si la m�sica est� sonando, la silencia
            audioSource.Pause();
        }
    }

    public bool IsMutedMusic()
    {
        return isMuted;
    }

    public void SetVolume(float volume){
        audioSource.volume = volume;
    }
}
