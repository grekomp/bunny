using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public static UIManager ui;

	public int score;
	public int highScore;

	public int level;
	public float difficultyModifier;

	public bool playerAlive = true;

	public GameObject player;

	public Vector3 cursorLocation;

	public Texture2D cursorTexture;
	public Vector2 cursorHotSpot = new Vector2(16, 16);

	private void Awake()
	{
		if (instance == null || instance == this)
		{
			instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);
		cursorLocation = new Vector3();
	}

	public void PrepareScene()
	{
		level = 1;
		difficultyModifier = 1f;

		ui.UpdateScore(score, highScore);
		ui.UpdateLevel(level);
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

		ui.UpdateScore(this.score, highScore);
	}

	public void NextLevel()
	{
		level++;
		ui.UpdateLevel(level);
	}

	public void RestartGame()
	{
		Debug.Log("Restarting");

		level = 1;
		score = 0;
		playerAlive = true;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void PlayerDead()
	{
		playerAlive = false;
		ui.PlayerDead();
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
