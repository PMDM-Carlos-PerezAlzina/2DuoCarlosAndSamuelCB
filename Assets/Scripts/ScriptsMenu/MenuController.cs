using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Carga la escena del juego
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        // Carga la escena de configuración
        SceneManager.LoadScene("Settings");
    }

    public void OpenHowTo()
    {
        // Carga la escena de cómo jugar
        SceneManager.LoadScene("HowTo");
    }

    public void OpenInfo()
    {
        // Carga la escena de información
        SceneManager.LoadScene("Info");
    }

    public void ExitGame()
    {
        // Sale del juego
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}