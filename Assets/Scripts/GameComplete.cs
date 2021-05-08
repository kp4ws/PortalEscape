/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;

public class GameComplete : MonoBehaviour
{
	[SerializeField] GameObject gameCompleteCanvas = default;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameCompleteCanvas.SetActive(true);
	}
}

