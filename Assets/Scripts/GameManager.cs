using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    private PlayerMotor motor;

    // UI and the UI fields
    public TextMeshProUGUI scoreText, coinText, modifierText;
    private float score, coinScore,modifierScore;

    private void Awake() 
    {
        Instance = this;
        UpdateScores();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }
    private void Update() 
    {
        if(MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
        }
    }

    public void UpdateScores()
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
        modifierText.text = modifierScore.ToString();
    }

    //Play Button when Obi Dies (Button to restart game)

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }


}
