using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void PlayGame()
    {
        PlaySound();
        SceneManager.LoadScene("FirstLevel");
    }

    public void PlaySecondLevel()
    {
        PlaySound();
        SceneManager.LoadScene("SecondLevel");
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
    // Find the SoundButton object
    GameObject soundButtonObject = GameObject.Find("SoundButton");
    if (soundButtonObject != null)
    {
        // Get the SoundButton component and play the sound
        SoundButton soundButton = soundButtonObject.GetComponent<SoundButton>();
        if (soundButton != null)
        {
            soundButton.PlaySound();
        }
    }
}

}
