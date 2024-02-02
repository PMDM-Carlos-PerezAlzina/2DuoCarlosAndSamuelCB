using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Sprite imageMuted; // La imagen para cuando la música está silenciada
    public Sprite imageNotMuted; // La imagen para cuando la música está sonando
    public Image imageMusic;

    public void ToggleMute()
    {

        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer != null)
        {
            // Si se encontró el objeto MusicPlayer, llama a su método ToggleMute
            musicPlayer.ToggleMute();

            // Cambia la visibilidad de las imágenes dependiendo de si la música está silenciada o no
            if (musicPlayer.IsMutedMusic())
            {
                imageMusic.sprite = imageMuted; // Cambia el sprite a la imagen de música silenciada
            }
            else
            {
                imageMusic.sprite = imageNotMuted; // Cambia el sprite a la imagen de música no silenciada
            }
        }
        else
        {
            Debug.Log("No se encontró el objeto MusicPlayer");
        }
    }

}
