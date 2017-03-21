using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float speed = 0.3F;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ManageBoatByCameraPosition();
    }


	private void ManageBoatByCameraPosition()
	{
		if (CameraInstance.Instance.transform.position.x < transform.position.x)
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (CameraInstance.Instance.transform.position.x > transform.position.x)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (CameraInstance.Instance.transform.position.z > transform.position.z)
		{
			transform.position += Vector3.forward * speed * Time.deltaTime;
		}
		if (CameraInstance.Instance.transform.position.z < transform.position.z)
		{
			transform.position += Vector3.back * speed * Time.deltaTime;
		}
	}

}
