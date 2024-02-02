using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Sprite imageMuted; // La imagen para cuando la m�sica est� silenciada
    public Sprite imageNotMuted; // La imagen para cuando la m�sica est� sonando
    public Image imageMusic;

    public void ToggleMute()
    {

        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer != null)
        {
            // Si se encontr� el objeto MusicPlayer, llama a su m�todo ToggleMute
            musicPlayer.ToggleMute();

            // Cambia la visibilidad de las im�genes dependiendo de si la m�sica est� silenciada o no
            if (musicPlayer.IsMutedMusic())
            {
                imageMusic.sprite = imageMuted; // Cambia el sprite a la imagen de m�sica silenciada
            }
            else
            {
                imageMusic.sprite = imageNotMuted; // Cambia el sprite a la imagen de m�sica no silenciada
            }
        }
        else
        {
            Debug.Log("No se encontr� el objeto MusicPlayer");
        }
    }

}
