﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int score;
	public int highScore;

	public int level;
	public float difficultyModifier;

	public GameObject player;

	public Text scoreText;
	public Text highScoreText;
	public Text ammoText;
	public Slider ammoSlider;
	public Text bombsText;
	public Text maxBombsText;
	public Text healthText;
	public Slider healthSlider;
	public Text levelText;
	public Text ammoLowText;

	public Vector3 cursorLocation;

	public Texture2D cursorTexture;
	public Vector2 cursorHotSpot = new Vector2(16, 16);

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

		level = 1;
		difficultyModifier = 1f;

		Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);
		cursorLocation = new Vector3();

		// Getting all ui objects
		player = GameObject.FindGameObjectWithTag("Player");
		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		highScoreText = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<Text>();
		ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
		ammoSlider = GameObject.Find("AmmoSlider").GetComponent<Slider>();
		bombsText = GameObject.Find("BombsText").GetComponent<Text>();
		maxBombsText = GameObject.Find("MaxBombsText").GetComponent<Text>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		ammoLowText = GameObject.Find("AmmoLowText").GetComponent<Text>();

		UpdateScore();
		UpdateLevel();
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

	public void UpdateLevel()
	{
		levelText.text = "Level " + level.ToString();
		levelText.gameObject.GetComponent<Animator>().SetTrigger("NextLevel");
	}

	public void NextLevel()
	{
		level++;
		UpdateLevel();
	}

	public void Exit()
	{
		Application.Quit();
	}

	// Utility methods
	public static bool RandomOnNavmesh(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 10; i++)
		{
			Vector2 randomPointInCircle = Random.insideUnitCircle * range;
			Vector3 randomPoint = center;
			randomPoint.x += randomPointInCircle.x;
			randomPoint.z += randomPointInCircle.y;

			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
			{
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}
}
