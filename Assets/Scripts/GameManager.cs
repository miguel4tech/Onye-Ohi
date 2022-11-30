using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;
    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    private bool isGameStarted = false;
    private PlayerMotor motor;

    // UI and the UI fields
    public TextMeshProUGUI scoreText, coinText, modifierText;
    private float score, coinScore,modifierScore;
    private int lastScore;

    // Death Menu
    public TextMeshProUGUI deadScoreText, deadCoinText;
    private GameObject deathMenu;

    private void Awake() 
    {
        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        deathMenu = GameObject.FindGameObjectWithTag("DeathMenu");

        scoreText.text = "Score: " + score.ToString("0");
        coinText.text = "Coin Score: " + coinScore.ToString("0");
        modifierText.text = "Speed: " + modifierScore.ToString("0.0"); 
    }
    
    private void Start() 
    {    
        deathMenu.SetActive(false);
    }

    private void Update() 
    {
        if(MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<GlacierSpawner>().IsScrolling = true;
        }

        if(isGameStarted && !IsDead)
        {
            // Bump the score up            
            score += (Time.deltaTime * modifierScore);
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = "Score: " + score.ToString("0");
            }            
        }
    }

    public void GetCoin()
    {
        coinScore++;
        coinText.text = "Coin Score: " + coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = "Coin: " + score.ToString("0");
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "Speed: " + modifierScore.ToString("0.0");
    }

    // Play Button when Obi Dies (Button to restart game)  

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        deathMenu.SetActive(false);
    }

    public void OnDeath()
    {
        IsDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deadScoreText.text = "Score: " + score.ToString("0");
        deadCoinText.text = "Coins: " + coinScore.ToString("0");
        
        deathMenu.SetActive(true);
    }


}
