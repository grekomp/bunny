using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int score;
	public int highScore;
	public GameObject player;

	Text scoreText;
	Text highScoreText;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		highScoreText = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<Text>();
		player = GameObject.FindGameObjectWithTag("Player");

		UpdateScore();
	}

	private void Update()
	{
		if (Input.GetButton("Cancel"))
		{
			Exit();
		}
	}

	public void AddScore(int score)
	{
		this.score += score;
		if(this.score > highScore)
		{
			highScore = score;
		}

		UpdateScore();
	}

	public void UpdateScore()
	{
		scoreText.text = score.ToString();
		highScoreText.text = highScore.ToString();
	}

	public void Exit()
	{
		Application.Quit();
	}
}
