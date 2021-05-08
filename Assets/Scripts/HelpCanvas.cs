/*
* Copyright (c) Kp4ws
*
*/

using System;
using UnityEngine;
using TMPro;

public class HelpCanvas : MonoBehaviour
{
	[SerializeField] GameObject portal;
	[SerializeField] TextMeshProUGUI helpText;
	[SerializeField] HelpState startingState;

	private bool moved;
	private bool jumped;
	private int helpIndex;
	private HelpState state;

	private void Start()
	{
		state = startingState;
		helpText.text = state.GetText();
	}

	private void Update()
	{
		if (!moved)
		{
			moved = CheckMove();
			return;
		}
		else if (!jumped)
		{
			ShowNextMessage();
			jumped = CheckJump();
			return;
		}
		else if (moved && jumped)
		{
			ShowNextMessage();
			portal.SetActive(true);
			moved = false;
			jumped = false;
			return;
		}
		else
		{
			Debug.LogError("Unexpected Behaviour");
			return;
		}
	}

	private void ShowNextMessage()
	{
		state = state.GetNextState();
		helpText.text = state.GetText();
	}

	private bool CheckMove()
	{
		return (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));
	}

	private bool CheckJump()
	{
		return (Input.GetKeyDown(KeyCode.Space));
	}
}

