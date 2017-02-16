using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressCheck : MonoBehaviour {

	public bool pressed;

	void move() {
		pressed = true;
	}

	void stop() {
		pressed = false;
	}
}
