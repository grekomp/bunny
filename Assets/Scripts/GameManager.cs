using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public static UIManager ui;

	public static bool paused = true;
	public static bool showNewgameMenu = true;

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

			Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);
			cursorLocation = new Vector3();

			Pause();
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
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
		if (Input.GetButtonDown("Cancel"))
		{
			TogglePause();
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

	public void TogglePause()
	{
		paused = !paused;
		if (paused)
		{
			Pause();
		} else
		{
			Play();
		}
	}

	public void Play()
	{
		paused = false;
		Time.timeScale = 1;
		showNewgameMenu = false;
		ui.HideMenu();
	}

	public void Pause()
	{
		paused = true;
		Time.timeScale = 0;
		ui.ShowMenu();
	}

	public void Resume()
	{
		paused = false;
		Time.timeScale = 1;
		ui.HideMenu();
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
		Pause();
		ui.PlayerDead();
	}

	public void Exit()
	{
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#else
				 Application.Quit();
		#endif
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
