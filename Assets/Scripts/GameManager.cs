using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int COIN_SCORE_AMOUNT = 5;
    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    private bool isGameStarted = false;
    public static bool isGamePaused;
    private PlayerMotor motor;

    // UI and the UI fields
    public TextMeshProUGUI scoreText, coinText, modifierText,hiscoreText;
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

        scoreText.text = score.ToString("0");
        coinText.text = "" + coinScore.ToString("0");
        modifierText.text = "X" + modifierScore.ToString("0.0"); 
        hiscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
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
            FindObjectOfType<CameraMotor>().IsMoving = true;
        }

        if(isGameStarted && !IsDead)
        {
            //Bump up the score
			lastScore = (int)score;
			score += (Time.deltaTime * modifierScore);
			if (lastScore == (int)score) 
			{
				scoreText.text = score.ToString ("0");
            }            
        }
    }

    public void GetCoin()
    {
        coinScore++;
        coinText.text = "" + coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = "" + score.ToString("0");
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "X" + modifierScore.ToString("0.0");
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
        deathMenu.SetActive(true);
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deadScoreText.text = "Score: " + score.ToString("0");
        deadCoinText.text = "Coins: " + coinScore.ToString("0");
        
        //Checks if this is a highscore
		if(score > PlayerPrefs.GetInt("Highscore"))
		{
			float s = score;
			if(s % 1 == 0)
				s += 1;
			PlayerPrefs.SetInt("Highscore", (int)s); //Sets/Put current score as highscore
		}
    }

    public void Pause()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}
    public void Quit()
    {
        print("Game Exit");
        Application.Quit();
    }

}
