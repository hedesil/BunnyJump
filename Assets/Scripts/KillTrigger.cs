using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D theObject) {
		if (theObject.tag == "Player") {
			PlayerController.PlayerSharedInstance.KillPlayer();
			
		}
	}
}