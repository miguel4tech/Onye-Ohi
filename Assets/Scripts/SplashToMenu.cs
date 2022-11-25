using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashToMenu : MonoBehaviour
{
    public GameObject theDetails;

    
    void Start()
    {
        StartCoroutine(RunSplash());
    }

    IEnumerator RunSplash()
    {
        yield return new WaitForSeconds(0.5f);
        theDetails.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(1);
    }
    
}
