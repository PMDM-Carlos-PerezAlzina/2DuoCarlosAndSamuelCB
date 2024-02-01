using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioSource buttonSound; // El AudioSource que reproducirá el sonido

    public void PlayGame()
    {
        PlaySound();
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        PlaySound();
        SceneManager.LoadScene("Settings");
    }

    public void OpenHowTo()
    {
        PlaySound();
        SceneManager.LoadScene("HowTo");
    }

    public void OpenInfo()
    {
        PlaySound();
        SceneManager.LoadScene("Info");
    }

    public void ExitGame()
    {
        PlaySound();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void BackMainMenu()
    {
        PlaySound();
        SceneManager.LoadScene("MainMenu");
    }

    private void PlaySound()
    {
        // Reproduce el sonido
        buttonSound.Play();
    }
}
