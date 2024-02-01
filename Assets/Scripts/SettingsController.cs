using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Image imageMuted; // La imagen para cuando la música está silenciada
    public Image imageNotMuted; // La imagen para cuando la música está sonando



    public void ToggleMute()
    {
        // Encuentra el objeto MusicPlayer en la escena
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer != null)
        {
            // Si se encontró el objeto MusicPlayer, llama a su método ToggleMute
            musicPlayer.ToggleMute();

            // Cambia la visibilidad de las imágenes dependiendo de si la música está silenciada o no
            if (musicPlayer.IsMutedMusic())
            {
                imageMuted.enabled = true;
                imageNotMuted.enabled = false;
            }
            else
            {
                imageMuted.enabled = false;
                imageNotMuted.enabled = true;
            }
        }
        else
        {
            Debug.Log("No se encontró el objeto MusicPlayer");
        }
    }
}
