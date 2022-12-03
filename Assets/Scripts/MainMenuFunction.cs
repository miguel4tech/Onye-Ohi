using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunction : MonoBehaviour
{
    public AudioSource buttonPress;

    public void PlayGame()
    {
        buttonPress.Play();
        SceneManager.LoadScene(2);
    }
    public void ExitGame()
    {
        buttonPress.Play();
        SceneManager.LoadScene(1);
    }
}
