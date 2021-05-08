/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;

[CreateAssetMenu(menuName ="HelpState")]
public class HelpState : ScriptableObject
{
	[TextArea(10, 14)] [SerializeField] private string helpText = default;
	[SerializeField] HelpState nextState;

	public string GetText()
	{
		return helpText;
	}

	public HelpState GetNextState()
	{
		return nextState;
	}
}

