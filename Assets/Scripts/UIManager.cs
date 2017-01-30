using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
	public Text ammoText;
	public Slider ammoSlider;
	public Text bombsText;
	public Text maxBombsText;
	public Text healthText;
	public Slider healthSlider;
	public Text levelText;
	public Text levelCountdownText;
	public Text ammoLowText;
	public GameObject gameOverUI;
	public Button restartButton;
	public Button exitButton;
	public Button playButton;
	public GameObject pauseMenu;

	private void Awake()
	{
		GameManager.ui = this;

		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		highScoreText = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<Text>();
		ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
		ammoSlider = GameObject.Find("AmmoSlider").GetComponent<Slider>();
		bombsText = GameObject.Find("BombsText").GetComponent<Text>();
		maxBombsText = GameObject.Find("MaxBombsText").GetComponent<Text>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelCountdownText = GameObject.Find("LevelCountdownText").GetComponent<Text>();
		ammoLowText = GameObject.Find("AmmoLowText").GetComponent<Text>();
		gameOverUI = GameObject.Find("GameOverUI");
		playButton = GameObject.Find("ResumeButton").GetComponent<Button>();
		playButton.onClick.AddListener(PlayGame);
		restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
		restartButton.onClick.AddListener(RestartGame);
		exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
		exitButton.onClick.AddListener(ExitGame);
		pauseMenu = GameObject.Find("PauseMenu");

		if(!GameManager.showNewgameMenu)
		{
			GameManager.instance.Play();
		}
	}

	public void UpdateScore(int score, int highScore)
	{
		scoreText.text = score.ToString();
		highScoreText.text = highScore.ToString();
	}

	public void UpdateLevel(int level)
	{
		levelText.text = "Level " + level.ToString();
		levelText.gameObject.GetComponent<Animator>().SetTrigger("NextLevel");
	}

	public void PlayerDead()
	{
		gameOverUI.GetComponent<Animator>().SetBool("GameOverOn", true);
		playButton.interactable = false;
	}

	public void ShowMenu()
	{
		pauseMenu.GetComponent<Animator>().SetBool("MenuOn", true);
	}

	public void HideMenu()
	{
		pauseMenu.GetComponent<Animator>().SetBool("MenuOn", false);
		gameOverUI.GetComponent<Animator>().SetBool("GameOverOn", false);
	}

	void PlayGame()
	{
		GameManager.instance.Play();
	}

	void RestartGame()
	{
		GameManager.instance.RestartGame();
	}

	void ExitGame()
	{
		GameManager.instance.Exit();
	}
}
