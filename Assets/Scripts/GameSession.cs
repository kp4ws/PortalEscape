/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using TMPro;
using System.Collections;

public class GameSession : MonoBehaviour
{
	[SerializeField] int playerLives = 3;
	[SerializeField] TextMeshProUGUI livesText = default;
	private float deathWaitTime = 1f;

	private const string LIVES_STRING = "Lives: ";

	private void Awake()
	{
		int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
		if(numOfGameSessions > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		UpdateLivesDisplay();
	}

	private void UpdateLivesDisplay()
	{
		livesText.text = LIVES_STRING + playerLives.ToString();
	}

	public void ProcessPlayerDeath()
	{
		StartCoroutine(PlayerDeathDelay());
	}

	private IEnumerator PlayerDeathDelay()
	{
		yield return new WaitForSeconds(deathWaitTime);

		if (playerLives > 0)
		{
			TakeLife();
		}
		else
		{
			GameOver();
		}
	}

	private void TakeLife()
	{
		playerLives--;
		FindObjectOfType<LevelController>().RestartScene();
		UpdateLivesDisplay();
	}

	private void GameOver()
	{
		FindObjectOfType<LevelController>().LoadGameOverScene();
		Destroy(gameObject);
	}

	public void RestartGame()
	{
		Destroy(gameObject);
	}
}