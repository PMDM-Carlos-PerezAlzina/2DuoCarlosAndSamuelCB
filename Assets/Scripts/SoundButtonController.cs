using UnityEngine;

public class SoundButtonController : MonoBehaviour
{
    public void SetVolumeTo25()
    {
        SetVolume(0.25f); // Cambia el volumen de la música al 25%
    }

    public void SetVolumeTo50()
    {
        SetVolume(0.5f); // Cambia el volumen de la música al 50%
    }

    public void SetVolumeTo100()
    {
        SetVolume(1f); // Cambia el volumen de la música al 100%
    }

    private void SetVolume(float volume)
    {
        // Encuentra el objeto MusicPlayer en la escena
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();

        if (musicPlayer != null)
        {
            // Si se encontró el objeto MusicPlayer, cambia el volumen de la música
            musicPlayer.SetVolume(volume);
        }
        else
        {
            Debug.Log("No se encontró el objeto MusicPlayer");
        }
    }
}
