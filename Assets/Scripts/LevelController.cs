/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelController : MonoBehaviour
{
	private const int LEVEL_ONE = 1;
	private int currentSceneIndex;
	private float levelLoadDelay = 0.2f;

	private void Start()
	{
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	public int GetCurrentLevel()
	{
		return currentSceneIndex;
	}

	public void LoadStartScene()
	{
		//Assuming the start screen is at index 0
		FindObjectOfType<GameSession>().RestartGame();
		SceneManager.LoadScene(0);
	}

	public void LoadGameOverScene()
	{
		FindObjectOfType<GameSession>().RestartGame();
		SceneManager.LoadScene("Gameover Screen");
	}

	public void LoadFirstLevel()
	{
		// TODO FindObjectOfType<GameSession>().RestartGame(); => Not sure if I'll need this?
		SceneManager.LoadScene(LEVEL_ONE);
		//currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	public void LoadNextLevel()
	{
		StartCoroutine(NextLevelDelay());
	}

	private IEnumerator NextLevelDelay()
	{
		yield return new WaitForSeconds(levelLoadDelay);
		SceneManager.LoadScene(currentSceneIndex + 1);
	}

	public void LoadWinScene()
	{
		SceneManager.LoadScene("Win Screen");
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(currentSceneIndex);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

