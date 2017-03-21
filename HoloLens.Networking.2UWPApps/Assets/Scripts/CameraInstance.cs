using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInstance : MonoBehaviour {

    public static CameraInstance Instance { get; set; }

    // Use this for initialization
    void Start () {
        Instance = this;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
