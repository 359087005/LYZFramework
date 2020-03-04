using UnityEngine;
using System.Collections;

public class StopPlayerRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetBeginDrag((go) => 
        FirstViewControl.instance.SetPlayerState(PlayerControlState.noUse));
        gameObject.SetEndDrag((go) => 
        FirstViewControl.instance.SetPlayerState(PlayerControlState.playerControl));
	}
	
	
}
