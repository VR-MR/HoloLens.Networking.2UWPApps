using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            if (CubeColliders.CubeColliderList.ContainsKey(other.gameObject.name))
            {
                isTriggered = true;
                print("Trigger enter between " + this.name + " and " + other.gameObject.name);
                print("Actions : " + CubeColliders.CubeColliderList[other.gameObject.name].actionsConfiguration);
                TCPClient.Instance.SendToTcp("Trigger enter with " + this.name + ". Actions : " + CubeColliders.CubeColliderList[other.gameObject.name].actionsConfiguration);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isTriggered)
        {
            isTriggered = false;
            print("Trigger exit between " + this.name + " and " + other.gameObject.name);
            print("Actions : " + CubeColliders.CubeColliderList[other.gameObject.name].actionsConfiguration);
            TCPClient.Instance.SendToTcp("Trigger exit with " + this.name + ". Actions : " + CubeColliders.CubeColliderList[other.gameObject.name].actionsConfiguration);
        }
    }

    private bool isCollider = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollider)
        {
            if (CubeColliders.CubeColliderList.ContainsKey(collision.gameObject.name))
            {
                isCollider = true;
                print("Collider enter between " + this.name + " and " + collision.gameObject.name);
                print("Actions : " + CubeColliders.CubeColliderList[collision.gameObject.name].actionsConfiguration);
                TCPClient.Instance.SendToTcp("Collider enter with " + this.name + ". Actions : " + CubeColliders.CubeColliderList[collision.gameObject.name].actionsConfiguration);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isCollider)
        {
            isCollider = false;
            print("Collider exit between " + this.name + " and " + collision.gameObject.name);
            print("Actions : " + CubeColliders.CubeColliderList[collision.gameObject.name].actionsConfiguration);
            TCPClient.Instance.SendToTcp("Collider exit with " + this.name + ". Actions : " + CubeColliders.CubeColliderList[collision.gameObject.name].actionsConfiguration);
        }
    }
}
