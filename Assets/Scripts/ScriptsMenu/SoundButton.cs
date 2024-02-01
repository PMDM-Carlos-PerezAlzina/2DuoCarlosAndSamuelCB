using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundButton : MonoBehaviour
{
    public AudioSource buttonSound;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound()
    {
        buttonSound.Play();
    }
}
