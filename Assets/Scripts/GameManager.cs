using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int score;
	public int highScore;
	public GameObject player;

	public Text scoreText;
	public Text highScoreText;
	public Text ammoText;
	public Slider ammoSlider;
	public Text bombsText;
	public Text maxBombsText;
	public Text healthText;
	public Slider healthSlider;

	public Vector3 cursorLocation;

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

		player = GameObject.FindGameObjectWithTag("Player");
		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		highScoreText = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<Text>();
		ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
		ammoSlider = GameObject.Find("AmmoSlider").GetComponent<Slider>();
		bombsText = GameObject.Find("BombsText").GetComponent<Text>();
		maxBombsText = GameObject.Find("MaxBombsText").GetComponent<Text>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

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
