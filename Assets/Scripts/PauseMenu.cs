/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] GameObject pauseScreen = default;

	private bool isPaused = false;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			PauseGame();
		}
	}

	private void PauseGame()
	{
		if(!isPaused)
		{
			pauseScreen.SetActive(true);
			isPaused = true;

			Time.timeScale = 0f;
			AudioListener.pause = true;
		}
		else
		{
			ResumeGame();
		}
	}

	private void ResumeGame()
	{
		pauseScreen.SetActive(false);
		isPaused = false;

		Time.timeScale = 1f;
		AudioListener.pause = false;
	}

	public void Resume()
	{
		ResumeGame();
	}

	public void LoadMainMenu()
	{
		Time.timeScale = 1f;
		if(FindObjectsOfType<GameSession>().Length > 0)
		{
			FindObjectOfType<GameSession>().RestartGame();
		}

		FindObjectOfType<LevelController>().LoadStartScene();
	}
}

