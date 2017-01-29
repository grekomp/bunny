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
	GameObject gameOverUI;
	Button restartButton;

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
		restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
		restartButton.onClick.AddListener(RestartGame);
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
		gameOverUI.GetComponent<Animator>().SetTrigger("GameOver");
	}

	void RestartGame()
	{
		GameManager.instance.RestartGame();
	}
}
