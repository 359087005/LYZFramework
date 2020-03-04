using UnityEngine;
using System.Collections;

public class SmallScene : MonoBehaviour {

    public GameObject cube;
    public ControlRot controlRot;
	void Start () {
        cube.OneselfName("cube");

        controlRot.MoveToDestination(new Vector3(3.415504f, -0.1725965f, 0.8631858f), new Vector3(33.5001f, 335.2501f, 0), 10, 1, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
