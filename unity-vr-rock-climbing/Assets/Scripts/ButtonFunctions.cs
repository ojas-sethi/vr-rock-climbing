using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
	public Relay relay;

	private string joinCode;
	public TMPro.TMP_InputField inputTextMeshPro;

<<<<<<< Updated upstream
	//private TouchScreenKeyboard keyboard;

	//public void ShowKeyboard()
	//{
	//	keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
	//}
=======
	private TouchScreenKeyboard keyboard;

	public void ShowKeyboard()
	{
		keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
	}
>>>>>>> Stashed changes

	public void ClientInput()
	{
		joinCode = inputTextMeshPro.text;
	}

	public void Host()
	{
		print("Hosted");
		relay.AllocateRelay();
	}
	public void Client()
	{
		print("Joined as Client");
<<<<<<< Updated upstream
		relay.JoinRelay(joinCode);
=======
		relay.JoinRelay();
>>>>>>> Stashed changes
	}
}
