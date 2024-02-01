using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Image imageMuted; // La imagen para cuando la m�sica est� silenciada
    public Image imageNotMuted; // La imagen para cuando la m�sica est� sonando



    public void ToggleMute()
    {
        // Encuentra el objeto MusicPlayer en la escena
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer != null)
        {
            // Si se encontr� el objeto MusicPlayer, llama a su m�todo ToggleMute
            musicPlayer.ToggleMute();

            // Cambia la visibilidad de las im�genes dependiendo de si la m�sica est� silenciada o no
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
            Debug.Log("No se encontr� el objeto MusicPlayer");
        }
    }
}
