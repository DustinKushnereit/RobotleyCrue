//-----------------------------------------------------------
// Xbox 360 Gamepad 'Testing' script
// Bonus Testing code by Lawrence M
//
// Disclaimer:
//-----------------------------------------------------------
// * Code in this script is provided AS IS and is intended
//   to demonstrate using the GamepadManager system and
//   reading input from a gamepad.
//
// * This script is NOT guaranteed to work for 3rd-Party
//   (non Microsoft official) controllers.
//
// * Script tested in Unity 5.0.0f4 'Personal' on a Windows
//   7 PC, using a Microsoft Xbox 360 controller (wired USB).
//
// USE: Attach this script to the main camera or any active
//      gameObject in the scene to use it.
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

// Sample testing script
public class TestGamepad : MonoBehaviour
{
	x360_Gamepad gamepad; // Gamepad instance

	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update()
	{
		// Obtain the desired gamepad from GamepadManager
		gamepad = GamepadManager.Instance.GetGamepad(1);

		// Sample code to test button input and rumble
		if (gamepad.GetButtonDown("A")) {
			TestRumble();
			Debug.Log("A down");
		}
	}

	// Send some rumble events to the gamepad
	void TestRumble()
	{
		//                timer            power         fade
		gamepad.AddRumble(1.0f, new Vector2(0.9f, 0.9f), 0.5f);
		gamepad.AddRumble(2.5f, new Vector2(0.5f, 0.5f), 0.2f);
	}
}
